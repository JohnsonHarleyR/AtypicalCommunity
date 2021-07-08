using Atypical.Crosscutting.Dtos.Diary;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace Atypical.Data.Repositories.Diary
{
    public class DiaryRepository
    {
        private string Schema = @"[db_owner]";
        private string ConnectionString;

        public DiaryRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Atypical"].ConnectionString;
        }

        // check if the table exists
        public bool TableExists()
        {

            IEnumerable<DiaryEntryDto> entries;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetAllDiaryEntries";

                entries = connection.Query<DiaryEntryDto>(sql,
                    commandType: System.Data.CommandType.StoredProcedure);

                try
                {
                    entries = connection.Query<DiaryEntryDto>(sql,
                    commandType: System.Data.CommandType.StoredProcedure);

                    return true;

                }
                catch (Exception)
                {
                    return false;
                }

            }

        }

        // create table if it doesn't exist
        public void CreateTable()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.CreateDiaryTable";

                connection.Execute(sql,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// Add a new diary entry.
        /// </summary>
        /// <param name="entryDto"></param>
        public void AddEntry(DiaryEntryDto entryDto)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.AddDiaryEntry";

                connection.Execute(sql,
                    new
                    {
                        UserId = entryDto.UserId,
                        DateAndTime = DateTime.Now,
                        Happy = entryDto.Happy,
                        Sad = entryDto.Sad,
                        Confident = entryDto.Confident,
                        Mad = entryDto.Mad,
                        Hopeful = entryDto.Hopeful,
                        Scared = entryDto.Scared,
                        Title = entryDto.Title,
                        Text = entryDto.Text
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

        }

        /// <summary>
        /// Update a diary entry.
        /// </summary>
        /// <param name="entryDto"></param>
        public void UpdateEntry(DiaryEntryDto entryDto)
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.UpdateDiaryEntry";

                connection.Execute(sql,
                    new
                    {
                        Id = entryDto.Id,
                        UserId = entryDto.UserId,
                        DateAndTime = DateTime.Now,
                        Happy = entryDto.Happy,
                        Sad = entryDto.Sad,
                        Confident = entryDto.Confident,
                        Mad = entryDto.Mad,
                        Hopeful = entryDto.Hopeful,
                        Scared = entryDto.Scared,
                        Title = entryDto.Title,
                        Text = entryDto.Text
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

        }

        /// <summary>
        /// Get all entries in the database based on a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<DiaryEntryDto> GetEntriesByUserId(int userId)
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            IEnumerable<DiaryEntryDto> entries = new List<DiaryEntryDto>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetDiaryEntriesByUserId";

                entries = connection.Query<DiaryEntryDto>(sql,
                    new {UserId = userId},
                    commandType: System.Data.CommandType.StoredProcedure);

            }

            // sort the entries by descending date - the default
            entries = entries.OrderByDescending(e => e.DateAndTime).ToList();

            
            return entries;
        }


        /// <summary>
        /// Grab an entry based on the entry's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DiaryEntryDto GetEntryById(int id)
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            DiaryEntryDto entry = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetDiaryEntryById";

                entry = connection.Query<DiaryEntryDto>(sql,
                    new { Id = id },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }

            // TODO add all diary entries to user


            return entry;
        }
    


        /// <summary>
        /// Get all entries in the database based on a date and a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateAndTime"></param>
        /// <returns></returns>
        public IEnumerable<DiaryEntryDto> GetEntriesByDate(int userId, DateTime dateAndTime)
        {

        // first check that table exists - if not, create the table
        if (!TableExists())
        {
            CreateTable();
        }

        IEnumerable<DiaryEntryDto> entries = new List<DiaryEntryDto>();

        DateTime date = new DateTime(dateAndTime.Year, dateAndTime.Month, dateAndTime.Day,
                0, 0, 0);
        DateTime nextDate = date.AddDays(1).AddSeconds(-1);

        using (var connection = new SqlConnection(ConnectionString))
        {
            string sql = $"{Schema}.GetDiaryEntriesByDateRange";

            entries = connection.Query<DiaryEntryDto>(sql,
                new { 
                    UserId = userId,
                    Date = date,
                    NextDate = nextDate
                },
                commandType: System.Data.CommandType.StoredProcedure);

        }

        // sort the entries by descending date - the default
        entries = entries.OrderByDescending(e => e.DateAndTime).ToList();


        return entries;
        }

        /// <summary>
        /// Get all entries in the database based on a date range and a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateAndTimeMin"></param>
        /// <param name="dateAndTimeMax"></param>
        /// <returns></returns>
        public IEnumerable<DiaryEntryDto> GetEntriesByDateRange(int userId, DateTime dateAndTimeMin, DateTime dateAndTimeMax)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            IEnumerable<DiaryEntryDto> entries = new List<DiaryEntryDto>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetDiaryEntriesByDateRange";

                entries = connection.Query<DiaryEntryDto>(sql,
                    new
                    {
                        UserId = userId,
                        Date = dateAndTimeMin,
                        NextDate = dateAndTimeMax
                    },
                    commandType: System.Data.CommandType.StoredProcedure);

            }

            // sort the entries by descending date - the default
            entries = entries.OrderByDescending(e => e.DateAndTime).ToList();


            return entries;
        }

        // TODO Add tags to entries that user can search for
        // TODO Add other ways to pull up entries
    }
}
