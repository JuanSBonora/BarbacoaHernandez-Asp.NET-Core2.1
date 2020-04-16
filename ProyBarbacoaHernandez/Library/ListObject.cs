using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyBarbacoaHernandez.Data;
using ProyBarbacoaHernandez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyBarbacoaHernandez.Library
{
    public class ListObject
    {
        public String description, code; 

        public UsersRoles _usersRole;
        public UserData _userData;
        public LUsuarios _usuarios;
        public IdentityError _identityError;
        public ApplicationDbContext _context;
        public IHostingEnvironment _environment; 


        public List<SelectListItem> _userRoles;

        public RoleManager<IdentityRole> _roleManager;
        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;

        public List<object[]> dataList = new List<object[]>();
    }
}
