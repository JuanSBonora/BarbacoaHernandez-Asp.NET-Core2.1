using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyBarbacoaHernandez.Controllers;
using ProyBarbacoaHernandez.Library;

namespace ProyBarbacoaHernandez.Areas.Principal.Controllers
{
    [Authorize]
    [Area("Principal")]
    public class PrincipalController : Controller
    {
        private LUsuarios _usuarios;
        private SignInManager<IdentityUser> _signInManager;
        public PrincipalController(SignInManager<IdentityUser> signInManager){
            _signInManager = signInManager;
            _usuarios = new LUsuarios();
        }
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //ViewData["Roles"] = _usuarios.userData(HttpContext);
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}