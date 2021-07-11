using Atypical.Crosscutting.Dtos.User;
using Atypical.Crosscutting.Enums;
using Atypical.Domain.Orchestrators.User;
using Atypical.Web.Helpers;
using Atypical.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace Atypical.Controllers
{
    // TODO Allow user to edit their details
    // TODO Add admin privledges for admins
    // TODO Allow account suspensions and freezes
    // TODO Add user activity logs
    // TODO Add admin activity logs

    public class UserController : Controller
    {

        private UserOrchestrator userOrchestrator = new UserOrchestrator();


        public ActionResult Login(string message)
        {
            // if there is a message passed, set it to the viewbag
            if (message != null)
            {
                ViewBag.Message = message;
            }

            //first check if the session has a user - if it does, go to home page
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new UserLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model)
        {
            // validate model
            if (ModelState.IsValid)
            {
                // check if the email exists that is in the model
                if (userOrchestrator.DoesUserExistByEmail(model.Email))
                {
                    // get the user id
                    int userId = userOrchestrator.GetUserIdByEmail(model.Email);

                    // grab the user from the orchestrator
                    UserDto userDto = userOrchestrator.GetUserById(userId);

                    // if the user is null, redirect back to the login page
                    if (userDto == null)
                    {
                        return View(model);
                    }

                    // now make sure that the password matches the one in the database
                    // turn the password that has been entered into a secure password
                    string password = userOrchestrator.SecurePassword(model.Password);

                    if (password == userDto.Password)
                    {
                        // Make this the user in the session
                        Session["username"] = userDto.Username;
                        Session["userId"] = userDto.Id;

                        // Redirect to new journal entry
                        return RedirectToAction("View", "Entry");

                    }

                }
            }

            ViewBag.IncorrectLogin = "Incorrect email or password.";

            // if it doesn't exist or isn't valid, redirect back to the login page
            return View(model);
        }

        public ActionResult Logout()
        {
            // set the session username back to null
            Session["username"] = null;
            Session["userId"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            //first check if the session has a user - if it does, go to home page
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model, HttpPostedFileBase ProfileImageFile)
        {
            // validate model
            if (ModelState.IsValid)
            {
                // make sure the user doesn't already exist
                bool doesUserExist = userOrchestrator.DoesUserExistByUsername(model.Username);

                if (doesUserExist == true)
                {
                    ModelState.AddModelError(string.Empty, "User account already exists.");
                }
                else // otherwise create the user account
                {

                    // Use hash to add security to the password
                    string hashPassword = userOrchestrator.SecurePassword(model.Password);
                    // set the model password to this hashpassword
                    model.Password = hashPassword;

                    // try to upload the filepath
                    bool successful = UserHelper.SaveImage(ProfileImageFile);

                    // if the file's filename is null, set it to "default.png"
                    if (!successful)
                    {
                        model.ProfileImageUrl = "/Images/default.png";
                    }
                    else
                    {
                        model.ProfileImageUrl = "/Images/" + ProfileImageFile.FileName;
                    }

                    UserDto newUser = UserHelper.ConvertUserModelToDto(model);

                    // HACK for now, just make email confirmed until we can get email sending working
                    //newUser.IsEmailConfirmed = true;

                    // validate success
                    bool successfullyCreatedUser = userOrchestrator.CreateUser(newUser);

                    if (successfullyCreatedUser == true)
                    {

                        Session["username"] = newUser.Username;
                        Session["userId"] = userOrchestrator.GetUserIdByEmail(newUser.Email);

                        // send email confirmation
                        EmailHelper.SendConfirmationEmail(newUser.Email, (int)Session["userId"]);

                        // TODO Find a way to alert the user that their account was created successfully

                        return RedirectToAction("New", "Entry");
                    }
                    else // otherwise create an error
                    {
                        ModelState.AddModelError(string.Empty, "New user could not be created.");
                    }

                }

            }

            // If the model view wasn't valid, return them to the create page
            return View(model);
        }

        public ActionResult SendConfirmation(int? id)
        {
            if (id != null)
            {
                // try to grab user
                UserDto user = userOrchestrator.GetUserById((int)id);

                if (user != null)
                {
                    // send email confirmation
                    EmailHelper.SendConfirmationEmail(user.Email, (int)Session["userId"]);
                }
            }

            // now redirect back
            return RedirectToAction("ConfirmEmail", "Error");

        }


        // Account confirmation and password recovery

        public ActionResult ForgotPassword()
        {
            //first check if the session has a user - if it does, go to home page
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Action = "send-email";
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            // validate model
            if (ModelState.IsValid)
            {

                //grab the user by their email
                UserDto user = userOrchestrator.GetUserByEmail(model.Email);

                // if the user is null, return to view the same way - this shouldn't occur
                if (user == null)
                {
                    ViewBag.Action = "send-email";
                }
                else
                {
                    ViewBag.Action = "enter-code";
                }
            }

            
            return View(model);
        }

        public ActionResult ConfirmEmail(int? id)
        {
            // try to grab the user
            UserDto user = userOrchestrator.GetUserById((int)id);

            // if it's null or user isn't logged in, redirect to home page
            // if the id doesn't match the session, redirect, too
            if (id == null || user == null || Session["username"] == null
                || Session["userId"] == null ||
                (int)Session["userId"] != id || user.IsEmailConfirmed == true)
            {

                return RedirectToAction("Index", "Home");
            }

            // otherwise, confirm their email and update the user
            user.IsEmailConfirmed = true;
            userOrchestrator.UpdateUser(user);

            // direct them to a page saying their email was confirmed
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendCode(ForgotPasswordViewModel model)
        {

            // create a verification code
            string code = EmailHelper.GenerateCode(10); // TODO turn to real code generated

            // get the user to send the email
            UserDto user = userOrchestrator.GetUserByEmail(model.Email);

            if (user != null)
            {
                // Send a message to their email with the verification code
                bool sendSuccessful = EmailHelper.SendResetCode(user.Email, code);

                // if it worked
                if (sendSuccessful)
                {
                    // put the code into the session
                    Session["resetCode"] = code;
                    Session["codeEmail"] = model.Email;

                    return RedirectToAction("CheckCode", "User");
                }

            }
            // otherwise, return to login and let user know there was an error
            return RedirectToAction("Login", "User", new { message = "Could not send email." });

        }

        


        public ActionResult CheckCode()
        {
            // if the session properties are null, redirect to login
            if (Session["codeEmail"].ToString() == null ||
                Session["resetCode"].ToString() == null)
            {
                return RedirectToAction("Login", "User", new { message = "Something weird happened." });
            }

            // set up the model to check code
            CheckCodeViewModel model = new CheckCodeViewModel()
            {
                Email = Session["codeEmail"].ToString(),
                CodeForReset = Session["resetCode"].ToString(),
                CodeEntered = ""
            };

            

            return View(model);

        }

        [HttpPost]
        public ActionResult CheckCode(CheckCodeViewModel model)
        {
            // check that the model is valid
            if (ModelState.IsValid)
            {
                // if the code is correct, redirect user to a page to set a new password
                if (model.CodeForReset == model.CodeEntered)
                {
                    return RedirectToAction("NewPassword", "User");
                }

            }
            // if model state is not valid, set error message before returning
            ViewBag.ErrorMessage = "Error: something went wrong.";

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPassword(CheckCodeViewModel model)
        {
            // Set up a view model so the user can reset their password

            //grab the user by their email
            UserDto user = userOrchestrator.GetUserByEmail(model.Email);

            // if the user is null for some reason, redirect to login page
            if (user == null)
            {
                return RedirectToAction("Login", "User", new { message = "Something weird happened." });
            }

            // otherwise add the user to a password model to give the view
            NewPasswordViewModel passwordModel = new NewPasswordViewModel()
            {
                User = user
            };

            // HACK Set session email again to reaffirm - having issues passing user in the model
            Session["codeEmail"] = model.Email;

            return View(passwordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassword(NewPasswordViewModel model)
        {
            // HACK Having issues passing the user in the model through a post form, so grabbing user info from session
            if (model.User == null)
            {
                model.User = userOrchestrator.GetUserByEmail(Session["codeEmail"].ToString());
            }
            
            // validate model
            if (ModelState.IsValid)
            {
                // set the new password for the user
                model.User.Password = model.NewPassword;

                // now change the information in the database
                UserDto updatedUser = new UserDto()
                {
                    Id = model.User.Id,
                    Username = model.User.Username,
                    FirstName = model.User.FirstName,
                    ProfileImageUrl = model.User.ProfileImageUrl,
                    DateOfBirth = model.User.DateOfBirth,
                    Email = model.User.Email,
                    Password = userOrchestrator.SecurePassword(model.User.Password), // add secure password to database
                    IsEmailConfirmed = model.User.IsEmailConfirmed
                };

                // validate success
                bool successfullyUpdatedUser = userOrchestrator.UpdateUser(updatedUser);

                if (successfullyUpdatedUser == true)
                {

                    // Redirect to login if successful and tell them it was successful

                    return RedirectToAction("Login", "User", new { message = "Password reset successfully!" });
                }

                return RedirectToAction("Login", "User", new { message = "Error: Empty DTO" });
            }

            return RedirectToAction("Login", "User", new { message = "Error: Could not reset password" });

        }


        



        // Validation for view model

        // See if the code needed to reset the password matches what is required
        public JsonResult CodeMatches(CheckCodeViewModel model)
        {
            if (model == null || model.CodeForReset == null ||
                model.CodeEntered == null || model.CodeEntered != model.CodeForReset)
            {
                return Json(true); //the opposite of what's expected
            }
            return Json(false);
        }


        public JsonResult UsernameExists(string username)
        {
            // grab list of users from repo
            List<UserDto> users = userOrchestrator.GetAllUsers();

            // check if it exists
            bool doesExist = users.Any(u => u.Username.ToLower() == username.ToLower());

            // return whether the username already exists or not
            return Json(!doesExist);
        }

        public JsonResult EmailExists(string email)
        {
            // grab list of users from repo
            List<UserDto> users = userOrchestrator.GetAllUsers();

            // check if it exists
            bool doesExist = users.Any(u => u.Email.ToLower() == email.ToLower());

            // return whether the username already exists or not
            return Json(!doesExist);
        }

        public JsonResult EmailDoesNotExist(string email)
        {
            // grab list of users from repo
            List<UserDto> users = userOrchestrator.GetAllUsers();

            // check if it exists
            bool doesExist = users.Any(u => u.Email.ToLower() == email.ToLower());

            // return whether the username already exists or not
            return Json(doesExist);
        }

        public JsonResult PasswordConfirmed(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return Json(false); // return the opposite of what is expected to work right
            }

            return Json(true);
        }

    }
}