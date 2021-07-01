using System;
using System.Collections.Generic;
using Atypical.Data.Repositories.Diary;
using Atypical.Crosscutting.Dtos.Diary;
using System.Text.RegularExpressions;

namespace Atypical.Domain.Orchestrators.Diary
{
    public class EntryOrchestrator
    {
        private EntryRepository entryRepository;

        public EntryOrchestrator()
        {
            entryRepository = new EntryRepository();
        }


        // Create a new entry
        public bool CreateEntry(EntryDto entryDto)
        {
            // if the dto is null, return false
            if (entryDto == null)
            {
                return false;
            }

            // Add the dto to the repo
            entryRepository.AddEntry(entryDto);


            // return true that it was successful
            return true;

        }

        // Update a new entry
        public bool UpdateEntry(EntryDto entryDto)
        {
            // if the dto is null, return false
            if (entryDto == null)
            {
                return false;
            }

            // update the entry in the repo
            entryRepository.UpdateEntry(entryDto);


            // return true that it was successful
            return true;

        }


        // Methods to get entry or entries

        public EntryDto GetEntryById(int id)
        {
            EntryDto entryDto = entryRepository.GetEntryById(id);

            if (entryDto == null)
            {
                return null;
            }

            return entryDto;
        }

        public List<EntryDto> GetAllUserEntries(int userId)
        {
            List<EntryDto> entries = entryRepository.GetEntriesByUserId(userId);

            if (entries == null)
            {
                return null;
            }

            return entries;
        }

        public List<EntryDto> GetUserEntriesByDate(int userId, DateTime dateAndTime)
        {
            List<EntryDto> entries = entryRepository.GetEntriesByDate(userId, dateAndTime);

            if (entries == null)
            {
                return null;
            }

            return entries;
        }

        public List<EntryDto> GetUserEntriesByDateRange(int userId, DateTime dateAndTimeMin, DateTime dateAndTimeMax)
        {
            List<EntryDto> entries = entryRepository.GetEntriesByDateRange(userId, dateAndTimeMin, dateAndTimeMax);

            if (entries == null)
            {
                return null;
            }

            return entries;
        }

        // Convert any valid BBCode in the text string
        public string ConvertBBCodeToHtml(string text)
        {
            string newText = text;

            // replace allowed BBCode tags with html
            newText = Regex.Replace(newText, @"\[br\]", "<br><br>", RegexOptions.IgnoreCase);
            newText = Regex.Replace(newText, @"\[b\]", "<b>", RegexOptions.IgnoreCase);
            newText = Regex.Replace(newText, @"\[/b\]", "</b>", RegexOptions.IgnoreCase);
            newText = Regex.Replace(newText, @"\[i\]", "<i>", RegexOptions.IgnoreCase);
            newText = Regex.Replace(newText, @"\[/i\]", "</i>", RegexOptions.IgnoreCase);
            newText = Regex.Replace(newText, @"\[u\]", "<u>", RegexOptions.IgnoreCase);
            newText = Regex.Replace(newText, @"\[/u\]", "</u>", RegexOptions.IgnoreCase);

            // return result
            return newText;
        }

        // Convert certain HTML tags back to BBCODE
        public string ConvertHtmlToBBCode(string text)
        {
            string newText = text;

            // replace allowed BBCode tags with html
            newText = newText.Replace("<br>", "[br]");
            newText = newText.Replace("<br><br>", "[br]");
            newText = newText.Replace("<b>", "[b]");
            newText = newText.Replace("</b>", "[/b]");
            newText = newText.Replace("<i>", "[i]");
            newText = newText.Replace("</i>", "[/i]");
            newText = newText.Replace("<u>", "[u]");
            newText = newText.Replace("</u>", "[/u]");

            // return result
            return newText;
        }


        // Check if any entries even exist
        public bool DoEntriesExistByUserId(int userId)
        {
            if (userId < 1)
            {
                return false;
            }

            List<EntryDto> entryDtos = entryRepository.GetEntriesByUserId(userId);

            if (entryDtos == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool DoEntriesExistByDate(int userId, DateTime dateAndTime)
        {
            if (dateAndTime == null || userId < 1)
            {
                return false;
            }

            List<EntryDto> entryDtos = entryRepository.GetEntriesByDate(userId, dateAndTime);

            if (entryDtos == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool DoEntriesExistByDateRange(int userId, DateTime dateAndTimeMin, DateTime dateAndTimeMax)
        {
            if (dateAndTimeMin == null || dateAndTimeMax == null || userId < 1)
            {
                return false;
            }

            List<EntryDto> entryDtos = entryRepository.GetEntriesByDateRange(userId, dateAndTimeMin, dateAndTimeMax);

            if (entryDtos == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
