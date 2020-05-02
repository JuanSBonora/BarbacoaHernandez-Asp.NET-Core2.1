using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyBarbacoaHernandez.Areas.Usuarios.Models;
using ProyBarbacoaHernandez.Library;

namespace ProyBarbacoaHernandez.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
        private ListObject objeto = new ListObject();
        //private LUsuarios _usuarios;
        public RegistrarModel(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager, IHostingEnvironment environment) {
            objeto._roleManager = roleManager;
            objeto._userManager = userManager;
            objeto._environment = environment;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UsersRoles();
            objeto._image = new Uploadimage();
            objeto._userRoles = new List<SelectListItem>();
    }
        public void OnGet() {
            Input = new InputModel
            {
                rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
            };
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar {
            [Required]
            public string Role { get; set; }
            [TempData]
            public string ErrorMessage { get; set; }
            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await guardarAsync();
            return Page();
        }
        private async Task guardarAsync()
        {
            try
            {
                objeto._userRoles.Add(new SelectListItem
                {
                    Text = Input.Role
                });
                var userList = objeto._userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count.Equals(0))
                {

                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = "El " + Input.Email + " ya esta registrado",
                        rolesLista = objeto._userRoles
                    };
                }
                var imageName = Input.Email + ".png";
                await objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios");
            }
            catch (Exception ex)
            {
                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    rolesLista = objeto._userRoles
                };
            }
        }
    }
}
