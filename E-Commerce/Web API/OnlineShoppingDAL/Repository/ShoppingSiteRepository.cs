using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;
using OnlineShoppingDAL.Data;
using OnlineShoppingDAL.Interface;
using OnlineShoppingDAL.Model;
using OnlineShoppingDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingDAL.Repository
{
    public class ShoppingSiteRepository : IShoppingSiteRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ShoppingSiteContext _context;



        public ShoppingSiteRepository(IConfiguration configuration, ShoppingSiteContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }



        //Products List
        public async Task<IEnumerable<ProductsList>> ProductsList()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "DisplayProductsAdmin";

            var listOfProducts = await Task.FromResult(dbConnection.Query<ProductsList>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfProducts;
        }



        //GET Image BY ID
        public async Task<ViewImage> GetImageById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Get_Product_by_ID";

            DynamicParameters parameters = new();

            parameters.Add("@ProductId", id);

            return await Task.FromResult(dbConnection.Query<ViewImage>(sp, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());

        }






        //Add list

        public void AddProducts(ProductsList productList)
        {
            Product product = new Product();

            product.Title = productList.Title;
            product.Description = productList.Description;
            product.CategoryId = productList.CategoryId;
            product.BrandId = productList.BrandId;
            product.Discount = productList.Discount;
            product.ExpiryDate = productList.ExpiryDate;
            product.StockDate = productList.StockDate;
            product.Price = productList.Price;
            product.NoOfStock = productList.NoOfStock;
            product.CreatedAt = productList.CreatedAt;
            product.CreatedBy = productList.CreatedBy;

            _context.Products.Add(product);
            _context.SaveChanges();


        }

        public void AddImage(AddImage addImage)
        {
            Image image = new Image();




            //Image Info from angular
            FileInfo fi = new(addImage.Image.FileName);

            var Filename = $"{DateTime.UtcNow.ToString("yyyyMMddTHHmmssfffffffK")}{fi.Extension}";

            //Store to filePath
            string filePath = Path.Combine(_configuration.GetSection("AppSettings:ImagePath").Value.ToString(), Filename);


            if (addImage.Image != null)
            {
                int version = _context.Images.Where(x => x.ProductId == addImage.ProductId).Count()+1;

               
                using (var fs = File.Create(filePath))
                {
                    addImage.Image.CopyTo(fs);
                }

                


                image.ProductId = addImage.ProductId;
                image.Version = version;

                //user uploaded name
                image.Image1 = addImage.Image.FileName;

                //timestamp + img Extension
                image.UniqueImageName = Filename;

            }




            _context.Images.Add(image);
            _context.SaveChanges();
        }


        





    }
}


// Task<FeedbackViewModel> GetFeedbackInfo();