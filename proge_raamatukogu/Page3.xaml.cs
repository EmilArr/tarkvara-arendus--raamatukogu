using System;
using System.Windows;
using Microsoft.Data.SqlClient; 
namespace proge_raamatukogu
{
    public class KasutajadRepository
    {
       private readonly string connectionString = "Data source=vhk-12r.database.windows.net;Initial Catalog=Rambo; User ID=Rambo;Password=SQL123go";

        
        /// <param name="name">The user's name.</param>
        /// <param name="createdDate">The creation date for the record.</param>
        public void InsertKasutaja(string name, DateTime createdDate)
        {
            
            string query = "INSERT INTO KASUTAJAD (NAME, CreatedDate) VALUES (@name, @createdDate)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@createdDate", createdDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public partial class Page3 : System.Windows.Controls.Page
    {
        private string userName = string.Empty;
        private long userId; 
        private DateTime createdDate;
        private bool nameSet = false;

     
        private readonly KasutajadRepository repo = new KasutajadRepository();

        public Page3()
        {
            InitializeComponent();
        }

        private void LisaUser(object sender, RoutedEventArgs e)
        {
            userName = tb_nimi.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Palun sisesta sobiv nimi.");
                return;
            }
            nameSet = true;
            MessageBox.Show("Nimi: " + userName);
        }
        private void LisaDate(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(tb_Date.Text.Trim(), out DateTime parsedDate))
            {
                createdDate = parsedDate;
                if (!nameSet)
                {
                    MessageBox.Show("Palun sisesta nimi enne kuupäeva.");
                    return;
                }

                try
                {
                    repo.InsertKasutaja(userName, createdDate);
                    MessageBox.Show("Kasutaja lisatud!");

                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sisestamisel: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lisa korrektses formaadis.");
            }
        }

        private void ClearFields()
        {
            tb_nimi.Text = string.Empty;
            tb_UserID.Text = string.Empty; 
            tb_Date.Text = string.Empty;
            nameSet = false;
        }
    }
}
