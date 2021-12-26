using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingDAL.ViewModel
{
   public class ViewImage
    {

        public int ProductId { get; set; }
        public string Version { get; set; }
        
        public string Image { get; set; }

         public string UniqueImageName { get; set; }


    }
}
