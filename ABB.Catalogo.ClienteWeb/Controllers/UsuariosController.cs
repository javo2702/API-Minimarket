using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class UsuariosController : Controller
    {
        string RutaApi = "https://localhost:44332/Api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat
        // GET: Usuarios
        public ActionResult Index()
        {
            string controladora = "Usuarios";
            string metodo = "Get";
            List<Usuarios> listausuarios = new List<Usuarios>();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listausuarios = JsonConvert.DeserializeObject<List<Usuarios>>(data);
            }

            return View(listausuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Usuarios usuario = new Usuarios();// se crea una instancia de la clase usuario
            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListaRol();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;

            return View(usuario);
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(Usuarios collection)
        {
            string controladora = "Usuarios";
            try
            {
                // TODO: Add insert logic here
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    //establece el tipo de dato de tranferencia
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    //typo de decodificador reconocimiento carecteres especiales
                    usuario.Encoding = UTF8Encoding.UTF8;
                    //convierte el objeto de tipo Usuarios a una trama Json
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), usuarioJson);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            string controladora = "Usuarios";
            string metodo = "GetUserId";
            Usuarios users = new Usuarios();

            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListaRol();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuarios>(data);
            }
            return View(users); // Retorna la vista con el usuario a editar
        }

        // POST: Usuarios/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Usuarios collection)
        {
            string controladora = "Usuarios";
            string metodo = "ModifyUserLess";
            try
            {
                // TODO: Add insert logic here
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    //establece el tipo de dato de tranferencia
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    //typo de decodificador reconocimiento carecteres especiales
                    usuario.Encoding = UTF8Encoding.UTF8;
                    //convierte el objeto de tipo Usuarios a una trama Json
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    //string rutacompleta = RutaApi + controladora + "/" + metodo + "?IdUsuario=" + id;
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), "PUT", usuarioJson);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
                return View();
            }
        }


        // POST: Usuarios/Edit/5
        public ActionResult Cambiar_Clave(int id)
        {
            string controladora = "Usuarios";
            string metodo = "Put";
            Usuarios users = new Usuarios();

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuarios>(data);
            }
            return View(users);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = "Usuarios";
            Usuarios users = new Usuarios();

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuarios>(data);
            }
            return View(users); // Retorna la vista con el usuario a editar
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Usuarios collection)
        {
            string controladora = "Usuarios";
            try
            {
                // TODO: Add insert logic here
                using (WebClient usuario = new WebClient())
                {
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), "DELETE", string.Empty);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
    }
}
