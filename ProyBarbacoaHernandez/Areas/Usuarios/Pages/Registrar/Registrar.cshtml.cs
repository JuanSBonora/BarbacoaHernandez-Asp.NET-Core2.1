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
        private ListObject listObject = new ListObject();
        //private LUsuarios _usuarios;
        public RegistrarModel(RoleManager<IdentityRole> roleManager, IHostingEnvironment environment) {
            listObject._roleManager = roleManager;
            listObject._environment = environment;
            listObject._usuarios = new LUsuarios();
            listObject._usersRole = new UsersRoles();
        }
        public void OnGet() {
            Input = new InputModel
            {
                rolesLista = listObject._usersRole.getRoles(listObject._roleManager)
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
            try
            {
                var imageName = Input.Email + ".png";
                var filePath = Path.Combine(listObject._environment.ContentRootPath, "wwwroot/images/fotos", imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.AvatarImage.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Page();
        }
    }
}
