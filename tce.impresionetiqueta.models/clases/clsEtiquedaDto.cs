using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tce.impresionetiqueta.models.clases
{
    public class clsEtiquedaDto
    {
        public string descripcionProducto { get; set; }
        public string codigoEAN { get; set; }
        public string modeloProducto { get; set; }
        public string contenido { get; set; }
        public byte[] imagenEtiqueta { get; set; }
        public string serialProducto { get; set; }
        public string paisOrigen { get; set; }
    }
}
