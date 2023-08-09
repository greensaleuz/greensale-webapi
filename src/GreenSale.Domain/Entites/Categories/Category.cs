using System.ComponentModel.DataAnnotations;

namespace GreenSale.Domain.Entites.Categories
{
    public class Category : Auditable
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
