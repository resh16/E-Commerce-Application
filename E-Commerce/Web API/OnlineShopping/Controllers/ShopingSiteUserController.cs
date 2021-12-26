using Microsoft.AspNetCore.Mvc;
using OnlineShoppingBL.InterfaceBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles="User")]
    public class ShopingSiteUserController : ControllerBase
    {


        private readonly IShoppingSiteBL _shoppingSiteBL;

        public ShopingSiteUserController(IShoppingSiteBL shoppingSiteBL)
        {
            this._shoppingSiteBL = shoppingSiteBL;

        }

        [HttpGet]
        public async Task<IActionResult> ProductsListUser()
        {

            try
            {

                var ListOfProducts = await _shoppingSiteBL.ProductsListBL();
                return Ok(ListOfProducts);

            }

            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

        }

        
    }
}
