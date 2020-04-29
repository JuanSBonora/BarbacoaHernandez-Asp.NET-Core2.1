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
        public RegistrarModel(RoleManager<IdentityRole> roleManager, IHostingEnvironment environment) {
            objeto._roleManager = roleManager;
            objeto._environment = environment;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UsersRoles();
            objeto._image = new Uploadimage();
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
                var imageName = Input.Email + ".png";
                await objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
