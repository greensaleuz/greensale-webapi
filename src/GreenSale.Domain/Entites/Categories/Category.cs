using System.ComponentModel.DataAnnotations;

namespace GreenSale.Domain.Entites.Categories
{
    public class Category : Auditable
    {
        public string Name { get; set; } = string.Empty;
    }
}
