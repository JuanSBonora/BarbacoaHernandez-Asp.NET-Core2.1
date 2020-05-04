using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyBarbacoaHernandez.Controllers;
using ProyBarbacoaHernandez.Data;
using ProyBarbacoaHernandez.Library;

namespace ProyBarbacoaHernandez.Areas.Usuarios.Controllers
{
    [Authorize]
    [Area("Usuarios")]
    public class UsuariosController : Controller
    {
        private ListObject objeto = new ListObject();
        public UsuariosController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            objeto._signInManager = signInManager;
            objeto._usuarios = new LUsuarios(userManager, signInManager, roleManager, context);
        }
        public async Task<IActionResult> Index()
        {
            if (objeto._signInManager.IsSignedIn(User))
            {
                //var data = User.Claims.FirstOrDefault(u => u.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Value;
                //ViewData["Roles"] = _usuarios.userData(HttpContext);
                var model = await objeto._usuarios.getTUsuariosAsync();
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        public async Task<IActionResult> SessionClose()
        {
            HttpContext.Session.Remove("User");
            await objeto._signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}