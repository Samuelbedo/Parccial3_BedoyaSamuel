using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Parcial3_BedoyaSamuel.DAL.Entities
{
    public class VehicleDetails : Entity
    {
        [Display(Name = "Fecha del Pedido")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Fecha de Entrega del Vehiculo")]
        public DateTime DeliveryDate { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
