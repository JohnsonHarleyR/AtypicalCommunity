using Atypical.Crosscutting.Dtos.Diary;
using Atypical.Domain.Orchestrators.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atypical.Helpers
{
    public static class EntryHelper
    {
        private static EntryOrchestrator entryOrchestrator = new EntryOrchestrator();

        // find out if the user already has any diary entries for the day
        public static bool HasEntryToday(int userId)
        {
            // grab any entries from today
            List<EntryDto> entries = entryOrchestrator.GetUserEntriesByDate(userId, DateTime.Now);

            // if the list is null or empty, return false
            if (entries == null || entries.Count == 0)
            {
                return false;
            }

            // otherwise return true
            return true;
        }

        // find out if the user already has any diary entries for the day
        public static bool HasEntryOnDate(int userId, DateTime date)
        {
            // grab any entries from today
            List<EntryDto> entries = entryOrchestrator.GetUserEntriesByDate(userId, date);

            // if the list is null or empty, return false
            if (entries == null || entries.Count == 0)
            {
                return false;
            }

            // otherwise return true
            return true;
        }

        // find out how many days in a row the user has entered a diary entry
        public static int GetDaysInRow(int userId)
        {
            int daysInRow = 0;
            DateTime today = DateTime.Now;

            // first check if they have an entry today
            if (HasEntryToday(userId))
            {
                DateTime date;

                // loop until there isn't an entry on a date
                do
                {
                    // add to count and continue looping
                    daysInRow++;

                    // set the date to check equal to today - days in row
                    date = today.AddDays(-1 * daysInRow);

                } while (HasEntryOnDate(userId, date));

            }

            // return days in row
            return daysInRow;

        }

    }
}