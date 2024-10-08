﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Atypical.Domain.Orchestrators.Diary;
using Atypical.Crosscutting.Dtos.Diary;
using Atypical.Web.Models.Diary;
using Atypical.Domain.Orchestrators.User;
using Atypical.Crosscutting.Dtos.User;
using Atypical.Helpers;
using Atypical.Crosscutting.Enums;

namespace Atypical.Controllers
{
    public class EntryController : Controller
    {

        // TODO refactor names of diary entries and charts so that it is more clear what they are

        private EntryOrchestrator entryOrchestrator = new EntryOrchestrator();
        private UserOrchestrator userOrchestrator = new UserOrchestrator();

        // GET: Diary

        // View a particular entry
        public ActionResult View(int? index)
        {
            // TODO if they just created an entry, generate message with how many days in a row and coin bonus

            //first check if the session has a user - if it doesn't, go to login page
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            // now grab the list of entries for that user
            List<DiaryEntryDto> userEntries = entryOrchestrator.GetAllUserEntries((int)Session["userId"]);

            // if the list of entries is empty, redirect to page to create new entry
            if (userEntries == null)
            {
                return RedirectToAction("New", "Entry");
            }

            // if the index is null, set it to 0
            if (index == null)
            {
                index = 0;
            }

            // if the list of entries is null, redirect to the home page
            if (userEntries == null || userEntries.Count == 0)
            {
                // TODO just make the page show that there are no entries yet instead of redirecting
                return RedirectToAction("Index", "Home");
            }

            // grab the item from the list based on the index passed
            DiaryEntryDto entry = userEntries[(int)index];

            // if the item is null, redirect to the home page
            if (entry == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // otherwise, convert entryDto to a model
            EntryViewModel entryModel = new EntryViewModel()
            {
                Id = entry.Id,
                UserId = entry.UserId,
                DateAndTime = entry.DateAndTime,
                Happy = entry.Happy,
                Sad = entry.Sad,
                Confident = entry.Confident,
                Mad = entry.Mad,
                Hopeful = entry.Hopeful,
                Scared = entry.Scared,
                Title = entry.Title,
                Text = entry.Text
            };

            // Also tell the view both the index of the entry AND
            // how many entries there are for that user
            ViewBag.EntryIndex = index;
            ViewBag.FinalIndex = userEntries.Count - 1;


            // pass the model and return the view
            return View(entryModel);

        }


        // Create new entry
        public ActionResult New()
        {
            // check if there is a username in the session

            // If there's no user in the session, redirect to the login page
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            // If the user has not confirmed their account, redirect to a page asking them to
            else {
                UserDto user = userOrchestrator
                    .GetUserById(Int32.Parse(Session["userId"].ToString()));

                if (user != null && !user.IsEmailConfirmed)
                {
                    return RedirectToAction("ConfirmEmail", "Error");
                }
            }

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(NewEntryViewModel model)
        {
            // validate model
            if (ModelState.IsValid)
            {

                // Create the entry
                DiaryEntryDto newEntry = new DiaryEntryDto()
                {
                    UserId = model.UserId,
                    Happy = model.Happy,
                    Sad = model.Sad,
                    Confident = model.Confident,
                    Mad = model.Mad,
                    Hopeful = model.Hopeful,
                    Scared = model.Scared,
                    Title = model.Title,
                    Text = entryOrchestrator.ConvertBBCodeToHtml(model.Text)
                };

                bool firstEntry = false;
                // check if it's the first entry today
                if (!EntryHelper.HasEntryToday(newEntry.UserId))
                {
                    firstEntry = true;

                }

                // validate success
                bool successfullyCreatedEntry = entryOrchestrator.CreateEntry(newEntry);

                if (successfullyCreatedEntry == true)
                {
                    // generate coin if it's the first entry
                    if (firstEntry)
                    {
                        // if so, generate coin for the user
                        CoinHelper.GenerateCoin(newEntry.UserId,
                            (int)MinCoinValues.NewDiaryEntry, (int)MaxCoinValues.NewDiaryEntry);

                        // determine their bonus for entries in a row
                        int bonus = EntryHelper.GetDaysInRow(newEntry.UserId) * (int)MaxCoinValues.DiaryEntryBonus;
                        // generate coin with the bonus as both the min and max
                        CoinHelper.GenerateCoin(newEntry.UserId, bonus, bonus);
                    }

                    return RedirectToAction("View", "Entry");
                }
                else // otherwise create an error
                {
                    ModelState.AddModelError(string.Empty, "New entry could not be created.");
                }
            }

            return View();
        }


        // Edit an entry
        public ActionResult Edit(int? id)
        {
            // check if there is a username in the session

            // If there's no user in the session or index, redirect to the login page
            if (Session["username"] == null || id == null)
            {
                return RedirectToAction("Login", "User");
            }

            // now grab the correct entry from the orchestrator
            DiaryEntryDto entry = entryOrchestrator.GetEntryById((int)id);

            // if the entry is null, redirect to home page
            if (entry == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // otherwise, convert entryDto to a model
            EntryViewModel entryModel = new EntryViewModel()
            {
                Id = entry.Id,
                UserId = entry.UserId,
                DateAndTime = entry.DateAndTime,
                Happy = entry.Happy,
                Sad = entry.Sad,
                Confident = entry.Confident,
                Mad = entry.Mad,
                Hopeful = entry.Hopeful,
                Scared = entry.Scared,
                Title = entry.Title,
                // turn the html back into BBCode
                Text = entryOrchestrator.ConvertHtmlToBBCode(entry.Text)
            };

            // pass the model and return the view
            return View(entryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntryViewModel model)
        {
            // validate model
            if (ModelState.IsValid)
            {

                // Create the entry
                DiaryEntryDto updatedEntry = new DiaryEntryDto()
                {
                    Id = model.Id,
                    Happy = model.Happy,
                    Sad = model.Sad,
                    Confident = model.Confident,
                    Mad = model.Mad,
                    Hopeful = model.Hopeful,
                    Scared = model.Scared,
                    Title = model.Title,
                    Text = entryOrchestrator.ConvertBBCodeToHtml(model.Text)
                };

                // validate success
                bool successfullyUpdatedEntry = entryOrchestrator.UpdateEntry(updatedEntry);

                if (successfullyUpdatedEntry == true)
                { // go back to see the same entry

                    // first double check there is a session user
                    if (Session["username"] == null)
                    {
                        return RedirectToAction("Login", "User");
                    }

                    // otherwise, get a list of that user's entries to identify index 
                    List<DiaryEntryDto> userEntries = entryOrchestrator.GetAllUserEntries((int)Session["userId"]);

                    // if the list of entries is empty, redirect regular view pagw
                    if (userEntries == null)
                    {
                        return RedirectToAction("View", "Entry");
                    }

                    // otherwise, find out the index of the entry for that user
                    int entryIndex = userEntries.FindIndex(e => e.Id.Equals(model.Id));

                    // now redirect to that entry view
                    return RedirectToAction("View", "Entry", new { index = entryIndex });
                }
                else // otherwise create an error
                {
                    ModelState.AddModelError(string.Empty, "Entry could not be updated.");
                }


            }

            return View();
        }


        // For custom attributes
        // Validate model entry text, that it does not contain html tags
        public JsonResult NoHtmlTags(string text)
        {
            // if it contains HTML brackets
            if (text.Contains("<"))
            {
                if (text.Contains(">"))
                {
                    return Json(false);
                }

            }

            return Json(true);

        }



    }
}