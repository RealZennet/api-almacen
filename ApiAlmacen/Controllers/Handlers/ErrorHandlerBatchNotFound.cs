using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Controllers.Handlers
{
    public class BatchNotFoundException : Exception
    {
        public BatchNotFoundException(int batchId) : base($"El lote con el ID {batchId} no existe.")
        {
        }
    }
}