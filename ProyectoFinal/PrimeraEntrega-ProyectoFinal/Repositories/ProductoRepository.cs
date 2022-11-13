using ProyectoFinal.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinal.Repositories
{
    public class ProductoRepository
    {
        public List<Producto> TraerProducto(int IdUsuario)
        {

            var listaProductos = new List<Producto>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto WHERE IdUsuario = @idUsuario";

                var param = new SqlParameter("idUsuario", SqlDbType.Int);
                param.Value = IdUsuario;

                cmd.Parameters.Add(param);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();

                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Descripciones = reader.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(producto);

                }

                reader.Close();

                return listaProductos;

            }
        }


        public List<Producto> TraerProductoParaCargarVenta(int IdProducto)
        {

            var listaProductos = new List<Producto>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto WHERE Id = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = IdProducto;

                cmd.Parameters.Add(param);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();

                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Descripciones = reader.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(producto);

                }

                reader.Close();

                return listaProductos;

            }
        }


        public void CrearProducto(Producto Producto)
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
                cmd.CommandText = "INSERT INTO Producto(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES(@descripcion, @costo, @precioVenta, @stock, @idUsuario)";

               
                var param1 = new SqlParameter("descripcion", SqlDbType.VarChar);
                param1.Value = Producto.Descripciones;

                var param2 = new SqlParameter("costo", SqlDbType.Int);
                param2.Value = Producto.Costo;

                var param3 = new SqlParameter("precioVenta", SqlDbType.Int);
                param3.Value = Producto.PrecioVenta;

                var param4 = new SqlParameter("stock", SqlDbType.Int);
                param4.Value = Producto.Stock;

                var param5 = new SqlParameter("idUsuario", SqlDbType.Int);
                param5.Value = Producto.IdUsuario;

                
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Parameters.Add(param4);
                cmd.Parameters.Add(param5);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public void ModificarProducto(Producto Producto)
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
                cmd.CommandText = "UPDATE producto SET Descripciones= @descripcion, Costo= @costo, PrecioVenta= @precioVenta, Stock= @stock, IdUsuario= @idUsuario WHERE Id = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = Producto.Id;

                var param1 = new SqlParameter("descripcion", SqlDbType.VarChar);
                param1.Value = Producto.Descripciones;

                var param2 = new SqlParameter("costo", SqlDbType.Int);
                param2.Value = Producto.Costo;

                var param3 = new SqlParameter("precioVenta", SqlDbType.Int);
                param3.Value = Producto.PrecioVenta;

                var param4 = new SqlParameter("stock", SqlDbType.Int);
                param4.Value = Producto.Stock;

                var param5 = new SqlParameter("idUsuario", SqlDbType.Int);
                param5.Value = Producto.IdUsuario;

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

        public void ModificarStockDelProducto(int Stock, int IdProducto)
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
                cmd.CommandText = "UPDATE Producto SET Stock -= @stockProductoVendido WHERE Id = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = IdProducto;

                var param1 = new SqlParameter("stockProductoVendido", SqlDbType.Int);
                param1.Value = Stock;

                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param1);
                
                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public void ModificarStockDelProductoParaElimivarVenta(int Stock, int IdProducto)
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
                cmd.CommandText = "UPDATE Producto SET Stock += @stockProductoVendido WHERE Id = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = IdProducto;

                var param1 = new SqlParameter("stockProductoVendido", SqlDbType.Int);
                param1.Value = Stock;

                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param1);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public void EliminarProducto(int IdProducto)
        {

            ProductoVendidoRepository ProductoVendido = new ProductoVendidoRepository();

            ProductoVendido.EliminarProductoVendido(IdProducto);

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM producto WHERE Id = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = IdProducto;
                
                cmd.Parameters.Add(param);
    
                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }


        public List<Producto> TraerProductos()
        {

            var listaProductos = new List<Producto>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();

                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Descripciones = reader.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(producto);

                }

                reader.Close();

                return listaProductos;

            }
        }

        public List<Producto> TraerProductoParaPruductoVendido(int IdProducto)
        {

            var listaProductos = new List<Producto>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto WHERE Id = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = IdProducto;

                cmd.Parameters.Add(param);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();

                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Descripciones = reader.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(producto);

                }

                reader.Close();

                return listaProductos;

            }
        }


    }
}
