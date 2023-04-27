using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.AccesoDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.LogicaNegocio.Core;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class ProductoLN : BaseLN
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                ProductoDA productos = new ProductoDA();
                return productos.ListarProductos();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                return lista;
            }
        }

        public List<Producto> ListarProductosCategoria(int IdCategoria)
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                ProductoDA productos = new ProductoDA();
                return productos.ListarProductosCategoria(IdCategoria);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                return lista;
            }
        }

        public List<Producto> ListarProductosNombre(string Nombre)
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                ProductoDA productos = new ProductoDA();
                return productos.ListarProductosNombre(Nombre);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                return lista;
            }
        }


        public Producto BuscarProductoId(int pProductoId)
        {
            try
            {
                ProductoDA producto = new ProductoDA();
                return producto.BuscarProductoId(pProductoId);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                throw;
            }
        }

        public Producto ActualizarStock(int IdProducto, int cantidad)
        {
            try
            {
                return new ProductoDA().ActualizarStock(IdProducto, cantidad);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}
