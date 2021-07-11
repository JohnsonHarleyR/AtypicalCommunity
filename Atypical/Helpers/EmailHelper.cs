using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Atypical.Crosscutting.Dtos.User;
using System.Threading.Tasks;
using System.Text;
using MimeKit;
using Atypical.Crosscutting.Enums;
using MailKit.Net.Smtp;
using System.Diagnostics;
using System.IO;

namespace Atypical.Web.Helpers
{
    // Email a message to someone
    public static class EmailHelper
    {
        private static Random random = new Random();

        private static string senderName = "Atypical Community";
        private static string senderEmail = "bot.atypical@gmail.com";

        private static string senderPassword = "t3chN0L0g!c";

        private static int portNumber = 587;
        private static string smtpHost = "smtp.gmail.com";


        // General - send a message - can use html in message
        public static bool SendEmail(string receiver, string subject, string message)
        {

            // if anything is null, return false
            if (receiver == null || subject == null || message == null)
            {
                return false;
            }

            // create email
            MimeMessage email = CreateEmail(receiver, subject, message);


            //create and set up the smtp client
            using (var smtp = new SmtpClient())
            {

                smtp.Connect(smtpHost, portNumber, false);
                smtp.Authenticate(senderEmail, senderPassword);

                // attempt to send email
                try
                {
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Delivery failed.");
                    return false;
                }

            }

        }


        public static MimeMessage CreateEmail(string receiverEmail, string subject, string message)
        {
            // start message
            var body = $"<p>{message}</p>";
            var email = new MimeMessage();

            // create email
            email.To.Add(MailboxAddress.Parse(receiverEmail));

            email.From.Add(MailboxAddress.Parse(senderEmail));

            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            // return email
            return email;
        }


        // Password reset email
        public static bool SendResetCode(string receiverEmail, string code)
        {
            // if there's not enough info to send the email, return false
            if (receiverEmail == null || code == null)
            {
                return false;
            }

            // create message to send
            string title = "Atypical: Password Reset";
            string message = $"<b>Code to reset your password:</b><h3>{code}</h3>";

            // send the message, store result as a bool
            //Task<bool> successful = SendEmail(receiverEmail, title, message);
            bool successful = SendEmail(receiverEmail, title, message);

            // return result of sendEmail
            //return successful.IsCompleted;
            return successful;
        }

        // Send confirmation email
        public static bool SendConfirmationEmail(string receiverEmail, int? userId)
        {
            // if there's not enough info to send the email, return false
            if (receiverEmail == null || userId == null)
            {
                return false;
            }

            // create message to send
            var link = $@"https://localhost:44398/User/ConfirmEmail?id={userId}";
            string title = "Atypical: Confirm Your Account";
            string message = $"<a href=\"{link}\">Click here to confirm your account." +
                "</a><br><i>(Be sure that you are logged in first.)</i>";

            // send the message, store result as a bool
            //Task<bool> successful = SendEmail(receiverEmail, title, message);
            bool successful = SendEmail(receiverEmail, title, message);

            // return result of sendEmail
            //return successful.IsCompleted;
            return successful;
        }

        // Generate a code to reset a person's password, with a given number of characters.
        public static string GenerateCode(int length)
        {
            // define characters for generator
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // define new string
            string code = "";

            // loop for the length given
            for (int i = 0; i < length; i++)
            {
                // determine which kind of character to give
                CharType type = (CharType)random.Next(0, 3);
                int choice;

                // add to string depending
                switch (type)
                {
                    case CharType.Number:
                        choice = random.Next(0, numbers.Length);
                        code += numbers[choice].ToString();
                        break;
                    case CharType.LowerCaseLetter:
                        choice = random.Next(0, alphabet.Length);
                        code += alphabet[choice].ToString().ToLower();
                        break;
                    default:
                    case CharType.UpperCaseLetter:
                        choice = random.Next(0, alphabet.Length);
                        code += alphabet[choice].ToString().ToUpper();
                        break;
                }

            }

            // return the code
            return code;

        }


    }
}