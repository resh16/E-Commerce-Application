
using Microsoft.AspNetCore.Http;
using OnlineShoppingBL.InterfaceBL;
using OnlineShoppingBL.ViewModelBL;
using OnlineShoppingDAL.Interface;
using OnlineShoppingDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingBL.Logic
{
    public class ShoppingSiteBL : IShoppingSiteBL
    {

        private readonly IShoppingSiteRepository _shoppingSiteDAL;

        public ShoppingSiteBL(IShoppingSiteRepository shoppingSite)
        {
            this._shoppingSiteDAL = shoppingSite;
        }


        

        //List all Products
        public async Task<IEnumerable<ProductsListBL>> ProductsListBL()
        {

            var obj = await _shoppingSiteDAL.ProductsList();

            var viewModel = (from o in obj
                             select new ProductsListBL
                             {
                                 Id = o.Id,
                                 Title = o.Title,
                                 CategoryId = o.CategoryId,
                                 BrandId = o.BrandId,
                                 BrandName = o.BrandName,
                                 CategoryName = o.CategoryName,
                                 Description = o.Description,
                                 Price = o.Price,
                                 StockDate = o.StockDate,
                                 ExpiryDate = o.ExpiryDate,
                                 NoOfStock = o.NoOfStock,
                                 Discount = o.Discount
                                 

                             }).ToList();
                            

                return viewModel;
            
            
        }

        //View Image By Product Id
        public async Task<ViewImage> GetImageById(int id)
        {
            
           var res = await _shoppingSiteDAL.GetImageById(id);

            return res;

        }


        //add products

        public void AddProducts(ProductsListBL productsListBL)
        {
            ProductsList productsList = new();

            productsList.Title = productsListBL.Title;
            productsList.BrandId = productsListBL.BrandId;
            productsList.CategoryId = productsListBL.CategoryId;
            productsList.Description = productsListBL.Description;
            productsList.Price = productsListBL.Price;
            productsList.Discount = productsListBL.Discount;
            productsList.StockDate = productsListBL.StockDate;
            productsList.ExpiryDate = productsListBL.ExpiryDate;
            productsList.NoOfStock = productsListBL.NoOfStock;
            productsList.CreatedAt = DateTime.UtcNow;
            productsList.CreatedBy = Guid.Parse("CEE1C569-C912-4CC7-1F3F-08D9B56A212A");

            

            _shoppingSiteDAL.AddProducts(productsList);
            

        }


        public void AddImage(AddImageBL addImageBL)
        {
            AddImage addImage = new();

           

            addImage.ProductId = addImageBL.ProductId;
            addImage.Image = addImageBL.Image;

            _shoppingSiteDAL.AddImage(addImage );
        }


    }
}
