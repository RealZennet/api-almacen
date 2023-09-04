using ApiAlmacen.Controllers.Handlers;
using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class PackageController : ApiController
    {

        private Dictionary<string, string> showResult(string message)
        {
            Dictionary<string, string> resultJson = new Dictionary<string, string>();
            resultJson.Add("Accion realizada con exito: ", message);
            return resultJson;
        }


        [Route("api/v1/paquetes")]
        public IHttpActionResult Post([FromBody] PackageModel packages)
        {
            if (!ModelState.IsValid || packages == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            packages.Save();
            return Ok($"Paquete guardado");
        }

        [Route("api/v1/paquetes")]
        public IHttpActionResult Get()
        {
            PackageModel packages = new PackageModel();
            var listpackages = packages.getAllPackages();
            var packagesView = listpackages.Select(everyPackage => new GetPackagesView
            {
                IDPackage = everyPackage.IDPackage,
                Street = everyPackage.Street,
                HouseNumber = everyPackage.HouseNumber,
                Corner = everyPackage.Corner,
                Customer = everyPackage.Customer
            }
            ).ToList();

            return Ok(packagesView);
        }

        [Route("api/v1/paquetes/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] PackageModel package)
        {
            try
            {
                if (!ModelState.IsValid || package == null)
                {
                    var errorResponse = $"Error en el ingreso de datos con {id}.";
                    return BadRequest(errorResponse.ToString());
                }

                package.IDPackage = id;
                package.Edit();

                return Ok(showResult(id.ToString()));
            }
            catch (ErrorHandlerPackageNotFound ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}