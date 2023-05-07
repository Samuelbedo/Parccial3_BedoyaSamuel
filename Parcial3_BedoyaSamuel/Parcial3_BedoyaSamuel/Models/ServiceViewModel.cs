using Parcial3_BedoyaSamuel.Controllers;
using Parcial3_BedoyaSamuel.DAL;
using System.ComponentModel.DataAnnotations;

namespace Parcial3_BedoyaSamuel.Models
{
    public class ServiceViewModel : ServicesController
    {
        public ServiceViewModel(DataBaseContext context) : base(context)
        {
        }

        [Display(Name = "Nombre del Propietario")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un país.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Owner{ get; set; }

        [Display(Name = "Placa del vehiculo")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un país.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NumberPlate { get; set; }
    }
    
    
    
    
    //Nombre del propietario........
     //Placa del vehiculo.............
     //servicio solicitado............
     //total..........................
}
