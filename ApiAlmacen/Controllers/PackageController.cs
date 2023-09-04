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
    }
}