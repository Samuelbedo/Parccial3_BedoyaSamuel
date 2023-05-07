using System.ComponentModel.DataAnnotations;

namespace Parcial3_BedoyaSamuel.DAL.Entities
{
    public class Service : Entity
    {
        [Display(Name = "Nombre del Servicio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")]
        public string Name { get; set; }

        [Display(Name = "Precio del Servicio")]
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")]
        public float Price { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
