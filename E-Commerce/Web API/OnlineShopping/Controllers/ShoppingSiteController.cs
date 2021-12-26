using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineShoppingBL.InterfaceBL;
using OnlineShoppingBL.ViewModelBL;
using System;
using System.IO;
using System.Threading.Tasks;


namespace OnlineShopping.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   // [Authorize(Roles="Admin")]
    public class ShoppingSiteController : ControllerBase
    {

        private readonly IShoppingSiteBL _shoppingSiteBL;
        private readonly IConfiguration _configuration;


        public ShoppingSiteController(IShoppingSiteBL shoppingSiteBL, IConfiguration configuration)
        {
           this._shoppingSiteBL = shoppingSiteBL;
            this._configuration = configuration;

        }

        [HttpGet]
       // [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ProductsList()
        {

            try
            {

                var ListOfProducts = await _shoppingSiteBL.ProductsListBL();
                return Ok(ListOfProducts);

            }

            catch(Exception e)
            {
                throw new Exception(e.Message);

            }

        }



        [HttpPost]
      //[Authorize(Roles = "Admin")]//

        public IActionResult AddProducts(ProductsListBL productsListBL)
        {
            try
            {
                _shoppingSiteBL.AddProducts(productsListBL);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }


        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult AddImage([FromForm]AddImageBL addImageBL)
        {
            try
            {
                //var productId = HttpContext.Request.Form["ProductId"];
                //var image = HttpContext.Request.Form["Image"];

                // var imgName = new String(Path.GetFileNameWithoutExtension(image.).Take(10).ToArray()).Replace(" ", "-");
                //  var imgpath = HttpContext.Current.Session.Server.MapPath("~/Images/" + image);
                //   postedFile.SaveAs(imgpath);
                _shoppingSiteBL.AddImage(addImageBL);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
      // [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> GetImageById(int id)
        {
            var imageById = await _shoppingSiteBL.GetImageById(id);

            //  string filePath = Path.Combine(_configuration.GetSection("AppSettings").GetSection("ImagePath").Value.ToString(), Filename);
            if (imageById != null && imageById.UniqueImageName != null)
            {

                string filePath = Path.Combine(_configuration.GetSection("AppSettings").GetSection("ImagePath").Value.ToString(),imageById.UniqueImageName);
               

            
                if (System.IO.File.Exists(filePath))
                {
                   
                    var bytes = System.IO.File.ReadAllBytes(filePath);
                    return File(bytes,  filePath.Contains(".jpg")? "image/jpg": "image/pjpg");
                }
            }
            else
            {

                string filePath2 = Path.Combine(_configuration.GetSection("AppSettings").GetSection("ImagePath").Value.ToString(), "DefaultImage.jpg");
                var bytes = System.IO.File.ReadAllBytes(filePath2);
                return File(bytes, "image/jpg");
            }

            return null;
        }


    }
}
