using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyBarbacoaHernandez.Areas.Usuarios.Models;
using ProyBarbacoaHernandez.Data;
using ProyBarbacoaHernandez.Library;

namespace ProyBarbacoaHernandez.Areas.Usuarios.Pages.Eliminar
{
    public class EliminarModel : PageModel
    {
        private ListObject objeto = new ListObject();
        private InputModel model;
        public EliminarModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            objeto._usuarios = new LUsuarios(userManager, signInManager, roleManager, context);
        }
        public async Task<IActionResult> OnGetAsync(String id)
        {
            if (id != null)
            {
                var data = await objeto._usuarios.getTUsuariosAsync(id);
                Input = new InputModel
                {
                    Id = data[0].Id,
                    ID = data[0].ID,
                    NID = data[0].NID,
                    Nombre = data[0].Nombre,
                    Apellido = data[0].Apellido,
                    Email = data[0].Email,
                };
                model = Input;
                return Page();
            }
            else
            {
                //var url = Request.Scheme + "://" + Request.Host.Value;
                return Redirect("/Usuarios?area=Usuarios");
            }
        }
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar
        {
            [TempData]
            public string ErrorMessage { get; set; }
        }
        public IActionResult OnPost()
        {
            try
            {
                var usuarios = new TUsuarios
                {
                    ID = model.Id,
                };
                objeto._context.TUsuarios.Remove(usuarios);
                objeto._context.SaveChanges();
                return Redirect("/Usuarios?area=Usuarios");
            }
            catch (Exception ex)
            {
                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                };
                return Page();
            }
        }
    }
}