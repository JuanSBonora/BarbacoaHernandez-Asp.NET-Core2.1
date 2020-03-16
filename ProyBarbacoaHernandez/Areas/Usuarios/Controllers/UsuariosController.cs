using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyBarbacoaHernandez.Controllers;

namespace ProyBarbacoaHernandez.Areas.Usuarios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public UsuariosController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SessionClose()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}