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
using ProyBarbacoaHernandez.Areas.Usuarios.Controllers;
using ProyBarbacoaHernandez.Areas.Usuarios.Models;
using ProyBarbacoaHernandez.Data;
using ProyBarbacoaHernandez.Library;

namespace ProyBarbacoaHernandez.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
        private ListObject objeto = new ListObject();
        private static String idGet = null;
        //private LUsuarios _usuarios;
        public RegistrarModel(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager, ApplicationDbContext context,IHostingEnvironment environment) {
            objeto._roleManager = roleManager;
            objeto._userManager = userManager;
            objeto._environment = environment;
            objeto._context = context;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UsersRoles();
            objeto._image = new Uploadimage();
            objeto._userRoles = new List<SelectListItem>();
    }
        public async Task OnGetAsync(String id) 
        {
            if (id != null)
            {
                idGet = id;
                await setEditarAsync(id);
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar {
            [TempData]
            public string ErrorMessage { get; set; }
            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var valor = await guardarAsync();
            if (valor)
            {
                return RedirectToAction(nameof(UsuariosController.Index), "Usuarios");
            }
            else
            {
                return Page();
            }
        }
        private async Task<bool> guardarAsync()
        {
            var valor = false;
            try
            {
                if (ModelState.IsValid)
                {
                    objeto._userRoles.Add(new SelectListItem
                    {
                        Text = Input.Role
                    });
                    var userList = objeto._userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                    if (userList.Count.Equals(0))
                    {
                        var imageName = Input.Email + ".png";
                        var user = new IdentityUser
                        {
                            UserName = Input.Email,
                            Email = Input.Email,
                            PhoneNumber = Input.Telefono
                        };
                        var result = await objeto._userManager.CreateAsync(user, Input.Password);
                        if (result.Succeeded)
                        {
                            await objeto._userManager.AddToRoleAsync(user, Input.Role);
                            var listUser = objeto._userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                            var usuarios = new TUsuarios
                            {
                                Nombre = Input.Nombre,
                                Apellido = Input.Apellido,
                                NID = Input.NID,
                                Imagen = Input.Email,
                                IdUser = listUser[0].Id,
                            };
                            await objeto._context.AddAsync(usuarios);
                            objeto._context.SaveChanges();
                            await objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios");
                            valor = true;
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                Input = new InputModel
                                {
                                    ErrorMessage = item.Description,
                                    rolesLista = objeto._userRoles
                                };
                            }
                            valor = false;
                        }
                    }
                    else
                    {
                        Input = new InputModel
                        {
                            ErrorMessage = "El " + Input.Email + " ya esta registrado",
                            rolesLista = objeto._userRoles
                        };
                        valor = false;
                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = "Seleccione un Role",
                        rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
                    };
                }
            }
            catch (Exception ex)
            {
                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    rolesLista = objeto._userRoles
                };
                valor = false;
            }
            return valor;
        }
        private async Task setEditarAsync(string Email)
        {
            var userList1 = objeto._userManager.Users.Where(u => u.Email.Equals(Email)).ToList();
            var userList2 = objeto._context.TUsuarios.Where(u => u.IdUser.Equals(userList1[0].Id)).ToList();
            var userRoles = await objeto._usersRole.getRole(objeto._userManager, objeto._roleManager, userList1[0].Id);

            Input = new InputModel
            {
                Nombre = userList2[0].Nombre,
                Apellido = userList2[0].Apellido,
                NID = userList2[0].NID,
                Telefono = userList1[0].PhoneNumber,
                Email = userList1[0].Email,
                Password = "*********",
                //Imagen = userList2[0].Imagen,
                //rolesLista = getRoles(userRoles[0].Text)
            };
        }
    }
}
