using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace Database_Operation
{
    public class KInfo
    {
        public class KAndmed
        {
            public static (string, string, string) isik(string isikukood)
            {
                string constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B";

                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                string k0_andmed_query = $"SELECT NAME FROM KASUTAJAD WHERE ISIKUKOOD = '{isikukood}';";
                SqlCommand k0_andmed_cmd = new SqlCommand(k0_andmed_query, conn);
                string nimi = k0_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

                string k1_andmed_query = $"SELECT AADRESS FROM KASUTAJAD WHERE ISIKUKOOD = '{isikukood}';";
                SqlCommand k1_andmed_cmd = new SqlCommand(k1_andmed_query, conn);
                string aadress = k1_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

                string k2_andmed_query = $"SELECT CreatedDate FROM KASUTAJAD WHERE ISIKUKOOD = '{isikukood}';";
                SqlCommand k2_andmed_cmd = new SqlCommand(k2_andmed_query, conn);
                string konto_loodud = k2_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

                conn.Close();

                return (nimi, aadress, konto_loodud);
            }
        }
    }
}

namespace proge_raamatukogu
{
    public partial class Page7 : Page
    {
        public Page7()
        {
            InitializeComponent();
        }

        private void kInfo_click(object sender, RoutedEventArgs e)
        {
            (string nimi, string aadress, string konto_loodud) = Database_Operation.KInfo.KAndmed.isik(tb_isikukood.Text);
            MessageBox.Show($"Nimi: {nimi}\nAadress: {aadress}\nKonto loodud: {konto_loodud}");
        }

        private void isikukood(object sender, TextChangedEventArgs e) { }
    }
}
