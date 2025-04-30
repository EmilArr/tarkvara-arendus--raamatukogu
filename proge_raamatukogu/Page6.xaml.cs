using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace Database_Operation
{
    public class RInfo
    {
        public class RAndmed
        {
            public static (string, string, string) raamat(string isbn)
            {
                string constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B";

                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                string r0_andmed_query = $"SELECT VÄLJAANNE FROM RAAMATUD WHERE ISBN = '{isbn}';";
                SqlCommand r0_andmed_cmd = new SqlCommand(r0_andmed_query, conn);
                string valjaanne = r0_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

                string r1_andmed_query = $"SELECT AUTOR FROM RAAMATUD WHERE ISBN = '{isbn}';";
                SqlCommand r1_andmed_cmd = new SqlCommand(r1_andmed_query, conn);
                string autor = r1_andmed_cmd.ExecuteScalar()?.ToString() ?? "";

                string r2_andmed_query = $"SELECT VÄLJAANTUD FROM RAAMATUD WHERE ISBN = '{isbn}';";
                SqlCommand r2_andmed_cmd = new SqlCommand(r2_andmed_query, conn);
                string aasta = r2_andmed_cmd.ExecuteScalar()?.ToString() ?? ""; 

                conn.Close();

                return (valjaanne, autor, aasta);
            }
        }
    }
}

namespace proge_raamatukogu
{
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public Page6()
        {
            InitializeComponent();
        }

        private void ISBN(object sender, TextChangedEventArgs e)
        {

        }
        private void rInfo_click(object sender, RoutedEventArgs e)
        {
            (string valjaanne, string aasta, string autor) = Database_Operation.RInfo.RAndmed.raamat(tb_isbn.Text);
            MessageBox.Show($"ISBN: {tb_isbn.Text}\nVäljaande nimi: \"{valjaanne}\"\nAutor: {autor}\nAasta: {aasta}");
        }
    }
}
