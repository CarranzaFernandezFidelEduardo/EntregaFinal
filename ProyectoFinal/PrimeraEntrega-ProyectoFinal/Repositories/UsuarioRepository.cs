using ProyectoFinal.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinal.Repositories
{
    public class UsuarioRepository
    {
        public List<Usuario> TraerUsuario(string NombreUsuario)
        {

            var listaUsuario = new List<Usuario>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM usuario WHERE NombreUsuario = @NombreUser";

                var param = new SqlParameter("NombreUser", SqlDbType.VarChar);
                param.Value = NombreUsuario;

                cmd.Parameters.Add(param);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Password = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();

                    listaUsuario.Add(usuario);

                }

                reader.Close();

                return listaUsuario;

            }
        }

        public void ModificarUsuario(Usuario Usuario)
        {

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE usuario SET Nombre= @nombre, Apellido= @apellido, NombreUsuario= @nombreApellido, Contraseña= @pass, Mail= @mail WHERE Id = @idUsuario";

                var param = new SqlParameter("idUsuario", SqlDbType.Int);
                param.Value = Usuario.Id;

                var param1 = new SqlParameter("nombre", SqlDbType.VarChar);
                param1.Value = Usuario.Nombre;

                var param2 = new SqlParameter("apellido", SqlDbType.VarChar);
                param2.Value = Usuario.Apellido;

                var param3 = new SqlParameter("nombreApellido", SqlDbType.VarChar);
                param3.Value = Usuario.NombreUsuario;

                var param4 = new SqlParameter("pass", SqlDbType.VarChar);
                param4.Value = Usuario.Password;

                var param5 = new SqlParameter("mail", SqlDbType.VarChar);
                param5.Value = Usuario.Mail;

                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Parameters.Add(param4);
                cmd.Parameters.Add(param5);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public string CrearUsuario(Usuario Usuario)
        {
            List<Usuario> usuarios = TraerUsuario(Usuario.NombreUsuario);

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;
      
                if(usuarios.Count == 0)
                {
                    using (SqlConnection connection = new SqlConnection(cs))
                    {
                        connection.Open();

                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@nombre, @apellido, @nombreUsuario, @pass, @mail)";

                        /*var param = new SqlParameter("idUsuario", SqlDbType.Int);
                        param.Value = Usuario.Id;*/

                        var param1 = new SqlParameter("nombre", SqlDbType.VarChar);
                        param1.Value = Usuario.Nombre;

                        var param2 = new SqlParameter("apellido", SqlDbType.VarChar);
                        param2.Value = Usuario.Apellido;

                        var param3 = new SqlParameter("nombreUsuario", SqlDbType.VarChar);
                        param3.Value = Usuario.NombreUsuario;

                        var param4 = new SqlParameter("pass", SqlDbType.VarChar);
                        param4.Value = Usuario.Password;

                        var param5 = new SqlParameter("mail", SqlDbType.VarChar);
                        param5.Value = Usuario.Mail;

                        cmd.Parameters.Add(param1);
                        cmd.Parameters.Add(param2);
                        cmd.Parameters.Add(param3);
                        cmd.Parameters.Add(param4);
                        cmd.Parameters.Add(param5);

                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                    return "Se creo el usuario correctamente";
                }
                else
                {
                  return "No se pudo crear el usuario porque ya existe un nombre de usuario " + Usuario.NombreUsuario + ", Ingrese uno nuevo";  
                }
        
        }

        public void EliminarUsuario(int IdUsuario)
        {

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM usuario WHERE Id = @idUsuario";

                var param = new SqlParameter("idUsuario", SqlDbType.Int);
                param.Value = IdUsuario;

                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public List<Usuario> InicioDeSesion(string NombreUsuario, string Contra)
        {

            var listaUsuario = new List<Usuario>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM usuario WHERE NombreUsuario = @NombreUser AND Contraseña = @Pass";

                var param = new SqlParameter("NombreUser", SqlDbType.VarChar);
                param.Value = NombreUsuario;

                var param2 = new SqlParameter("Pass", SqlDbType.VarChar);
                param2.Value = Contra;

                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param2);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();

                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = reader.GetValue(1).ToString();
                        usuario.Apellido = reader.GetValue(2).ToString();
                        usuario.NombreUsuario = reader.GetValue(3).ToString();
                        usuario.Password = reader.GetValue(4).ToString();
                        usuario.Mail = reader.GetValue(5).ToString();

                        listaUsuario.Add(usuario);

                    }
                }
                else
                {
                    Usuario usuario = new Usuario();

                    usuario.Id = 0;
                    usuario.Nombre = string.Empty;
                    usuario.Apellido = string.Empty;
                    usuario.NombreUsuario = string.Empty;
                    usuario.Password = string.Empty;
                    usuario.Mail = string.Empty;

                    listaUsuario.Add(usuario);
                }


                reader.Close();

                return listaUsuario;

            }
        }
    }
}
