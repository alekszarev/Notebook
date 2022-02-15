using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Data.Models;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Services;
using Notebook.Web.Models;
using Notebook.Web.Models.ModelMapper;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Notebook.Web.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly ICalendarOldService calendarService;
        private readonly IAppointmentService appointmentService;
        private readonly IDayService dayService;
        private readonly IProcedureService procedureService;
        private readonly IUserBufferService userBufferService;
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IHourService hourService;
        private readonly ModelMapper modelMapper;
        public OwnerController(ILogger<OwnerController> logger,
            ICalendarOldService calendarService,
            IAppointmentService appointmentService,
            IDayService dayService,
                        IHourService hourService,
            IProcedureService procedureService,
            IUserService userService,
            ModelMapper modelMapper,
            IUserBufferService userBufferService,
            IMessageService messageService)
        {
            this.modelMapper = modelMapper;
            this.hourService=hourService;
            this.userService = userService;
            this.procedureService = procedureService;
            this.calendarService = calendarService;
            this.appointmentService = appointmentService;
            this.dayService = dayService;
            this.userBufferService = userBufferService;
            this.messageService = messageService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Messages()
        {
            var messages = this.messageService.GetAll().ToList();
            return this.View(messages);
        }

        public IActionResult SendMessage(MessageWebModel messageWeb)
        {
            var message = this.modelMapper.ToModel(messageWeb);
            this.messageService.Send(message);
            return RedirectToAction("Messages");
        }

        public IActionResult WriteMessage()
        {
            var message = new MessageWebModel();
            message.ListUsers = GetUsers();
            return this.View(message);
        }
        public IActionResult ForApprovement()
        {
            var apps = this.appointmentService.GetAll()
               .Where(x => x.IsAproved == false)
               .Include(x => x.Procedure)
               .Include(x => x.User)
               .Include(x => x.UserBuffer)
               .Include(x => x.Hour)
               .ThenInclude(x => x.Day)
               .ThenInclude(x => x.Month)
               .OrderBy(x => x.Hour.Day.MonthId)
               .ThenBy(x => x.Hour.DayId).ToList();

            return View(apps);

        }

        //----Status---//
        public IActionResult SetStatus(int hourId)
        {
            var appointment = this.appointmentService.Get(hourId);
            return this.View(appointment);
        }
        [HttpPost]
        public IActionResult Confirm(int id)
        {
            this.appointmentService.SetStatus(id);
            return RedirectToAction("Appointments");
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Calendar(int month)
        {

            var calendar = await this.calendarService.GetCalendarAsync(1, month, null, null, null);
            return View(calendar);
        }


        //--------------------Day-----------------------//
        [Authorize(Roles = "Owner")]
        public IActionResult Day(int id)
        {
            var day = this.dayService.Get(id);
            return View(day);
        }

        //------------------------------------APPOINTMENTS----------------------------------//
        [Authorize(Roles = "Owner")]
        public IActionResult Appointments(bool? delete)
        {

            var apps = this.appointmentService.GetAll()
                .Include(x => x.Procedure)
                .Include(x => x.User)
                .Include(x => x.UserBuffer)
                .Include(x => x.Hour)
                .ThenInclude(x => x.Day)
                .ThenInclude(x => x.Month)
                .OrderBy(x => x.Hour.Day.MonthId)
                .ThenBy(x => x.Hour.DayId).ToList();

            return View(apps);
        }

        //----------------------------------------APPROVE-----------------------------------//
        [HttpPost]
        public IActionResult Send(UserBufferWebModel userWeb)
        {
            try
            {

                var user = this.userBufferService.Create(userWeb.Name, userWeb.Email, userWeb.Phone, userWeb.HourId, userWeb.ProcedureId);
                var app = new Appointment
                {
                    HourId = (int)user.HourId,
                    UserBufferId = user.UserBuferId,
                    ProcedureId = (int)user.ProcedureId,
                    IsAproved = true,

                };
                this.appointmentService.MakeAppointment(app);
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

        public IActionResult MakeAppointment([FromQuery] int hourId)
        {
            var userWeb = new UserBufferWebModel
            {
                HourId = hourId,
                Procedure = GetProcedures(),
                Date = $"{this.hourService.Get(hourId).Clock}:00ч.  {this.hourService.Get(hourId).Day.Date} {this.hourService.Get(hourId).Day.Month.MonthName} "
            };
            return this.View(userWeb);
        }

        //---Procedures---//
        private SelectList GetProcedures()
        {
            var procedures = this.procedureService.GetAll();
            return new SelectList(procedures, "ProcedureId", "ProcedureName");
        }

        //----Users----//
        private SelectList GetUsers()
        {
            var users = this.userService.GetAll().ToList();
            foreach(var user in users)
            {
                if (user.Name == null)
                {
                    user.Name = user.UserName;
                }
            }
            return new SelectList(users, "Id", "Name");
        }

    }
}
