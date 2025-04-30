using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace Database_Operation // Andmebaasi lisamise funktsioon (internetist võetud!!!)
{
    public class sqlTagastus
    {
        public static void Tagasta(string isbn, string isikukood)
        {
            string constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B";
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            string ID = "";
            string raamatud_sqlQuery = $"SELECT RAAMATUD FROM KASUTAJAD WHERE ISIKUKOOD = '{isikukood}';";
            SqlCommand raamatud_sqlCmd = new SqlCommand(raamatud_sqlQuery, conn);
            string raamatud = raamatud_sqlCmd.ExecuteScalar()?.ToString() ?? "";

            List<string> raamatudList = raamatud.Split(',').ToList();
            for (int i = 0; i < raamatudList.Count; i++)
            {
                string checkIsbnQuery = $"SELECT ISBN FROM RAAMATUD WHERE ID = '{raamatudList[i]}';";
                SqlCommand checkIsbnCmd = new SqlCommand(checkIsbnQuery, conn);
                string foundIsbn = checkIsbnCmd.ExecuteScalar()?.ToString() ?? "";
                    
                if (foundIsbn == isbn){
                    ID = raamatudList[i];
                    checkIsbnCmd.Dispose();
                    break;}
                    checkIsbnCmd.Dispose();
            }

            raamatudList.Remove(ID);
            string raamatudU = string.Join(",", raamatudList);

            string kasutajaU_sqlQuery = $"UPDATE KASUTAJAD SET RAAMATUD = '{raamatudU}' WHERE ISIKUKOOD = '{isikukood}';";
            SqlCommand kasutajaU_sqlCmd = new SqlCommand(kasutajaU_sqlQuery, conn);
            kasutajaU_sqlCmd.ExecuteNonQuery();

            string raamatudU_sqlQuery = $"UPDATE RAAMATUD SET TAGASTAMISKUUPÄEV = NULL WHERE ID = {ID};";
            SqlCommand raamatudU_sqlCmd = new SqlCommand(raamatudU_sqlQuery, conn);
            raamatudU_sqlCmd.ExecuteNonQuery();

            raamatud_sqlCmd.Dispose();
            kasutajaU_sqlCmd.Dispose();
            raamatudU_sqlCmd.Dispose();

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
        public Page4() {InitializeComponent(); }
        private void ISBN(object sender, TextChangedEventArgs e) { }
        private void isikukood(object sender, TextChangedEventArgs e) { }

        private void Tagasta(object sender, RoutedEventArgs e)
        {
            string isbn = tb_isbn.Text;
            string isikukood = tb_isik.Text;

            Database_Operation.sqlTagastus.Tagasta(isbn, isikukood);
            (string raamat, string inimene) = Database_Operation.andmed.nimed(isbn, isikukood);
            MessageBox.Show($"{inimene} tagastas raamatu \"{raamat}\"");
        }
    }
}
