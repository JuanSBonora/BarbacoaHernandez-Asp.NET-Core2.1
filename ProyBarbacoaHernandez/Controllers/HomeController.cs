﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProyBarbacoaHernandez.Areas.Principal.Controllers;
using ProyBarbacoaHernandez.Data;
using ProyBarbacoaHernandez.Library;
using ProyBarbacoaHernandez.Models;

namespace ProyBarbacoaHernandez.Controllers
{
    public class HomeController : Controller
    {
        IServiceProvider _serviceProvider;

        private LUsuarios _usuarios;
        private SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
               ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _usuarios = new LUsuarios( userManager, signInManager, roleManager, context);
            //_serviceProvider = serviceProvider;
            // ejecutarTareaAsync();
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(PrincipalController.Index), "Principal");
            }
            else
            {
                return View();
            }
            await CreateRolesAsync(_serviceProvider);
            
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
                    //var data = JsonConvert.SerializeObject(objects[1]);
                    //HttpContext.Session.SetString("User",data);
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
                //Cambiar Usuario para evitar error: index.(Login)
                //Proporcionar usuario de la tabla Tusuarios & Users
                // jfsb@gmail.com => Juan.123 (Not working)
                var user = await userManager.FindByIdAsync("83d37e4d-50f6-49b5-87b4-eb17e75e9856");
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
