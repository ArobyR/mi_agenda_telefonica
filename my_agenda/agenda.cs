using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace my_agenda
{
    class Agenda
    {
        private SQLiteConnection cn = null; // para la conexion;
        private SQLiteCommand cmd = null; // para ejecutar comando SQLite
        private SQLiteDataReader reader = null; // para almacenar los datos 
        private DataTable table = null; // para organizar la informacion recibida

        // metodos para insertar en la base de datos
        public bool insertar(string nombre,string telefono)
        {
            //string query = "INSERT INTO directorio(nombre, telefono) VALUES('" + nombre + "','" + telefono + "')";
            string query = $"INSERT INTO directorio(nombre, telefono)VALUES('{nombre}','{telefono}')";
                
            return ExecuteNoNQuery(query);
        }


        // metodo para eliminar
        public bool eliminar(int id)
        {
            //string query = "DELETE FROM directorio WHERE id ='" + id + "'";
            string query = $"DELETE FROM directorio WHERE id='{id}'";
                
            return ExecuteNoNQuery(query);
        }

        // metodo para actualizar
        public bool actualizar (int id, string nombre, string telefono)
        {

            //string query = "UPDATE directorio SET nombre='" + nombre + "',telefono='" + telefono + "'WHERE id='" + id.ToString() + "'";
            string query = $"UPDATE directorio SET nombre = {nombre}, telefono={telefono} WHERE id='{id}'";

            return ExecuteNoNQuery(query);
        }
        private bool ExecuteNoNQuery(string query)
        {
            try
            {
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "ocurrio un Error en el proceso");
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }
        // metodo para consultar
        public DataTable consultar()
        {
            string query = "SELECT * FROM directorio";
               
            return ExecuteReader(query);
        }
        public DataTable Filtrar(string Texto)
        {
            string query = $"SELECT * FROM directorio WHERE Nombre LIKE '%{Texto}%' OR Telefono LIKE '%{Texto}%'";
            return ExecuteReader(query);
        }
        private DataTable ExecuteReader(string query)
        {
            try
            {
                nombresColumnas();
                
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                reader = cmd.ExecuteReader();

                //int count = 0;
                while (reader.Read())
                {
                    //count++;
                    table.Rows.Add(new object[] { reader["id"], reader["nombre"], reader["telefono"] });
                }
                reader.Close();
                cn.Close();
                return table;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + " " + e.StackTrace);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return table;
        }

        // metodo para darle nombres a la columnas
        private void nombresColumnas()
        {
            table = new DataTable();
            table.Columns.Add("Id");
            //table.Columns.Add("N.");
            table.Columns.Add("Nombres");
            table.Columns.Add("Telefono");
        }
    }
}