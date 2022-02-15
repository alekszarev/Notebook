using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Service.Contracts;
using Notebook.Service.Services;
using Notebook.Web.Models;
using Notebook.Web.Models.ModelMapper;
using System.Diagnostics;
using System.Security.Claims;

namespace Notebook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICalendarOldService calendarService;
        private readonly IAppointmentService appointmentService;
        private readonly IDayService dayService;
        private readonly IProcedureService procedureService;
        private readonly IUserService userService;
        private readonly ModelMapper modelMapper;
        private readonly IHourService hourService;
        public HomeController(ILogger<HomeController> logger,
            ICalendarOldService calendarService,
            IAppointmentService appointmentService,
            IDayService dayService,
                        IHourService hourService,
            IProcedureService procedureService,
            IUserService userService,
            ModelMapper modelMapper)
        {
            this.hourService = hourService;
            this.modelMapper = modelMapper;
            this.procedureService = procedureService;
            this.calendarService = calendarService;
            this.appointmentService = appointmentService;
            this.dayService = dayService;
            this.userService = userService;
            _logger = logger;
        }


        public IActionResult Gallery()
        {
            return View();
        }
        public async Task<IActionResult> Approve()
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return Redirect("/Owner/Approve");
            }
            else if (HttpContext.User.IsInRole("Client"))
            {
                return Redirect("/Client/Approve");
            }
            else
            {
                throw new ArgumentException("Нужна е регистрация");
            }
        }

        public async Task<IActionResult> Messages()
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return Redirect("/Owner/Messages");
            }
            else if (HttpContext.User.IsInRole("Client"))
            {
                return Redirect("/Client/Messages");
            }
            else
            {
                throw new ArgumentException("Нужна е регистрация");
            }
        }

        public async Task<IActionResult> Calendar()
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return Redirect("/Owner/Calendar");
            }
            else if (HttpContext.User.IsInRole("Client"))
            {
                return Redirect("/Client/Calendar");
            }
            else
            {
                throw new ArgumentException("Нужна е регистрация");
            }
        }

        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return Redirect("Owner");
            }
            else if (HttpContext.User.IsInRole("Client"))
            {
                return Redirect("Client");
            }

            else
            {
                return View();
            }
        }


        public IActionResult Appointments()
        {
            if (HttpContext.User.IsInRole("Owner"))
            {
                return Redirect("/Owner/Appointments");
            }
            else if (HttpContext.User.IsInRole("Client"))
            {
                return Redirect("/Client/Appointments");
            }

            else
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(Exception ex)
        {
            return View(ex);
        }
    }
}