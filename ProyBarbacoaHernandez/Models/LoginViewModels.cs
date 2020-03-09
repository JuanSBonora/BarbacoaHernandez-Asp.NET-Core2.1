using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyBarbacoaHernandez.Models
{
    public class LoginViewModels
    {
        [BindProperty]
        public InputModel input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "<font color='#ff3d00'><strong>Escribe una dirección de correo electrónico</strong></font>")]
            [EmailAddress(ErrorMessage = "<font color='#ff3d00'><strong>El correo electronico no es una dirección de correo electrónico válida</strong></font>")]
            public string Email { get; set; }

            [Required(ErrorMessage = "<font color='#ff3d00'><strong>La contraseña es incorrecta.</strong></font>")]
            [DataType(DataType.Password)] 
            [StringLength(100, ErrorMessage = "<font color='#ff3d00'><strong>El numero de caracteres de {0} debe ser al menos {2} </strong></font>", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
