using Microsoft.AspNetCore.Mvc;
using OnlineShoppingDAL.Model;
using OnlineShoppingDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingDAL.Interface
{
    public interface IShoppingSiteRepository
    {
        Task<IEnumerable<ProductsList>> ProductsList();
        public void AddProducts(ProductsList productList);

        public void AddImage(AddImage addImage);

         Task<ViewImage> GetImageById(int id);

    }
}
