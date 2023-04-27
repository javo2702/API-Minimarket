using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.Entidades.Core
{
    public class Producto
    {
        public int idProducto { get; set; }
        public int idCategoria { get; set; }
        public string nomProducto { get; set; }
        public decimal precio { get; set; }
        public string descProducto { get; set; }
        public string imagen { get; set; }
        public int stock { get; set; }
        public decimal descuentoProducto { get; set; }
        public string nomCategoria { get; set; }
    }
}
