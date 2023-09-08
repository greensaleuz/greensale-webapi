using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.DataAccess.ViewModels.BuyerPostsImages
{
    public class BuyerPostsImageViewModel
    {
        public long Id { get; set; }
        public long SellerPostId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
