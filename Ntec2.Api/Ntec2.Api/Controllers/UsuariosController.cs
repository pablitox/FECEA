using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Ntec2.Api.Globals;
using Ntec2.Api.Models;

namespace Ntec2.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        public string GetUsuarios()
        {
            GlobalsDb.Query = "select * from Usuarios";
            GetData(GlobalsDb.Query);
            return GlobalsDb.JsonsResponse;
        }

        [HttpGet("{nombre}")]
        public string GetUsuario(string nombre)
        {
            GlobalsDb.Query = "select * from Usuarios where Nombre like '%" + nombre + "%'" ;
            GetData(GlobalsDb.Query);
            return GlobalsDb.JsonsResponse;
        }

        [HttpPost]
        public string PostUsuarios(Usuario usuario)
        {
            GlobalsDb.Query = "insert into Usuarios values (null,'" + usuario.Nombre + "','" + usuario.Email + "','" + usuario.Dni + "')";
            updateData(GlobalsDb.Query);
            return GlobalsDb.JsonsResponse;
        }

        [HttpPut]
        public string PutUsuarios(Usuario usuario, int id)
        {
            GlobalsDb.Query = "update Usuarios set Nombre = '" + usuario.Nombre 
                + "', Email = '" + usuario.Email 
                + "', Dni = '" + usuario.Dni
                + "' where Id = " + id;
            updateData(GlobalsDb.Query);
            return GlobalsDb.JsonsResponse;
        }

        [HttpDelete]
        public string DeleteUsuarios(string id)
        {
            GlobalsDb.Query = "delete from Usuarios where Id = " + id;
            updateData(GlobalsDb.Query);
            return GlobalsDb.JsonsResponse;
        }

        private void GetData(string query)
        {
            SqliteConnection ObjConnection = new SqliteConnection(GlobalsDb.ConnectionString);
            SqliteCommand ObjCommand = new SqliteCommand(query, ObjConnection);
            ObjConnection.Open();
            SqliteDataReader reader = ObjCommand.ExecuteReader();
            var lstUsers = new List<Usuario>();

            while (reader.Read())
            {
                lstUsers.Add(new Usuario
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Email = reader["Email"].ToString(),
                    Dni = reader["Dni"].ToString(),
                });
            }

            GlobalsDb.JsonsResponse = JsonConvert.SerializeObject(lstUsers, Formatting.Indented);
            ObjConnection.Close();
        }

        private void updateData(string query)
        {
            SqliteConnection ObjConnection = new SqliteConnection(GlobalsDb.ConnectionString);
            ObjConnection.Open();
            SqliteCommand accion = new SqliteCommand(query, ObjConnection);
            accion.ExecuteNonQuery();
            GlobalsDb.JsonsResponse = JsonConvert.SerializeObject("ok", Formatting.Indented);
            ObjConnection.Close();
        }

    }
}
