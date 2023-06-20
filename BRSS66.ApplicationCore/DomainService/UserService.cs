using System.ComponentModel.DataAnnotations;
using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Mapper;
using BRSS66.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;

namespace BRSS66.ApplicationCore.DomainService;

public class UserService : IUserService
{
    
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IUserRepository _userRepo;
    private readonly UserManager<AppUser> _userManager;
    
    public UserService(RoleManager<AppRole> roleManager, IUserRepository userRepo, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userRepo = userRepo;
        _userManager = userManager;
    }
    
    public Task<PagedResponse<UserInformation>> GetUsers(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
    {
        var query = _userRepo.Query(includeProperties: "Identity");
        if (!string.IsNullOrEmpty(textSearch))
        {
            query = query.Where(m =>
                m.FirstName.Contains(textSearch) ||
                m.LastName.Contains(textSearch) ||
                m.Identity!.Email!.Contains(textSearch)
            ).OrderByDescending(c => c.Id);
        }
        var totalItems = query.Count();

        if (!string.IsNullOrEmpty(sortColumnName))
        {
            query = query.OrderBy(s => s.FirstName);  //Fix here
        }

        var items = query.Skip(currentPage * pageSize).Take(pageSize).Select(m => m.MapToUserInformation()).ToList();
        return Task.FromResult(new PagedResponse<UserInformation>()
        {
            Page = currentPage,
            Limit = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            Items = items
        });
    }

    public async Task<UserInformation> GetUserById(int id)
    {
        var user = await _userRepo.Get(id, includeProperties: "Identity");
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var userModel = new UserInformation();
        AppUser appUser = (await _userManager.FindByIdAsync(user.IdentityId.ToString()))!;
        userModel = user.MapToUserInformation();
        var roles = await _userManager.GetRolesAsync(appUser);
        if (roles != null)
        {
            userModel.Roles = new List<string>(roles).ToArray();
        }
        return userModel;
    }

    public async Task CreateUser(UserInformation model)
    {
        if (model.Roles!.Length == 0)
        {
            throw new Exception("Roles is required");
        }
        if (_userManager.FindByEmailAsync(model.Email!).Result != null)
        {
            throw new Exception("Duplicate Email");
        }
        var email = new EmailAddressAttribute();
        if (!email.IsValid(model.Email))
        {
            throw new Exception("Invalid email");
        }

        AppUser appUser = model.MapToAppUser();
        var pwd = new Password(8);
        var password = pwd.Next();
        var result = await _userManager.CreateAsync(appUser, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(appUser, model.Roles);
            User user = model.MapToUser();
            user.Identity = appUser;
            user.IdentityId = appUser.Id;
            await _userRepo.Add(user);
        }
        else
        {
            string errorMessages = "";
            foreach (var it in result.Errors)
            {
                errorMessages += it.Description + "\n";
            }
            throw new Exception(errorMessages);
        }
    }

    public async Task UpdateUser(int id, UserInformation model)
    {
           var user = await _userRepo.Get(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var appUser = await _userManager.FindByIdAsync(user.IdentityId.ToString());
            if (appUser == null)
            {
                throw new Exception("User not found");
            }
            if (!string.IsNullOrEmpty(model.Email) && model.Email != appUser.Email)
            {
                var emailExisted = await _userRepo.Get(u => u.Identity!.Email!.ToUpper() == model.Email.ToUpper(), "Identity");
                {
                    if (emailExisted != null)
                    {
                        throw new Exception("Email already exists");
                    }
                    appUser.Email = model.Email;
                    appUser.UserName = model.Email;
                }
            }
            if (!string.IsNullOrEmpty(model.FirstName) && model.FirstName != user.FirstName)
            {
                user.FirstName = model.FirstName;
                user.FullName = user.FirstName + " " + user.LastName;
            }
            if (!string.IsNullOrEmpty(model.LastName) && model.LastName != user.LastName)
            {
                user.LastName = model.LastName;
                user.FullName = user.FirstName + " " + user.LastName;
            }
            if (!string.IsNullOrEmpty(model.PhoneNumber) && model.PhoneNumber != appUser.PhoneNumber)
            {
                appUser.PhoneNumber = model.PhoneNumber;
            }
            if (model.Roles!.Length > 0)
            {
                foreach (var role in model.Roles)
                {
                    var roleExisted = await _roleManager.RoleExistsAsync(role);
                    if (roleExisted)
                    {
                        var roles = await _userManager.GetRolesAsync(user.Identity!);
                        if (!roles.SequenceEqual(model.Roles))
                        {
                            await _userManager.RemoveFromRolesAsync(appUser, roles);
                            await _userManager.AddToRolesAsync(appUser, model.Roles);
                        }
                    }
                }
            }
            var result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {
                await _userRepo.Update(user);
            }
            else
            {
                string errorMessages = "";
                foreach (var it in result.Errors)
                {
                    errorMessages += it.Description + "\n";
                }
                throw new InvalidOperationException(errorMessages);
            }
    }

    public async Task ActiveUser(int id)
    {
        var user = await _userRepo.Get(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.IsActive = true;
        await _userRepo.Update(user);
    }

    public async Task InactiveUser(int id)
    {
        var user = await _userRepo.Get(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.IsActive = false;
        await _userRepo.Update(user);
    }
}