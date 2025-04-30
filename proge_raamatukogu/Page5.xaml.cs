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

namespace Database_Operation
{
    public class sqlLaenutus
    {
        public static void Laenuta(string isbn, string kp, string isikukood) 
        {
            string kp_t = DateTime.ParseExact(kp, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(14).ToString("dd/MM/yyyy");
            string constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B"; //VÕTA PAROOL STUUDIUMIST!!!; Catalog on kaust (?) milles hakkab päringuid tegema, seal andmebaasis on igal tiimil oma kataloog

            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            string ID_sqlQuery = $"SELECT TOP(1) ID FROM RAAMATUD WHERE ISBN = {isbn} AND TAGASTAMISKUUPÄEV IS NULL ORDER BY ID;";
            SqlCommand ID_sqlCmd = new SqlCommand(ID_sqlQuery, conn);
            string ID = ID_sqlCmd.ExecuteScalar()?.ToString() ?? "";

            string raamatud_sqlQuery = $"SELECT RAAMATUD FROM KASUTAJAD WHERE ISIKUKOOD = '{isikukood}';";
            SqlCommand raamatud_sqlCmd = new SqlCommand(raamatud_sqlQuery, conn);
            string raamatud = raamatud_sqlCmd.ExecuteScalar()?.ToString() ?? "";

            string raamatudU = string.IsNullOrEmpty(raamatud) ? ID : raamatud + "," + ID;
            string kasutajaU_sqlQuery = $"UPDATE KASUTAJAD SET RAAMATUD = '{raamatudU}' WHERE ISIKUKOOD = '{isikukood}';";
            SqlCommand kasutajaU_sqlCmd = new SqlCommand(kasutajaU_sqlQuery, conn);
            kasutajaU_sqlCmd.ExecuteNonQuery();


            string raamatudU_sqlQuery = $"UPDATE TOP (1) RAAMATUD \n SET TAGASTAMISKUUPÄEV = '{kp_t}' \n WHERE ISBN = {isbn} AND TAGASTAMISKUUPÄEV IS NULL;";

            SqlCommand raamatudU_sqlCmd = new SqlCommand(raamatudU_sqlQuery, conn);
            raamatudU_sqlCmd.ExecuteNonQuery();

            raamatudU_sqlCmd.Dispose();
            conn.Close();
        }
    }
    public class andmed
    {
        public static (string, string) nimed(string isbn, string isikukood)
        {
            string constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B";

            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            string r_andmed_query = $"SELECT VÄLJAANNE FROM RAAMATUD WHERE ISBN = '{isbn}';";
            SqlCommand r_andmed_cmd = new SqlCommand(r_andmed_query, conn);
            string raamatuNimi = r_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

            string i_andmed_query = $"SELECT NAME FROM KASUTAJAD WHERE ISIKUKOOD = '{isikukood}';";
            SqlCommand i_andmed_cmd = new SqlCommand(i_andmed_query, conn);
            string inimeseNimi = i_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

            conn.Close();

            return (raamatuNimi, inimeseNimi);
        }
    }
}




namespace proge_raamatukogu
{
    public partial class Page5 : Page
    {
        public Page5() { InitializeComponent(); }
        private void ISBN(object sender, TextChangedEventArgs e) {}
        private void isikukood(object sender, TextChangedEventArgs e) {}

        // TESTIMISEKS: ISBN 9789985325933
        //                   9780747542988 
        //                   9781515190998

        private void Laenuta(object sender, RoutedEventArgs e)
        {
            string isbn = tb_isbn.Text;
            string kp = DateTime.Now.ToString("dd/MM/yyyy");
            string isikukood = tb_isik.Text;
            
            Database_Operation.sqlLaenutus.Laenuta(isbn, kp, isikukood);
            (string raamat, string inimene) = Database_Operation.andmed.nimed(isbn, isikukood);
            MessageBox.Show($"{inimene} laenutas raamatu {raamat}");

        }
    }
}
