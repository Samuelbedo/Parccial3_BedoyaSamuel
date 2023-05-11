using Microsoft.AspNetCore.Mvc.Rendering;
using Parcial3_BedoyaSamuel.Controllers;
using Parcial3_BedoyaSamuel.DAL;
using Parcial3_BedoyaSamuel.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parcial3_BedoyaSamuel.Models
{
    public class AddServiceViewModel : EditServiceViewModel
    {
        [Display(Name = "Nombre del Propietario")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")]
        public string Owner { get; set; }

        [Display(Name = "Placa del Vehiculo")]
        [MaxLength(6, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")]
        public string NumberPlate { get; set; }

        [Display(Name = "Tipo de Servicio ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid ServiceId { get; set; }

        public IEnumerable<SelectListItem> Services { get; set; }

    }




    //Nombre del propietario........
    //Placa del vehiculo.............
    //servicio solicitado............
    //total..........................
}
