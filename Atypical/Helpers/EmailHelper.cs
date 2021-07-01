using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Atypical.Crosscutting.Dtos.User;
using System.Threading.Tasks;
using System.Text;

namespace Atypical.Web.Helpers
{
    // Email a message to someone
    public static class EmailHelper
    {
        private static string senderName = "Atypical Community";
        private static string senderEmail = "bot.atypical@gmail.com";

        private static string senderPassword = "t3chN0L0g!c";

        private static int portNumber = 587;
        private static string smtpHost = "smtp.gmail.com";


        //// General - send a message - can use html in message
        //public static async Task<bool> SendEmail(UserDto receiver, string subject, string message)
        //{

        //    // if anything is null, return false
        //    if (receiver == null || receiver.Email == null || subject == null || message == null)
        //    {
        //        return false;
        //    }

        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("email", "password"),
        //        EnableSsl = true,
        //    };

        //    // create email
        //    MailMessage email = CreateEmail(receiver, subject, message);

        //    smtpClient.Send(email);



        //    //// verify credentials and send email
        //    //using (var smtp = new SmtpClient())
        //    //{
        //    //    var credentials = new NetworkCredential
        //    //    {
        //    //        UserName = senderEmail,
        //    //        Password = senderPassword

        //    //    };

        //    //    smtp.Credentials = credentials;
        //    //    smtp.Host = "smtp.gmail.com";
        //    //    smtp.Port = 587;
        //    //    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //    //smtp.EnableSsl = true;
        //    //    smtp.EnableSsl = true;



        //    try
        //    {
        //        smtp.Send(email);
        //    }
        //    catch (SmtpFailedRecipientsException ex)
        //    {
        //        for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //        {
        //            SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //            if (status == SmtpStatusCode.MailboxBusy ||
        //                status == SmtpStatusCode.MailboxUnavailable)
        //            {
        //                Console.WriteLine("Delivery failed - retrying in 5 seconds.");
        //                System.Threading.Thread.Sleep(5000);
        //                smtp.Send(email);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to deliver message to {0}",
        //                    ex.InnerExceptions[i].FailedRecipient);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
        //                ex.ToString());

        //        return false;
        //    }

        //    // return true after this is done
        //    return true;

        //}

        //General - send a message - can use html in message
        public static async Task<bool> SendEmail(UserDto receiver, string subject, string message)
        {

            // if anything is null, return false
            if (receiver == null || receiver.Email == null || subject == null || message == null)
            {
                return false;
            }

            // create email
            MailMessage email = CreateEmail(receiver, subject, message);


            //create and set up the smtp client
            using (var smtp = GetSmtpClient())
            {
               // NetworkCredential credentials =
               //new NetworkCredential(senderEmail, senderPassword);
               // smtp.Credentials = credentials;
               // smtp.Host = smtpHost;
               // smtp.Port = portNumber;
               // smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               // smtp.EnableSsl = true;
               // smtp.UseDefaultCredentials = false; // may be able to use true

                //// attempt to send email
                //try
                //{
                //    await smtp.SendMailAsync(email);
                //    return true;
                //}
                //catch (Exception)
                //{
                //    Console.WriteLine("Delivery failed.");
                //    return false;
                //}

                try
                {
                    await smtp.SendMailAsync(email);
                    // return true after this is done
                    return true;
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                            System.Threading.Thread.Sleep(5000);
                            await smtp.SendMailAsync(email);

                        }
                        else
                        {
                            Console.WriteLine("Failed to deliver message to {0}",
                                ex.InnerExceptions[i].FailedRecipient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                            ex.ToString());

                    return false;
                }

                return false;


            }

        }

        public static SmtpClient GetSmtpClient()
        {
            // create
            var smtp = new SmtpClient();
            smtp.Credentials = 
                new NetworkCredential(senderEmail, senderPassword);
            //smtp.Host = smtpHost;
            //smtp.Port = portNumber;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false; // may be able to use true

            // return
            return smtp;
        }


        //public static bool SendEmail(UserDto receiver, string subject, string message)
        //{

        //    // if anything is null, return false
        //    if (receiver == null || receiver.Email == null || subject == null || message == null)
        //    {
        //        return false;
        //    }

        //    // create email
        //    MailMessage email = CreateEmail(receiver, subject, message);


        //    //create and set up the smtp client
        //    using (var smtp = new SmtpClient())
        //    {
        //        NetworkCredential credentials =
        //       new NetworkCredential(senderEmail, senderPassword);
        //        smtp.Credentials = credentials;
        //        smtp.Host = smtpHost;
        //        smtp.Port = portNumber;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = false; // may be able to use true

        //        //// attempt to send email
        //        //try
        //        //{
        //        //    await smtp.SendMailAsync(email);
        //        //    return true;
        //        //}
        //        //catch (Exception)
        //        //{
        //        //    Console.WriteLine("Delivery failed.");
        //        //    return false;
        //        //}

        //        try
        //        {
        //            smtp.Send(email);
        //            // return true after this is done
        //            return true;
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            {
        //                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //                if (status == SmtpStatusCode.MailboxBusy ||
        //                    status == SmtpStatusCode.MailboxUnavailable)
        //                {
        //                    Console.WriteLine("Delivery failed - retrying in 5 seconds.");
        //                    System.Threading.Thread.Sleep(5000);
        //                    smtp.Send(email);

        //                }
        //                else
        //                {
        //                    Console.WriteLine("Failed to deliver message to {0}",
        //                        ex.InnerExceptions[i].FailedRecipient);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
        //                    ex.ToString());

        //            return false;
        //        }

        //        return false;


        //    }

        //}


        //// General - send a message - can use html in message
        //public static bool SendEmail(UserDto receiver, string subject, string message)
        //{

        //    // if anything is null, return false
        //    if (receiver == null || receiver.Email == null || subject == null || message == null)
        //    {
        //        return false;
        //    }

        //    // create email
        //    MailMessage email = CreateEmail(receiver, subject, message);


        //    //create and set up the smtp client
        //    using (var smtp = new SmtpClient())
        //    {
        //        NetworkCredential credentials =
        //       new NetworkCredential(senderEmail, senderPassword);
        //        smtp.Credentials = credentials;
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.EnableSsl = true;
        //        smtp.EnableSsl = false;
        //        smtp.UseDefaultCredentials = false; // may be able to use true

        //        // attempt to send email
        //        try
        //        {
        //            smtp.Send(email);
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine("Delivery failed.");
        //            return false;
        //        }

        //    }

        //}


        public static MailMessage CreateEmail(UserDto receiver, string subject, string message)
        {
            // start message
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var email = new MailMessage();

            // create email
            email.To.Add(new MailAddress(receiver.Email));

            email.From = new MailAddress(senderEmail);

            email.Subject = subject;
            email.Body = string.Format(body, senderName, senderEmail, message);
            email.BodyEncoding = Encoding.UTF8;
            email.IsBodyHtml = true;

            // return email
            return email;
        }


    




        //// General - send a message - can use html in message
        //public static async Task<bool> SendEmail(UserDto receiver, string subject, string message)
        //{

        //    // if anything is null, return false
        //    if (receiver == null || receiver.Email == null || subject == null || message == null)
        //    {
        //        return false;
        //    }

        //    // start message
        //    var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //    var email = new MailMessage();

        //    // create email
        //    email.To.Add(new MailAddress(receiver.Email));

        //    email.From = new MailAddress(senderEmail);

        //    email.Subject = subject;
        //    email.Body = string.Format(body, senderName, senderEmail, message);
        //    email.IsBodyHtml = true;

        //    // verify credentials and send email
        //    using (var smtp = new SmtpClient())
        //    {
        //        var credentials = new NetworkCredential
        //        {
        //            UserName = senderEmail,
        //            Password = senderPassword

        //        };

        //        smtp.Credentials = credentials;
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        //smtp.EnableSsl = true;
        //        smtp.EnableSsl = false;



        //        try
        //        {
        //            smtp.Send(email);
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            {
        //                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //                if (status == SmtpStatusCode.MailboxBusy ||
        //                    status == SmtpStatusCode.MailboxUnavailable)
        //                {
        //                    Console.WriteLine("Delivery failed - retrying in 5 seconds.");
        //                    System.Threading.Thread.Sleep(5000);
        //                    smtp.Send(email);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Failed to deliver message to {0}",
        //                        ex.InnerExceptions[i].FailedRecipient);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
        //                    ex.ToString());

        //            return false;
        //        }

        //        // return true after this is done
        //        return true;

        //    }

        //}



        // Password reset email
        public static bool SendResetCode(UserDto receiver, string code)
        {
            // if there's not enough info to send the email, return false
            if (receiver == null || receiver.Email == null || code == null)
            {
                return false;
            }

            // create message to send
            string title = "Password Reset Code";
            string message = $"Test send!<br><br>Code: {code}";

            // send the message, store result as a bool
            Task<bool> successful = SendEmail(receiver, title, message);
            //bool successful = SendEmail(receiver, title, message);

            // return result of sendEmail
            return successful.IsCompleted;
            //return successful;
        }

    }
}