using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData.Query.SemanticAst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class UserRoles
    {
        public List<SelectListItem> userRoles;

        public UserRoles()
        {
            userRoles = new List<SelectListItem>();
        }

        /// <summary>
        /// Aqui podemos obtener los datos de la tabla rol para mostrarlos en el DropDownList
        /// 
        /// </summary>
        /// <param name="roleManager">es el objeto que maneja los roles utilizando
        /// la clase RoleManager la cual se encarga de manejar los Roles en Asp.Net</param>
        /// <returns></returns>
        public List<SelectListItem> getRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();
            foreach(var Data in roles)
            {
                userRoles.Add(new SelectListItem()
                {
                    Value = Data.Id,
                    Text = Data.Name
                });
            }
            return userRoles;
        }
    }
}
