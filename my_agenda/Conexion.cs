using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace my_agenda
{
    class Conexion
    {
        public static SQLiteConnection conectar()
        {
            string database = Application.StartupPath + "\\myAgenda.db;";

            SQLiteConnection cn = new SQLiteConnection("Data Source = "+database);
            // cn...
            //cn.Open();
            return cn;
        }
    }
}
