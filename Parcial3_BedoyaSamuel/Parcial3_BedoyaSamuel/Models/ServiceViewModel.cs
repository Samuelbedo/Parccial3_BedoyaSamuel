using System.ComponentModel.DataAnnotations;

namespace Parcial3_BedoyaSamuel.Models
{
    public class ServiceViewModel
    {
        [Display(Name = "Nombre del Propietario")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un país.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid CountryId { get; set; }
    }
    
    
    
    
    //Nombre del propietario........
     //Placa del vehiculo.............
     //servicio solicitado............
     //total..........................
}
