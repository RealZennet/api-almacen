using ApiAlmacen.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class LoteCountController : ApiController
    {
        [HttpGet]
        [Route("api/v1/almacen/generalinfo/products/count")]
        public IHttpActionResult GetProductCount()
        {
            ProductModel ProductModel = new ProductModel();
            int productCount = ProductModel.GetTotalProductOnStoreHouse();

            Dictionary<string, int> result = new Dictionary<string, int>
            {
                { "Count", productCount }
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("api/v1/almacen/generalinfo/batch/count")]
        public IHttpActionResult GetBatchCount()
        {
            BatchModels BatchModel = new BatchModels();
            int loteCount = BatchModel.GetTotalBatchOnStoreHouse();

            Dictionary<string, int> result = new Dictionary<string, int>
            {
                { "Count", loteCount }
            };

            return Ok(result);
        }
    }

}
