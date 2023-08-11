using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Application.Exceptions
{
    public class ImageNotFoundException:NotFoundException
    {
        public ImageNotFoundException()
        {
            TitleMessage = "Image not found!";
        }
    }
}
