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

            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"INSERT INTO Entry " +
                    $@"(UserId, DateAndTime, Title, Text, Happy, Sad, Confident, Mad, Hopeful, Scared)" +
                    $@"VALUES (@UserId, @DateAndTime, @Title, @Text, @Happy, @Sad, @Confident, @Mad, @Hopeful, @Scared)";

                SqliteCommand command = new SqliteCommand(sql, connection);

                // TODO Decide if there should be anything that can't be null - there probably should be lol

                command.Parameters.AddWithValue("@UserId", entryDto.UserId);
                command.Parameters.AddWithValue("@DateAndTime", DateTime.Now); // Enter the current datetime for this

                command.Parameters.AddWithValue("@Title", entryDto.Title);

                command.Parameters.AddWithValue("@Text", entryDto.Text);

                command.Parameters.AddWithValue("@Happy", entryDto.Happy);

                command.Parameters.AddWithValue("@Sad", entryDto.Sad);

                command.Parameters.AddWithValue("@Confident", entryDto.Confident);

                command.Parameters.AddWithValue("@Mad", entryDto.Mad);

                command.Parameters.AddWithValue("@Hopeful", entryDto.Hopeful);

                command.Parameters.AddWithValue("@Scared", entryDto.Scared);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

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

            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"UPDATE Entry " +
                    $@"SET Title = @Title, Text = @Text, Happy = @Happy, Sad = @Sad, " +
                    $@"Confident = @Confident, Mad = @Mad, Hopeful = @Hopeful, Scared = @Scared " +
                    $@"WHERE Id = @Id;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", entryDto.Id);

                command.Parameters.AddWithValue("@Title", entryDto.Title);

                command.Parameters.AddWithValue("@Text", entryDto.Text);

                command.Parameters.AddWithValue("@Happy", entryDto.Happy);

                command.Parameters.AddWithValue("@Sad", entryDto.Sad);

                command.Parameters.AddWithValue("@Confident", entryDto.Confident);

                command.Parameters.AddWithValue("@Mad", entryDto.Mad);

                command.Parameters.AddWithValue("@Hopeful", entryDto.Hopeful);

                command.Parameters.AddWithValue("@Scared", entryDto.Scared);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }

        }

        /// <summary>
        /// Get all entries in the database based on a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DiaryEntryDto> GetEntriesByUserId(int userId)
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            List<DiaryEntryDto> entries = new List<DiaryEntryDto>();

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM Entry WHERE UserId = @UserId";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entries.Add(
                            new DiaryEntryDto()
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                UserId = Int32.Parse(reader["UserId"].ToString()),
                                DateAndTime = DateTime.Parse(reader["DateAndTime"].ToString()),
                                Happy = Int32.Parse(reader["Happy"].ToString()),
                                Sad = Int32.Parse(reader["Sad"].ToString()),
                                Confident = Int32.Parse(reader["Confident"].ToString()),
                                Mad = Int32.Parse(reader["Mad"].ToString()),
                                Hopeful = Int32.Parse(reader["Hopeful"].ToString()),
                                Scared = Int32.Parse(reader["Scared"].ToString()),
                                Title = reader["Title"].ToString(),
                                Text = reader["Text"].ToString()
                            });

                    }

                    connection.Close();
                }

                // sort the entries by descending date - the default
                entries = entries.OrderByDescending(e => e.DateAndTime).ToList();

            }
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

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM Entry WHERE Id = @Id;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        entry = new DiaryEntryDto()
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            UserId = Int32.Parse(reader["UserId"].ToString()),
                            DateAndTime = DateTime.Parse(reader["DateAndTime"].ToString()),
                            Happy = Int32.Parse(reader["Happy"].ToString()),
                            Sad = Int32.Parse(reader["Sad"].ToString()),
                            Confident = Int32.Parse(reader["Confident"].ToString()),
                            Mad = Int32.Parse(reader["Mad"].ToString()),
                            Hopeful = Int32.Parse(reader["Hopeful"].ToString()),
                            Scared = Int32.Parse(reader["Scared"].ToString()),
                            Title = reader["Title"].ToString(),
                            Text = reader["Text"].ToString()
                        };
                    }
                    connection.Close();
                }

            }
            return entry;
        }


        /// <summary>
        /// Get all entries in the database based on a date and a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateAndTime"></param>
        /// <returns></returns>
        public List<DiaryEntryDto> GetEntriesByDate(int userId, DateTime dateAndTime)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            List<DiaryEntryDto> entries = new List<DiaryEntryDto>();

            DateTime date = new DateTime(dateAndTime.Year, dateAndTime.Month, dateAndTime.Day,
                0, 0, 0);
            DateTime nextDate = date.AddDays(1).AddSeconds(-1);

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                //string sql = $@"SELECT * FROM Entry WHERE CAST(DateAndTime AS DATE) = CAST(@DateAndTime AS DATE) AND UserId = @UserId;";
                string sql = $@"SELECT * FROM Entry WHERE (DateAndTime BETWEEN @Date AND @NextDate) AND UserId = @UserId;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Date", date);
                command.Parameters.AddWithValue("@NextDate", nextDate);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entries.Add(
                            new DiaryEntryDto()
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                UserId = Int32.Parse(reader["UserId"].ToString()),
                                DateAndTime = DateTime.Parse(reader["DateAndTime"].ToString()),
                                Happy = Int32.Parse(reader["Happy"].ToString()),
                                Sad = Int32.Parse(reader["Sad"].ToString()),
                                Confident = Int32.Parse(reader["Confident"].ToString()),
                                Mad = Int32.Parse(reader["Mad"].ToString()),
                                Hopeful = Int32.Parse(reader["Hopeful"].ToString()),
                                Scared = Int32.Parse(reader["Scared"].ToString()),
                                Title = reader["Title"].ToString(),
                                Text = reader["Text"].ToString()
                            });

                    }

                    connection.Close();
                }

                // sort the entries by descending date - the default
                entries.OrderByDescending(e => e.DateAndTime);
                entries.Reverse();

            }
            return entries;
        }

        /// <summary>
        /// Get all entries in the database based on a date range and a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateAndTimeMin"></param>
        /// <param name="dateAndTimeMax"></param>
        /// <returns></returns>
        public List<DiaryEntryDto> GetEntriesByDateRange(int userId, DateTime dateAndTimeMin, DateTime dateAndTimeMax)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            List<DiaryEntryDto> entries = new List<DiaryEntryDto>();

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM Entry WHERE CAST(DateAndTime AS DATE) >= CAST(@DateAndTimeMin AS DATE) " +
                                $@"AND CAST(DateAndTime AS DATE) <= CAST(@DateAndTimeMax AS DATE) AND UserId = @UserId;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@DateAndTimeMin", dateAndTimeMin);
                command.Parameters.AddWithValue("@DateAndTimeMax", dateAndTimeMax);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entries.Add(
                            new DiaryEntryDto()
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                UserId = Int32.Parse(reader["UserId"].ToString()),
                                DateAndTime = DateTime.Parse(reader["DateAndTime"].ToString()),
                                Happy = Int32.Parse(reader["Happy"].ToString()),
                                Sad = Int32.Parse(reader["Sad"].ToString()),
                                Confident = Int32.Parse(reader["Confident"].ToString()),
                                Mad = Int32.Parse(reader["Mad"].ToString()),
                                Hopeful = Int32.Parse(reader["Hopeful"].ToString()),
                                Scared = Int32.Parse(reader["Scared"].ToString()),
                                Title = reader["Title"].ToString(),
                                Text = reader["Text"].ToString()
                            });

                    }

                    connection.Close();
                }

                // sort the entries by descending date - the default
                entries.OrderByDescending(e => e.DateAndTime);

            }
            return entries;
        }

        // TODO Add tags to entries that user can search for
        // TODO Add other ways to pull up entries
    }
}
