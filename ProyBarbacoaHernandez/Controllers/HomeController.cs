using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProyBarbacoaHernandez.Areas.Principal.Controllers;
using ProyBarbacoaHernandez.Library;
using ProyBarbacoaHernandez.Models;

namespace ProyBarbacoaHernandez.Controllers
{
    public class HomeController : Controller
    {
        IServiceProvider _serviceProvider;

        private Usuarios _usuarios;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _usuarios = new Usuarios( userManager, signInManager, roleManager);
            //_serviceProvider = serviceProvider;
            // ejecutarTareaAsync();
        }

        public async Task<IActionResult> Index()
        {
            await CreateRolesAsync(_serviceProvider);
            return View();
        }
        [HttpPost]
        [AllowAnonymous] 
        public async Task<IActionResult> Index(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                List<object[]> listObject = await _usuarios.userLogin(model.input.Email, model.input.Password);
                object[] objects = listObject[0];
                var _identityError = (IdentityError)objects[0];
                model.ErrorMessage = _identityError.Description;
                if (model.ErrorMessage.Equals("True"))
                {
                    var data = JsonConvert.SerializeObject(objects[1]);
                    return RedirectToAction(nameof(PrincipalController.Index), "Principal");
                }
                else
                {
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            String mensaje;
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                String[] rolesName = { "Admin", "User" };
                foreach (var item in rolesName)
                {
                    var roleExist = await roleManager.RoleExistsAsync(item);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item));
                    }
                }
                var user = await userManager.FindByIdAsync("75ba7e9c-85cf-408a-8147-688a2653d22f");
                await userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {

                mensaje = ex.Message;
            }
        }
        private  async  Task ejecutarTareaAsync()
        {
            var data = await Tareas();
            String tarea = "Ahora ejecutaremos las demas lineas de codigo";
        }
        private async Task<String> Tareas()
        {
            Thread.Sleep(20 * 1000);
            String tarea = "Tarea finalizada";
            return tarea;
        }
    }
}
