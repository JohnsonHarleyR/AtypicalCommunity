using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Atypical.Crosscutting.Dtos.User;
using Atypical.Domain.Orchestrators.User;
using Atypical.Web.Models.User;

namespace Atypical.Web.Controllers
{
    public class HomeController : Controller
    {

        private UserOrchestrator userOrchestrator = new UserOrchestrator();

        public ActionResult Index()
        {

            if (Session["username"] != null)
            {
                UserDto userDto = userOrchestrator.GetUserById(Int32.Parse(Session["userId"].ToString()));
                UserViewModel user = new UserViewModel()
                {
                    Username = userDto.Username,
                    FirstName = userDto.FirstName,
                    ProfileImageUrl = userDto.ProfileImageUrl
                };

                return View(user);
            }

            return View();
        }

    }
}