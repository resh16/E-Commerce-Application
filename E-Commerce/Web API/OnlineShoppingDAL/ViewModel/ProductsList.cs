using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingDAL.ViewModel
{
        public class ProductsList
        {

           public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public int CategoryId { get; set; }

            public int BrandId { get; set; }

            public string BrandName { get; set; }

             public string CategoryName { get; set; }    

             public decimal Price { get; set; }

            public decimal Discount { get; set; }
           
            public DateTime StockDate { get; set; }
            public int NoOfStock { get; set; }
           
            public DateTime ExpiryDate { get; set; }

            public DateTime CreatedAt { get; set; }
       
       
            public Guid CreatedBy { get; set; }





        }
}
