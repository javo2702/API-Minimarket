﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.LogicaNegocio.Core;
using ABB.Catalogo.Entidades.Core;

namespace WebServicesAbb.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
        // GET: api/Usuarios
        [HttpGet]
        public IEnumerable<Usuarios> Get()
        {
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = new UsuariosLN().ListarUsuarios();

            return usuarios;
        }

        // GET: api/Usuarios/5
        // Metodo que recibe 2 parametros, si los parámetros son válidos se retornará 1 en caso contrario devolverá 0
        [HttpGet]
        public IHttpActionResult Get([FromUri] string pUsuario, [FromUri] string pPassword)
        {
            if (pUsuario == null || pPassword == null)
            {
                return BadRequest("Debe enviar las credenciales correctas");
            }

            try
            {
                UsuariosLN usuario = new UsuariosLN();
                var rsp = usuario.GetUsuarioId(pUsuario, pPassword);
                return Ok(Convert.ToString(rsp));
                // return usuario.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw ex;
            }
        }

        // public Usuarios GetUserId([FromUri] int IdUsuario)
        [HttpGet]
        public IHttpActionResult GetUserId([FromUri] int IdUsuario)
        {
            if (IdUsuario <= 0)
            {
                return BadRequest("el Id debe ser mayor que 0");
            }

            try
            {
                Usuarios usu = new Usuarios();
                UsuariosLN usuario = new UsuariosLN();
                usu = usuario.BuscaUsuarioId(IdUsuario);
                return Ok(usu);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }

        // POST: api/Usuarios
        [HttpPost]
        // public void Post([FromBody]Usuarios value)
        public IHttpActionResult Post([FromBody] Usuarios value)
        {
            if (value.CodUsuario == null)
            {
                return BadRequest("CodUsuario es nulo");
            }
            if (value.ClaveTxt == null)
            {
                return BadRequest("ClaveTxt es nulo");
            }
            if (value.Nombres == null)
            {
                return BadRequest("Nombres es nulo");
            }
            if (value.IdRol <= 0)
            {
                return BadRequest("IdRol es nulo");
            }
            Usuarios usuario = new UsuariosLN().InsertarUsuario(value);
            return Ok(usuario);
        }


        // PUT: api/Usuarios/5
        [HttpPut]
        //public void Put(int id, [FromBody]Usuarios value)
        public IHttpActionResult Put(int id, [FromBody] Usuarios value)
        {
            if (id <= 0)
            {
                return BadRequest("CodUsuario es nulo");
            }
            Usuarios usuario = new Usuarios();
            usuario = new UsuariosLN().ModificarUsuario(id, value);
            return Ok(usuario);
        }


        // DELETE: api/Usuarios/5
        [HttpDelete]
        public void Delete(int id)  // Para borrar informacion
        {
            UsuariosLN u = new UsuariosLN();
            u.EliminarUsuario(id);
        }
    }
}
