using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Parcial3_BedoyaSamuel.DAL.Entities
{
    public class Vehicle : Entity
    {
        [Display(Name = "Nombre del Propietario")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")]
        public string Owner { get; set; }

        [Display(Name = "Placa del Vehiculo")]
        [MaxLength(6, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")]
        public string NumberPlate { get; set; }

        public Service Services { get; set; }

        public ICollection<VehicleDetails> VehicleDetails { get; set; }
    }
}
