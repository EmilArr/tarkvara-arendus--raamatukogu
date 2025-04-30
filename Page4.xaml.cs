using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace Database_Operation // Andmebaasi lisamise funktsioon (internetist võetud!!!)
{
    public class sqlTagastus
    {
        public static void Tagasta(string isbn, string isikukood)
        {
            string constr;
            SqlConnection conn;

            constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B"; //VÕTA PAROOL STUUDIUMIST!!!; Catalog on kaust (?) milles hakkab päringuid tegema, seal andmebaasis on igal tiimil oma kataloog

            conn = new SqlConnection(constr);
            conn.Open();

            string sql = $"UPDATE TOP (1) RAAMATUD \n SET TAGASTAMISKUUPÄEV = NULL \n WHERE ISBN = {isbn} AND TAGASTAMISKUUPÄEV IS NOT NULL;";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }
    }
}

namespace proge_raamatukogu
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void ISBN(object sender, TextChangedEventArgs e)
        {

        }

        private void isikukood(object sender, TextChangedEventArgs e)
        {

        }

        private void Tagasta(object sender, RoutedEventArgs e)
        {
            string isbn = tb_isbn.Text;
            string isikukood = tb_isik.Text;

            Database_Operation.sqlTagastus.Tagasta(isbn, isikukood);
        }
    }
}
