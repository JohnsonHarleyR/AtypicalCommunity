using System;
using System.Collections.Generic;
using System.Linq;
using Atypical.Crosscutting.Dtos.Diary;
using Atypical.Crosscutting.Enums;
using Atypical.Data.Repositories.Diary;

namespace Atypical.Domain.Orchestrators.Diary
{
    public class ChartOrchestrator
    {
        private EntryRepository entryRepository = new EntryRepository();


        // Create a chart based on a week's data starting from a given date
        public FullChartDto CreateWeeklyChart(int userId, DateTime endDate) // DateTime startDate)
        {


            // find the start date - 7 days before the end date
            DateTime startDate = endDate.AddDays(-6);

            //// find the end date - 7 days from start date
            //DateTime endDate = startDate.AddDays(7);

            // get the entries based on the userId and start date
            List<EntryDto> entries = entryRepository.GetEntriesByDateRange(userId, startDate.Date,
                endDate.Date);

            entries.OrderByDescending(e => e.DateAndTime);

            // if the list of entries is null, return null
            if (entries == null)
            {
                return null;
            }

            // Create a full chart that will get returned
            FullChartDto newChart = new FullChartDto()
            {
                Type = EChart.Weekly,
                Nodes = new List<ChartNodeDto>()
            };

            // cycle through the days, each time storing data
            DateTime cycleDate = startDate;
            int dayValue = 1; // the first day should equal 1
            while (cycleDate.Date <= endDate.Date)
            {

                // create a temporary DTO for each day - with the mood averages for that day
                ChartNodeDto newNode = new ChartNodeDto();

                // first, get a list of all entries from this day
                List<EntryDto> dayEntries = entryRepository.GetEntriesByDate(userId, cycleDate);
                //IEnumerable<EntryDto> dayEntries = (List<EntryDto>)(from e in entries
                //                            where e.DateAndTime.Day.Equals(cycleDate.Day) select e);

                //throw new Exception($"Entries from {cycleDate}: {dayEntries.Count}");

                // now store the data as a node
                newNode.NodeName = cycleDate.Date.DayOfWeek.ToString();
                //newNode.NodeValue = dayValue;
                newNode.Happy = GetMoodAverage(EMood.Happy, dayEntries);
                newNode.Sad = GetMoodAverage(EMood.Sad, dayEntries);
                newNode.Confident = GetMoodAverage(EMood.Confident, dayEntries);
                newNode.Mad = GetMoodAverage(EMood.Mad, dayEntries);
                newNode.Hopeful = GetMoodAverage(EMood.Hopeful, dayEntries);
                newNode.Scared = GetMoodAverage(EMood.Scared, dayEntries);

                // put the new node into the chart
                newChart.Nodes.Add(newNode);

                // Add to the cycleDatw
                cycleDate = cycleDate.AddDays(1);
                // Add to the day value
                dayValue++;
            }

            //throw new Exception($"Start date: {startDate}, End date: {endDate}, Number of nodes: {newChart.Nodes.Count}");

            // set the title of the new chart
            newChart.Title = $"Week of {startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}";

            // return the new cart
            return newChart;

        }


        // Helpful methods 


        // Get the date of the latest entry for a user
        public DateTime? GetNewestEntryDate(int userId)
        {
            // get a list of entries based on the userId
            List<EntryDto> allEntries = entryRepository.GetEntriesByUserId(userId);

            // if the returned list is null or empty, return null
            if (allEntries == null)
            {
                throw new Exception("User entry list came back null.");
            }

            // if the list is empty, return today's date
            if (allEntries.Count == 0)
            {
                return DateTime.Now.Date;
            }

            // the list should be ordered by the latest date, so return the date
            // of the first entry
            return allEntries.Max(e => e.DateAndTime);


        }

        // Find the average of a mood from a list, based on the chosen mood
        public double? GetMoodAverage(EMood mood, IEnumerable<EntryDto> data)
        {
            // if the list is null, throw an exception
            if (data == null)
            {
                throw new Exception("A null list was entered.");
            }

            // if the list is empty, return null
            if (data.Count() == 0)
            {
                return null;
            }

            // return the average of the chosen mood
            switch (mood)
            {
                case (EMood.Happy):
                    return data.Select(d => d.Happy).Average();
                case (EMood.Sad):
                    return data.Select(d => d.Sad).Average();
                case (EMood.Confident):
                    return data.Select(d => d.Confident).Average();
                case (EMood.Mad):
                    return data.Select(d => d.Mad).Average();
                case (EMood.Hopeful):
                    return data.Select(d => d.Hopeful).Average();
                case (EMood.Scared):
                    return data.Select(d => d.Scared).Average();
                default:
                    throw new Exception("No valid mood was chosen.");
            }

        }
    }
}
