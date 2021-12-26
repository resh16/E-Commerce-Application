using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingBL.ViewModelBL
{
    public class ViewImageBL
    {
        public int ProductId { get; set; }
        public string Version { get; set; }

        public IFormFile Image { get; set; }

        public string UniqueImageName { get; set; }


    }
}
