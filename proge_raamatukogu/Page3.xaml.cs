using System;
using System.Windows;
using Database_Operation;
using Microsoft.Data.SqlClient;

namespace Database_Operation // Andmebaasi lisamise funktsioon (internetist võetud!!!)
{
    public class kasutaja
    {
        public static void lisaKasutaja(string nimi, string isikukood, string aadress) // Pärast 'Lisa' nupu vajutamist leiab andmed -> sorteerib need -> lisab Insert() funktsiooniga andmebaasi, Inser võtab sisendiks need väärtused mis enne välja said sorteeritud ja eeldab, et lisamise hetkel on raamat kohal mitte välja laenutatud
        {
            string constr;
            SqlConnection conn;

            constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=f6t5zW5B"; //VÕTA PAROOL STUUDIUMIST!!!; Catalog on kaust (?) milles hakkab päringuid tegema, seal andmebaasis on igal tiimil oma kataloog

            conn = new SqlConnection(constr);
            conn.Open();

            SqlCommand cmd;
            SqlDataAdapter adap = new SqlDataAdapter();

            string sql = $"INSERT INTO KASUTAJAD (NAME, ISIKUKOOD, AADRESS) VALUES ('{nimi}', '{isikukood}', '{aadress}');"; // lisab ühe rea andmeid

            cmd = new SqlCommand(sql, conn);

            adap.InsertCommand = new SqlCommand(sql, conn);
            adap.InsertCommand.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }
    }
}

namespace proge_raamatukogu
{
    public partial class Page3 : System.Windows.Controls.Page
    {
        private void isikukood(object sender, System.Windows.Controls.TextChangedEventArgs e) {}
        private void nimi(object sender, System.Windows.Controls.TextChangedEventArgs e) {}
        private void aadress(object sender, System.Windows.Controls.TextChangedEventArgs e) {}


        private void Lisa(object sender, RoutedEventArgs e)
        {
            string isikukood = tb_isikukood.Text;
            string nimi = tb_nimi.Text;
            string aadress = tb_aadress.Text; 
            Database_Operation.kasutaja.lisaKasutaja(nimi, isikukood, aadress); // andmebaasi lisamine
            MessageBox.Show($"Kasutaja {nimi} on lisatud andmebaasi!\nIsikukood: {isikukood}\nAadress: {aadress}");
        }
    }
}
