using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData.Query.SemanticAst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class EstadoSolicitud
    {
        public int ID { get; set; }
        public List<SelectListItem> Estados;
        public string Estado { get; set; }

        public EstadoSolicitud()
        {
            Estados = new List<SelectListItem>();
        }

        public List<SelectListItem> getEstado(ApplicationDbContext _context)
        {
            var estados = from l in _context.EstadoSolicitud select l;
            var listaEstado = estados.ToList();
            foreach (var Data in listaEstado)
            {
                Estados.Add(new SelectListItem()
                {
                    Value = Data.ID.ToString(),
                    Text = Data.Estado
                });
            }
            return Estados;
        }
    }
}
