using HelpDesk.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un maximo de {1} caracter de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Permisos")]
        [UIHint("List")]
        public List<SelectListItem> Roles { get; set; }
        public string Rol { get; set; }
        private UserRoles userRoles { get; set; }

        public RegisterViewModel()
        {
            Roles = new List<SelectListItem>();
            userRoles = new UserRoles();

            /*Roles.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Usuario"
            });

            Roles.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Administrador"
            });

            Roles.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Tecnicos"
            });*/
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context">es el parametro que contiene la db haciendo referencia
        /// al DbContext</param>
        public void getRoles(RoleManager<IdentityRole> roleManager)
        {
            Roles = userRoles.getRoles(roleManager);
        }
    }
}
