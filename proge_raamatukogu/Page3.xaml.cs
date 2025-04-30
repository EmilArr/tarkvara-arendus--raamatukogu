using System;
using System.Windows;
using Microsoft.Data.SqlClient;
using Database_Operation;

namespace proge_raamatukogu
{
    public class KasutajadRepository
    {
        // ma ei saa aru, mis selle connection stringiga viga on
        // see annab errori: login failed user Rambo
        private readonly string connectionString =
            "Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password=UtNZj6Kp;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void InsertKasutaja(string name)
        {
            string query = "INSERT INTO KASUTAJAD (NAME, CreatedDate) VALUES (@name, @createdDate)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public partial class Page3 : System.Windows.Controls.Page
    {
        private readonly KasutajadRepository repo = new KasutajadRepository();

        public Page3()
        {
            InitializeComponent();
        }

        private void SaveAllTextBoxes(object sender, RoutedEventArgs e)
        {
            string userName = tb_nimi.Text.Trim();
            string userIdText = tb_UserID.Text.Trim();

            // Kontrollib nime
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Palun sisesta sobiv nimi.");
                return;
            }

            // Kontrollib user ID
            if (!long.TryParse(userIdText, out long userId))
            {
                MessageBox.Show("Palun sisesta kehtiv kasutaja ID (numbriline väärtus).");
                return;
            }

            try
            {
                repo.InsertKasutaja(userName);
                MessageBox.Show($"Kasutaja lisatud!\nNimi: {userName}");

                // kustutab textboxid.
                tb_nimi.Text = string.Empty;
                tb_UserID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sisestamisel: {ex.Message}");
            }
        }

        private void LisaUserID(object sender, RoutedEventArgs e)
        {

        }
    }
}
