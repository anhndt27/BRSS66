using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels;

namespace BRSS66.ApplicationCore.Mapper;

public static class UserMapper
{
    public static User MapToUser(this UserInformation model)
    {
        User user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FullName = model.FirstName + " " + model.LastName,
            IsActive = model.IsActive,
        };
        return user;
    }

    public static AppUser MapToAppUser(this UserInformation userModel)
    {
        AppUser appUser = new AppUser
        {
            UserName = userModel.Email,
            Email = userModel.Email,
            EmailConfirmed = true,
            PhoneNumber = userModel.PhoneNumber
        };
        return appUser;
    }

    public static UserInformation MapToUserInformation(this User user)
    {
        UserInformation userInformation = new UserInformation
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Identity!.Email,
            PhoneNumber = user.Identity.PhoneNumber!,
            IsActive = user.IsActive
        };
        return userInformation;
    }
   
}