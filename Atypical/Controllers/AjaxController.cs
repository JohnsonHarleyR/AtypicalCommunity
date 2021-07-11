using Atypical.Crosscutting.Dtos.Bank;
using Atypical.Crosscutting.Dtos.User;
using Atypical.Domain.Orchestrators.Bank;
using Atypical.Domain.Orchestrators.User;
using Atypical.Helpers;
using Atypical.Web.Helpers;
using Atypical.Web.Models.Bank;
using Atypical.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Atypical.Controllers
{
    public class AjaxController : Controller
    {
        private UserOrchestrator userOrchestrator = new UserOrchestrator();
        private BankOrchestrator bankOrchestrator = new BankOrchestrator();

        // GET: Ajax

        [HttpPost]
        public JsonResult GetUserInfo()
        {
            // create user and initially set them to null
            UserViewModel userModel = null;

            // find out if a user exists in the session or not.
            if (Session["username"] != null && Session["userId"] != null)
            {
                // if they do, grab the user to return them
                UserDto userDto = userOrchestrator.GetUserById((int)Session["userId"]);
                userModel = UserHelper.ConvertUserDtoToModel(userDto);
            }

            // return result in json
            return Json(userModel);

        }

        [HttpPost]
        public JsonResult GetCoinInfo()
        {
            // create user and initially set them to null
            BankAccountViewModel accountModel = null;

            // find out if a user exists in the session or not.
            if (Session["username"] != null && Session["userId"] != null)
            {
                // if they do, grab the bank account by user id
                BankAccountDto account = bankOrchestrator.GetAccountByUserId((int)Session["userId"]);
                accountModel = CoinHelper.ConvertBankAccountDtoToModel(account);
            }

            // return result in json
            return Json(accountModel);
        }

    }
}