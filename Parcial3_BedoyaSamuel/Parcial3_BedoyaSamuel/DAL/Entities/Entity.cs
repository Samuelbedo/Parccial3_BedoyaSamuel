using System.ComponentModel.DataAnnotations;

namespace Parcial3_BedoyaSamuel.DAL.Entities
{
    public class Entity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
