using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class TecnicoAsignado
    {
        public int ID { get; set; }
        [NotMapped]
        public List<SelectListItem> nombreTecnicos { get; set; }
        public string nombreTecnico { get; set; }

        public TecnicoAsignado()
        {
            nombreTecnicos = new List<SelectListItem>();
        }

        public List<SelectListItem> getTecnicos(ApplicationDbContext _context)
        {
            var tecnicos = from t in _context.applicationUsers select t;
            var listadoUser = tecnicos.ToList();
            foreach (var Data in listadoUser)
            {
                nombreTecnicos.Add(new SelectListItem()
                {
                    Value = Data.Id,
                    Text = Data.Nombre
                });
            }

            return nombreTecnicos;
        }
    }
}
