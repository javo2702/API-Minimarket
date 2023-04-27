using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;


namespace WebServicesAbb.Controllers
{
    //[Authorize]
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        //Obtener toda la lista de productos
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProductos();

            return productos;
        }

        // GET: api/Productos
        //Buscar productos con la categoria en comun
        [HttpGet]
        public IEnumerable<Producto> Get([FromUri] int IdCategoria)
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProductosCategoria(IdCategoria);

            return productos;
        }

        //Buscar productos que Coincidan con el nombre ingresado
        [HttpGet]
        public IEnumerable<Producto> Get([FromUri]string Nombre)
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProductosNombre(Nombre);
            return productos;
        }

        // Obtener un producto por su Id
        [HttpGet]
        public IHttpActionResult GetOne([FromUri]int id)
        {
            
            if (id <= 0)
                return BadRequest("el Id debe ser mayor que 0");
            
            try
            {
                Producto prod = new Producto();
                ProductoLN producto = new ProductoLN();
                prod = producto.BuscarProductoId(id);
                return Ok(prod);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }

        // Actualizar Stock
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]string cantidad)
        {
            if (id <= 0)
                return BadRequest("IdProducto es nulo");
            try
            {
                Producto producto = new Producto();
                producto = new ProductoLN().ActualizarStock(id, Convert.ToInt32(cantidad));
                return Ok(producto);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }
    }
}
