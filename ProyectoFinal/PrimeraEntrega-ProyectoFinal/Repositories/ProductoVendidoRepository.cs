using ProyectoFinal.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal.Repositories
{
    public class ProductoVendidoRepository
    {
        public List<ProductoVendido> TraerProductoVendido(int IdUsuario)
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();
            ProductoRepository producto = new ProductoRepository();

            List<Producto> ListaProducto = producto.TraerProducto(IdUsuario);

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;


            foreach(Producto prod in ListaProducto)
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM ProductoVendido WHERE IdProducto = @idProducto";

                    var param = new SqlParameter("idProducto", SqlDbType.Int);
                    param.Value = prod.Id;

                    cmd.Parameters.Add(param);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductoVendido ProductoVendido = new ProductoVendido();

                        ProductoVendido.Id = Convert.ToInt32(reader.GetValue(0));
                        ProductoVendido.Stock = Convert.ToInt32(reader.GetValue(1));
                        ProductoVendido.IdProducto = Convert.ToInt32(reader.GetValue(2));
                        ProductoVendido.IdVenta = Convert.ToInt32(reader.GetValue(3));


                        listaProductosVendidos.Add(ProductoVendido);

                    }

                    reader.Close();
  
                }
            }

            return listaProductosVendidos;

        }

        public List<ProductoVendido> TraerProductoVendidoParaVentas(int IdVenta)
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM ProductoVendido WHERE IdVenta = @idVenta";

                var param = new SqlParameter("idVenta", SqlDbType.Int);
                param.Value = IdVenta;

                cmd.Parameters.Add(param);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido ProductoVendido = new ProductoVendido();

                    ProductoVendido.Id = Convert.ToInt32(reader.GetValue(0));
                    ProductoVendido.Stock = Convert.ToInt32(reader.GetValue(1));
                    ProductoVendido.IdProducto = Convert.ToInt32(reader.GetValue(2));
                    ProductoVendido.IdVenta = Convert.ToInt32(reader.GetValue(3));


                    listaProductosVendidos.Add(ProductoVendido);

                }

                reader.Close();

                return listaProductosVendidos;
            }
        }

        public void EliminarProductoVendido(int IdProducto)
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
                cmd.CommandText = "DELETE FROM ProductoVendido WHERE IdProducto = @idProducto";

                var param = new SqlParameter("idProducto", SqlDbType.Int);
                param.Value = IdProducto;

                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public void CrearProductoVendido(Venta venta, int IdVenta)
        {
            ProductoRepository productos = new ProductoRepository();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();

                    foreach (ProductoVendido prod in venta.listaProductoVendido)
                    {

                        SqlCommand cmd2 = connection.CreateCommand();
                        cmd2.CommandText = "INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) VALUES( @stock, @idProducto, @idVenta)";

                        var param1 = new SqlParameter("stock", SqlDbType.Int);
                        param1.Value = prod.Stock;

                        var param2 = new SqlParameter("idProducto", SqlDbType.Int);
                        param2.Value = prod.IdProducto;

                        var param3 = new SqlParameter("idVenta", SqlDbType.BigInt);
                        param3.Value = IdVenta;

                        cmd2.Parameters.Add(param1);
                        cmd2.Parameters.Add(param2);
                        cmd2.Parameters.Add(param3);

                        cmd2.ExecuteNonQuery();

                        productos.ModificarStockDelProducto(prod.Stock, prod.IdProducto);

                    }

                connection.Close();
        
            }

        }

        public void EliminarProductoVendidoXIdVenta(int IdVenta)
        {

            List<ProductoVendido> ProductoVendido = TraerProductoVendidoParaVentas(IdVenta);

            ProductoRepository Producto = new ProductoRepository();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-KU34FRJ\\MSSSQLSERVER";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;

            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM ProductoVendido WHERE IdVenta = @idVenta";

                var param = new SqlParameter("idVenta", SqlDbType.Int);
                param.Value = IdVenta;

                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                connection.Close();

            }

            foreach(ProductoVendido producVen in ProductoVendido)
            {
                Producto.ModificarStockDelProductoParaElimivarVenta(producVen.Stock,producVen.IdProducto);
            }

           
        }
    }
}
