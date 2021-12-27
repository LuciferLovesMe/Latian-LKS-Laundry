using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timer_lks_2
{
    class Utils
    {
        public static string conn = @"Data Source=asmodeus;Initial Catalog=test2;Integrated Security=True";
    }

    class Model
    {
        public static int id { set; get; }
        public static string name { set; get; }
        public static int id_job { set; get; }
        public static string email { set; get; }
        public static string phone { set; get; }
    }

    class Command
    {
        static SqlConnection connection = new SqlConnection(Utils.conn);
        public static void exec(string com)
        {
            SqlCommand command = new SqlCommand(com, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataTable getData (string com)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(com, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

    }
}

