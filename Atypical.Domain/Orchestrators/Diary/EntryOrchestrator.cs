using System;
using System.Collections.Generic;
using Atypical.Data.Repositories.Diary;
using Atypical.Crosscutting.Dtos.Diary;
using System.Text.RegularExpressions;

namespace Atypical.Domain.Orchestrators.Diary
{
    public class EntryOrchestrator
    {
        private DiaryRepository DiaryRepository;

        public EntryOrchestrator()
        {
            DiaryRepository = new DiaryRepository();
        }


        // Create a new entry
        public bool CreateEntry(DiaryEntryDto entryDto)
        {
            // if the dto is null, return false
            if (entryDto == null)
            {
                return false;
            }

            // Add the dto to the repo
            DiaryRepository.AddEntry(entryDto);


            // return true that it was successful
            return true;

        }

        // Update a new entry
        public bool UpdateEntry(DiaryEntryDto entryDto)
        {
            // if the dto is null, return false
            if (entryDto == null)
            {
                return false;
            }

            // update the entry in the repo
            DiaryRepository.UpdateEntry(entryDto);


            // return true that it was successful
            return true;

        }


        // Methods to get entry or entries

        public DiaryEntryDto GetEntryById(int id)
        {
            DiaryEntryDto entryDto = DiaryRepository.GetEntryById(id);

            if (entryDto == null)
            {
                return null;
            }

            return entryDto;
        }

        public List<DiaryEntryDto> GetAllUserEntries(int userId)
        {
            List<DiaryEntryDto> entries = (List<DiaryEntryDto>)DiaryRepository.GetEntriesByUserId(userId);

            if (entries == null)
            {
                return null;
            }

            return entries;
        }

        public List<DiaryEntryDto> GetUserEntriesByDate(int userId, DateTime dateAndTime)
        {
            List<DiaryEntryDto> entries = (List<DiaryEntryDto>)DiaryRepository.GetEntriesByDate(userId, dateAndTime);

            if (entries == null)
            {
                return null;
            }

            return entries;
        }

        public List<DiaryEntryDto> GetUserEntriesByDateRange(int userId, DateTime dateAndTimeMin, DateTime dateAndTimeMax)
        {
            List<DiaryEntryDto> entries = (List<DiaryEntryDto>)DiaryRepository.GetEntriesByDateRange(userId, dateAndTimeMin, dateAndTimeMax);

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

            List<DiaryEntryDto> entryDtos = (List<DiaryEntryDto>)DiaryRepository.GetEntriesByUserId(userId);

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

            List<DiaryEntryDto> entryDtos = (List<DiaryEntryDto>)DiaryRepository.GetEntriesByDate(userId, dateAndTime);

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

            List<DiaryEntryDto> entryDtos = (List<DiaryEntryDto>)DiaryRepository.GetEntriesByDateRange(userId, dateAndTimeMin, dateAndTimeMax);

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
