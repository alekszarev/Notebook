using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Data.Models;
using Notebook.Service.Contracts;
using Notebook.Service.Services;
using Notebook.Web.Models;
using Notebook.Web.Models.ModelMapper;
using System.Security.Claims;

namespace Notebook.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ICalendarOldService calendarService;
        private readonly IAppointmentService appointmentService;
        private readonly IDayService dayService;
        private readonly IProcedureService procedureService;
        private readonly IUserService userService;
        private readonly ModelMapper modelMapper;
        private readonly IHourService hourService;
        private readonly UserManager<User> userManager;

        public ClientController(ILogger<ClientController> logger,
            ICalendarOldService calendarService,
            IAppointmentService appointmentService,
            IDayService dayService,
                        IHourService hourService,
            IProcedureService procedureService,
            IUserService userService,
            ModelMapper modelMapper,
            UserManager<User> userManager)
        {
            this.hourService = hourService;
            this.modelMapper = modelMapper;
            this.procedureService = procedureService;
            this.calendarService = calendarService;
            this.appointmentService = appointmentService;
            this.dayService = dayService;
            this.userService = userService;
            this.userManager = userManager;
            _logger = logger;
        }

        //----------------------------------------APPROVE-----------------------------------//
        [HttpPost]
        public IActionResult Send([FromForm] SendModel sendModel)
        {
            try
            {
                var model = new SendModel
                {
                    Day = this.dayService.Get(sendModel.DayId),
                    Hour = this.hourService.Get(sendModel.HourId),
                    UserId = sendModel.UserId,
                    ProcedureId = sendModel.ProcedureId,
                };

                var appointment = this.modelMapper.ToModel(model);
                var hour = this.hourService.Get(sendModel.HourId + 1);
                if(hour.IsBusy == true && appointment.ProcedureId > 1)
                {
                    throw new ArgumentException("За съжаление свободен е само 1 астрономически час, който не е достатъчен за посочената процедура.");
                }
                this.appointmentService.MakeAppointment(appointment);
                return this.Redirect("Appointments");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public IActionResult Approve([FromQuery] int day, [FromQuery] int hour)
        {

            var dayObject = dayService.Get(day);
            var hourObject = dayObject.Hours.FirstOrDefault(x => x.Clock == hour);
            var listProcedures = this.procedureService.GetAll().ToList();

            var appModel = new SendModel()
            {
                Day = dayObject,
                Hour = hourObject,
                Procedure = GetProcedures(),

            };

            return this.View(appModel);
        }

        //------------------------------------APPOINTMENTS----------------------------------//
        public IActionResult Appointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var apps = this.appointmentService.ClientAppointments(userId)
                .Include(x=>x.User)
                .Include(x => x.Hour)
                .ThenInclude(x => x.Day)
                .ThenInclude(x => x.Month)
                .OrderBy(x => x.Hour.Day.MonthId)
                .ThenBy(x => x.Hour.DayId).ToList();

            var list = new List<AppointmentWebModel>();
            foreach (var appointment in apps)
            {
                list.Add(this.modelMapper.ToModel(appointment));

            }

            return View(list);
        }
        public IActionResult ForAprrovement()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var apps = this.appointmentService.ClientAppointments(userId)
                .Include(x => x.User)
                .Include(x => x.Hour)
                .ThenInclude(x => x.Day)
                .ThenInclude(x => x.Month)
                .OrderBy(x => x.Hour.Day.MonthId)
                .ThenBy(x => x.Hour.DayId)
                .Where(x=>x.IsAproved ==false).ToList();

            var list = new List<AppointmentWebModel>();
            foreach (var appointment in apps)
            {
                list.Add(this.modelMapper.ToModel(appointment));

            }

            return View(list);
        }

        //----------------Cancel--------------//
        public IActionResult Cancel(int id, bool? delete)
        {
            if (delete == true)
            {
                this.appointmentService.Delete(id);
                return this.Redirect("Appointments");
            }
            return this.View(id);
        }

        //--------------------Day-----------------------//
        public IActionResult Day(int id)
        {
            var day = this.dayService.Get(id);
            return View(day);
        }

        //--------------------Index-----------------------//
        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return RedirectToAction("Owner");
            }
            else if (HttpContext.User.IsInRole("Client"))
            {
            return View();             
            }

            else
            {
                throw new ArgumentException("Не сте влезли в профила си");
            }
        }
        //--------------------Calendar-----------------------//
        public async Task<IActionResult> Calendar(int month)
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return RedirectToAction("Calendar");
            }
            else
            {
                var calendar = await this.calendarService.GetCalendarAsync(1, month, null, null, null);
                return View(calendar);
            }
        }

        //---Procedures---//
        private SelectList GetProcedures()
        {
            var procedures = this.procedureService.GetAll();
            return new SelectList(procedures, "ProcedureId", "ProcedureName");
        }
    }
}
