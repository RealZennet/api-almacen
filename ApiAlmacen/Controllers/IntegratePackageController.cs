using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class IntegratePackageController:ApiController
    {

        private Dictionary<string, string> showResult(string message)
        {
            Dictionary<string, string> resultJson = new Dictionary<string, string>();
            resultJson.Add("Accion realizada con exito: ", message);
            return resultJson;
        }

        [Route("api/v1/integrarpaquetes")]
        public IHttpActionResult Post([FromBody] IntegratePackageModels IntegratePackage)
        {
            if (!ModelState.IsValid || IntegratePackage == null)
            {
                var errorResponse = $"Error en el ingreso de datos con el id {IntegratePackage.IDBatch} y " +
                    $"{IntegratePackage.IDPackage}";
                return BadRequest(errorResponse.ToString());
            }

            try
            {
                IntegratePackage.Save();
                return Ok(showResult($"Producto {IntegratePackage.IDPackage} fue asignado correctamente al lote {IntegratePackage.IDBatch}"));
            }
            catch (Exception)
            {
                return BadRequest("Error en el ingreso de datos, si el error persiste contacta a un desarrollador");
            }
        }


        [Route("api/v1/integrarpaquetes")]
        public IHttpActionResult Get()
        {
            IntegratePackageModels products = new IntegratePackageModels();
            var assignedProductsList = products.getAllAsignedProducts();
            if (assignedProductsList == null || !assignedProductsList.Any())
            {
                return NotFound();
            }

            var AssignedProductsView = assignedProductsList.Select(everyPackage => new GetIntegratePackagesView
            {
                IDPackage = everyPackage.IDPackage,
                IDBatch = everyPackage.IDBatch,
            }).ToList();

            return Ok(AssignedProductsView);
        }

        [Route("api/v1/integrarpaquetes/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            IntegratePackageModels package = new IntegratePackageModels();
            var integratedPackageList = package.getAllAsignedProducts();
            var IntegratedPackageSearch = integratedPackageList.FirstOrDefault(p => p.IDPackage == id);

            if (IntegratedPackageSearch == null)
            {
                return NotFound();
            }
            else
            {
                var IntegratedPackagesView = new GetIntegratePackagesView
                {
                    IDPackage = IntegratedPackageSearch.IDPackage,
                    IDBatch = IntegratedPackageSearch.IDBatch,
                };

                return Ok(IntegratedPackagesView);
            }
        }
        [Route("api/v1/integrarpaquetes/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            IntegratePackageModels package = new IntegratePackageModels();
            var packageList = package.getAllAsignedProducts();
            var packageAssigned = packageList.FirstOrDefault(everyPackage => everyPackage.IDPackage == id);
            if (packageAssigned == null)
            {
                return NotFound();
            }
            else
            {
                packageAssigned.Delete();
                return Ok(showResult($"Package con ID {id} eliminado con éxito"));
            }
        }

    }

}