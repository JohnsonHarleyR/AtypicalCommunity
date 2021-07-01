using System;
using System.Web.Mvc;
using Atypical.Domain.Orchestrators.Diary;
using Atypical.Crosscutting.Dtos.Diary;
using Atypical.Web.Models.Diary;
using Atypical.Web.Helpers;

namespace Atypical.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart

        //private EntryOrchestrator entryOrchestrator = new EntryOrchestrator();
        private ChartOrchestrator chartOrchestrator = new ChartOrchestrator();




        // See a weekly chart
        public ActionResult Weekly(DateTime? endDate)
        {

            //first check if the session has a user - if it doesn't, go to login page
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            // if the session userId is null for some weird reason, which it shouldn't be if
            // there is a user assigned to the session, then throw an exception
            if (Session["userId"] == null)
            {
                throw new Exception("Session user Id is null.");
            }

            // if the endDate is null, set it to the latest entry date - or today if there
            // is none - (this is done in the method called from the orchestrator)
            if (endDate == null)
            {
                endDate = chartOrchestrator.GetNewestEntryDate((int)Session["userId"]);
            }

            // now a chart needs to be generated based on the information
            FullChartDto weeklyChartDto = chartOrchestrator.CreateWeeklyChart((int)Session["userId"],
                (DateTime)endDate);

            // if the weekly chart dto is null, redirect to homepage
            if (weeklyChartDto == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // turn the weekly chart dto into a model
            ChartViewModel weeklyChartModel = ChartHelper.TurnChartToModel(weeklyChartDto);

            return View(weeklyChartModel);
        }



    }
}