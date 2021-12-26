using Microsoft.AspNetCore.Mvc;
using OnlineShoppingBL.ViewModelBL;
using OnlineShoppingDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingBL.InterfaceBL
{
   public interface IShoppingSiteBL
   {
        Task<IEnumerable<ProductsListBL>> ProductsListBL();
        public void AddProducts(ProductsListBL productsListBL);

        public void AddImage(AddImageBL addImageBL);

        Task<ViewImage> GetImageById(int id);
    }
}
//Task<IEnumerable<ProductsList>> ProductsList();