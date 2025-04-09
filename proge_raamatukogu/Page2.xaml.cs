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

namespace proge_raamatukogu
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private async void Lisa(object sender, RoutedEventArgs e)
        {
            string isbn = tb_isbn.Text;
            string apiUrl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var andmed = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonResponse) ?? new Dictionary<string, JsonElement>();
                    var raamat = new Dictionary<string, string>();


                    if (andmed.TryGetValue($"ISBN:{isbn}", out JsonElement andmed1))
                    {
                        if (andmed1.TryGetProperty("title", out JsonElement title)) { raamat.Add("Nimi", title.GetString() ?? "Unknown"); }
                        else { raamat.Add("Nimi", "???"); }

                        if (andmed1.TryGetProperty("authors", out JsonElement authors) && (authors.ValueKind == JsonValueKind.Array) && authors.ValueKind == JsonValueKind.Array)
                        {
                            var nimed = new List<string>();
                            foreach (var aut in authors.EnumerateArray())
                            { if (aut.TryGetProperty("name", out JsonElement nimi)) { nimed.Add(nimi.GetString() ?? "Unknown Author"); } }
                            raamat.Add("Autor", string.Join(", ", nimed));
                        }
                        else { raamat.Add("Autor", "???"); }

                        if (andmed1.TryGetProperty("publish_date", out JsonElement aasta)) { raamat.Add("Aasta", aasta.GetString() ?? "Unknown"); }
                        else { raamat.Add("Aasta", "???"); }

                    }
                    raamat.Add("ISBN", isbn.ToString());
                    MessageBox.Show($"{string.Join(Environment.NewLine, raamat)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void ISBN(object sender, TextChangedEventArgs e)
        {

        }
    }
}
