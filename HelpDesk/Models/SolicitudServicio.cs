using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class SolicitudServicio
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="El nombre debe de tener al menos 2 caracteres.", MinimumLength = 2)]
        public string Solicitante { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name ="Fecha de Solicitud")]
        public DateTime FechaSolicitud { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [NotMapped]
        [Display(Name ="Estado")]
        public List<SelectListItem> estadoSolicitudes { get; set; }
        [Display(Name = "Estado")]
        public string estadoS { get; set; }
        private EstadoSolicitud estadoSolicitud { get; set; }
        [NotMapped]
        public List<SelectListItem> nombreTecnicos { get; set; }
        [Display(Name = "Tecnico Asignado")]
        public string tecnicoAsig { get; set; }
        private TecnicoAsignado tecnicoAsignado { get; set; }
        [NotMapped]
        public List<SelectListItem> solicitudesSinAsignar { get; set; }
        [Display(Name = "Comentario Adicional")]
        public string comentarioAdicional { get; set; }
        [Display(Name = "Comentario Tecnico")]
        public string comentarioTareaCompleta { get; set; }

        public SolicitudServicio()
        {
            estadoSolicitudes = new List<SelectListItem>();
            estadoSolicitud = new EstadoSolicitud();
            nombreTecnicos = new List<SelectListItem>();
            tecnicoAsignado = new TecnicoAsignado();
            solicitudesSinAsignar = new List<SelectListItem>();
        }

        public void getEstados(ApplicationDbContext _context) {
            estadoSolicitudes = estadoSolicitud.getEstado(_context);
        }

        public void getTecnico(ApplicationDbContext _context)
        {
            nombreTecnicos = tecnicoAsignado.getTecnicos(_context);
        }

        public List<SelectListItem> getSolicitudSinAsignar(ApplicationDbContext _context)
        {
            var solicitudSinAsignar = from s in _context.SolicitudServicio
                                      where s.tecnicoAsig == null
                                      select s;
            var listarSolicitudSinAsignar = solicitudSinAsignar.ToList();
            foreach(var Data in listarSolicitudSinAsignar)
            {
                solicitudesSinAsignar.Add(new SelectListItem()
                {
                    Value = Data.ID.ToString(),
                    Text = Data.estadoS
                });
            }
            return solicitudesSinAsignar;
        }
        
    }
}
