using System.Web;
using System.IO;
using Atypical.Web.Models.User;
using Atypical.Crosscutting.Dtos.User;
using Atypical.Crosscutting.Enums;
using System;

namespace Atypical.Web.Helpers
{
    public static class UserHelper
    {
        // Returns true if successful
        public static bool SaveImage(HttpPostedFileBase file)
        {

            if (file != null)
            {
                FileInfo fileInfo = new FileInfo(file.FileName);
                string path = fileInfo.FullName;
                //string path = Path.Combine(System.Web.HttpContext.Current
                //    .Server.MapPath("~/Images"),
                //    Path.GetFileName(file.FileName) + "." + fileInfo.Extension);
                file.SaveAs(path);
                return true;
            }
            return false;
        }


        public static UserViewModel ConvertUserDtoToModel(UserDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            UserViewModel model = new UserViewModel()
            {
                Id = dto.Id,
                Username = dto.Username,
                FirstName = dto.FirstName,
                ProfileImageUrl = dto.ProfileImageUrl,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Password = dto.Password,
                IsEmailConfirmed = dto.IsEmailConfirmed,
                UserType = Enum.GetName(typeof(UserType), dto.UserType),
                AccountStatus = Enum.GetName(typeof(AccountStatus), dto.AccountStatus)
            };

            return model;

        }

        public static UserDto ConvertUserModelToDto(UserViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            UserDto dto = new UserDto()
            {
                Username = model.Username,
                FirstName = model.FirstName,
                ProfileImageUrl = model.ProfileImageUrl,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                Password = model.Password,
                IsEmailConfirmed = model.IsEmailConfirmed,
                UserType = (UserType)Enum.Parse(typeof(UserType), model.UserType),
                AccountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), model.AccountStatus)
            };

            return dto;

        }

    }
}