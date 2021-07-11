using Atypical.Crosscutting.Dtos.Bank;
using Atypical.Domain.Orchestrators.Bank;
using Atypical.Web.Models.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Atypical.Controllers
{
    public class BankController : Controller
    {
        private BankOrchestrator bankOrchestrator = new BankOrchestrator();




        // Partial view to show amount in navbar
        [ChildActionOnly]
        public ActionResult RenderMoney(BankAccountViewModel model)
        {

            // check if there's a user in the session
            if (Session["username"] != null && Session["userId"] != null)
            {
                // if there is, grab the account dto for the user
                BankAccountDto accountDto = bankOrchestrator.GetAccountByUserId((int)Session["userId"]);

                // if the accountDto is null, set the session username and userId to null
                if (accountDto == null)
                {
                    Session["username"] = null;
                    Session["userId"] = null;
                }

                // otherwise, add values to the model
                model.UserId = accountDto.UserId;
                model.Checking = accountDto.Checking;
                model.Savings = accountDto.Savings;

            }

            // return view with model
            return PartialView("_Money", model);
        }

    }
}