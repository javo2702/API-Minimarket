using ABB.Catalogo.Entidades.Core;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Catalogo.Utiles.Helpers;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySql.Data.MySqlClient;

namespace ABB.Catalogo.AccesoDatos.Core
{
    public class ProductoDA
    {
        
        public Producto LlenarEntidad(IDataReader reader)
        {
            Producto producto = new Producto();

            if (!Convert.IsDBNull(reader["idProducto"]))
                producto.idProducto = Convert.ToInt32(reader["idProducto"]);

            if (!Convert.IsDBNull(reader["idCategoria"]))
                producto.idCategoria = Convert.ToInt32(reader["idCategoria"]);

            if (!Convert.IsDBNull(reader["nomProducto"]))
                producto.nomProducto = Convert.ToString(reader["nomProducto"]);

            if (!Convert.IsDBNull(reader["precio"]))
                producto.precio = Convert.ToDecimal(reader["precio"]);

            if (!Convert.IsDBNull(reader["descProducto"]))
                producto.descProducto = Convert.ToString(reader["descProducto"]);

            if (!Convert.IsDBNull(reader["imagen"]))
                producto.imagen = Convert.ToString(reader["imagen"]);

            if (!Convert.IsDBNull(reader["stock"]))
                producto.stock = Convert.ToInt32(reader["stock"]);

            if (!Convert.IsDBNull(reader["descuentoProducto"]))
                producto.descuentoProducto = Convert.ToDecimal(reader["descuentoProducto"]);

            if (!Convert.IsDBNull(reader["nomCategoria"]))
                producto.nomCategoria = Convert.ToString(reader["nomCategoria"]);

            return producto;
        }
        
        public List<Producto> ListarProductos()
        {
            List<Producto> ListaProductos = new List<Producto>();
            Producto producto = null;
            using (MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (MySqlCommand comando = new MySqlCommand("paListarProductos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        producto = LlenarEntidad(reader);
                        ListaProductos.Add(producto);
                    }
                }
                conexion.Close();
            }
            return ListaProductos;
        }
 
        public Producto BuscarProductoId(int IdProducto)
        {
            Producto producto = null;
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
                {
                    using (MySqlCommand comando = new MySqlCommand("paProducto_BuscaProductoId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@idProductoBuscar", IdProducto);
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            producto = LlenarEntidad(reader);
                        }
                        conexion.Close();
                    }
                }
                return producto;
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                return producto;
            }
        }

        public List<Producto> ListarProductosCategoria(int IdCategoria)
        {
            List<Producto> ListaProductosFiltrados = new List<Producto>();
            Producto producto = null;
            using (MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (MySqlCommand comando = new MySqlCommand("paListarProductosCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idCategoriaFiltrar", IdCategoria);
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        producto = LlenarEntidad(reader);
                        ListaProductosFiltrados.Add(producto);
                    }
                }
                conexion.Close();
            }
            return ListaProductosFiltrados;
        }

        public List<Producto> ListarProductosNombre(string Nombre)
        {
            List<Producto> ListaProductosFiltrados = new List<Producto>();
            Producto producto = null;
            using (MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (MySqlCommand comando = new MySqlCommand("paListarProductosNombre", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombreFiltrar", Nombre);
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        producto = LlenarEntidad(reader);
                        ListaProductosFiltrados.Add(producto);
                    }
                }
                conexion.Close();
            }
            return ListaProductosFiltrados;
        }

        public Producto ActualizarStock(int IdProducto, int cantidad)
        {
            Producto producto = null;

            using (MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {

                using (MySqlCommand comando = new MySqlCommand("sp_ActualizarStock", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@idProd", IdProducto);
                    comando.Parameters.AddWithValue("@cantidad", cantidad);
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        producto = LlenarEntidad(reader);
                    }
                    conexion.Close();
                }
            }
            return producto;
        }

    }
}
