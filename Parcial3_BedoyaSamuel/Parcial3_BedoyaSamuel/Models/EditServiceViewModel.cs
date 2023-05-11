using Parcial3_BedoyaSamuel.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parcial3_BedoyaSamuel.Models
{
    public class EditServiceViewModel : Entity
    {
        [Display(Name = "Fecha de Entrega del Vehiculo")]
        public DateTime DeliveryDate { get; set; }
    }
}
