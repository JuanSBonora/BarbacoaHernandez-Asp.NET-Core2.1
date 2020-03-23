using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyBarbacoaHernandez.Areas.Usuarios.Models
{
    public class InputModelRegistrar
    {
        [Required(ErrorMessage = "<font color = 'red'>El campo Nombre es obligatorio.</font>")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "<font color = 'red'>El campo Apellido es obligatorio.</font>")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "<font color = 'red'>El campo NID es obligatorio.</font>")]
        public string NID { get; set; }
        [Required(ErrorMessage = "<font color = 'red'>El campo Telefono es obligatorio.</font>")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "<font color = 'red'>El campo Correo electrónico es obligatorio.</font>")]
        [EmailAddress(ErrorMessage = "<font color = 'red'>El campo Correo electrónico no es una direccion de correo electrónico válida.</font>")]
        public string Email { get; set; }
        [Required(ErrorMessage = "<font color = 'red'>El campo Contraseña es obligatorio.</font>")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "<font color = 'red'>El número de caracteres de {0} debe ser al menos {2}.</font>", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
