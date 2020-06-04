using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyBarbacoaHernandez.Areas.Usuarios.Models;
using ProyBarbacoaHernandez.Controllers;
using ProyBarbacoaHernandez.Data;
using ProyBarbacoaHernandez.Library;
using ProyBarbacoaHernandez.Models;

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
        public async Task<IActionResult> Index(int id, String Search)
        {
            if (objeto._signInManager.IsSignedIn(User))
            {
                var url = Request.Scheme + "://" + Request.Host.Value;
                var objects = new Paginador<InputModelRegistrar>().paginador(await objeto._usuarios.getTUsuariosAsync(Search), id, "Usuarios", "Usuarios", "Index", url);

                var models = new DataPaginador<InputModelRegistrar>
                {
                    List = (List<InputModelRegistrar>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new InputModelRegistrar()
                };
                
                return View(models);
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