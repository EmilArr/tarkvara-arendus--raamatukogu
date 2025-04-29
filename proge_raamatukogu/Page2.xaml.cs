// siin tuleks osad ära võtta, sest paljusid tegelt ei kasuta ja need on lihtsalt juhenditest kopeerides kaasa tulnud
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
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
using System.Collections;
using Microsoft.Data.SqlClient; // !! Peab eraldi installima !! --> Tools>NuGet Package Manager>Manage NuGet packages for solution> installi Microsoft.Data.SqlClient

namespace Database_Operation // Andmebaasi lisamise funktsioon (internetist võetud!!!)
{
    public class InsertStatement
    {
        public static void Insert(string Valjaanne, string Autor, string Aasta, string ISBN) // Pärast 'Lisa' nupu vajutamist leiab andmed -> sorteerib need -> lisab Insert() funktsiooniga andmebaasi, Inser võtab sisendiks need väärtused mis enne välja said sorteeritud ja eeldab, et lisamise hetkel on raamat kohal mitte välja laenutatud
        {
            string constr;
            SqlConnection conn;

            constr = @"Data Source=vhk-12r.database.windows.net;Initial Catalog=Rambo;User ID=Rambo;Password="; //VÕTA PAROOL STUUDIUMIST!!!; Catalog on kaust (?) milles hakkab päringuid tegema, seal andmebaasis on igal tiimil oma kataloog

            conn = new SqlConnection(constr);
            conn.Open();

            SqlCommand cmd;
            SqlDataAdapter adap = new SqlDataAdapter();

            string sql = $"INSERT INTO RAAMATUD (VÄLJAANNE, AUTOR, VÄLJAANTUD, ISBN, TAGASTAMISKUUPÄEV) VALUES ('{Valjaanne.Replace("'", "''")}', '{Autor}', '{Aasta}', '{ISBN}', NULL);"; // lisab ühe rea andmeid
                    // TAGASTAMISKUUPÄEV on kas NULL või kuupäev, nii täidab ka kohaloleku staatuse funktsiooni

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
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private async void Lisa(object sender, RoutedEventArgs e) // lisamis nupu funktsioon
        {
            string isbn = tb_isbn.Text; // võtab tb_isbn nimelise text box'i sisu
            string apiUrl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data"; // openlibrary works api
            using (HttpClient client = new HttpClient())
            {
                try
                {   // proovib apist andmeid saada ja teeb need JSON formaati
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var andmed = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonResponse) ?? new Dictionary<string, JsonElement>();
                    string valjaanne = "?";
                    string autor = "?";
                    string aasta = "?";

                    if (andmed.TryGetValue($"ISBN:{isbn}", out JsonElement andmed1)) // välja valitud andmete dictionarysse lisamine, peab JSON formaadist neid välja filtreerima
                    {
                        if (andmed1.TryGetProperty("title", out JsonElement title)) { valjaanne = title.GetString() ?? "?"; }
                        else { valjaanne = "?"; }

                        if (andmed1.TryGetProperty("authors", out JsonElement authors) && (authors.ValueKind == JsonValueKind.Array) && authors.ValueKind == JsonValueKind.Array)
                        {
                            var nimed = new List<string>();
                            foreach (var aut in authors.EnumerateArray())
                            { if (aut.TryGetProperty("name", out JsonElement nimi)) { nimed.Add(nimi.GetString() ?? "Unknown Author"); } }
                            autor = string.Join(", ", nimed);
                        }
                        else { autor = "?"; }

                        if (andmed1.TryGetProperty("publish_date", out JsonElement yr)) { aasta = yr.GetString() ?? "?"; }
                        else { aasta = "?"; }

                    }
                    // TESTIMISEKS: Õnnepalu "Mandala" ISBN 9789985325933
                    Database_Operation.InsertStatement.Insert(valjaanne, autor, aasta, isbn); // andmebaasi lisamine
                    MessageBox.Show($"RAAMAT LISATUD!\n{autor} \"{valjaanne}\"");
                }
                catch (Exception ex) // kui ei õnnestu isbni võtta või mingi muu error
                {
                    MessageBox.Show($"Error: {ex.Message}"); // selle asemel et vigaseid või tühjasid andmeid tabelisse panna kuvab errori teavituse
                }

            }
            
        }


        private void ISBN(object sender, TextChangedEventArgs e) // isbn sisestamise text box'i funktsioon, tegelt ebavajalik
        {

        }
    }
}

