using Microsoft.AspNetCore.Identity;
using ProyBarbacoaHernandez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyBarbacoaHernandez.Library
{
    public class Usuarios : ListObject
    {
        public Usuarios()
        {

        }
        public Usuarios(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _usersRole = new UsersRoles();
        }
        public Usuarios(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _usersRole = new UsersRoles();
        }
        internal async Task<List<object[]>> userLogin(string email, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.Where(u => u.Email.Equals(email)).ToList();
                    _userRoles = await _usersRole.getRole(_userManager, _roleManager, appUser[0].Id);
                    _userData = new UserData
                    {
                        Id = appUser[0].Id,
                        Role = _userRoles[0].Text,
                        UserName = appUser[0].UserName
                    };
                    code = "0";
                    description = result.Succeeded.ToString();
                }
                else
                {
                    code = "1";
                    description = "incorrect e-mail address or password";
                }
            }
            catch (Exception ex)
            {
                code = "2";
                description = ex.Message;
            }
            _identityError = new IdentityError
            {
                Code = code,
                Description = description
            };
            object[] data = { _identityError, _userData };
            dataList.Add(data);
            return dataList;
        }
    }
}
