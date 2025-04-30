using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace proge_raamatukogu
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void lisaRaamat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void lisaKasutaja_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }

        private void raamatuTagastus_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }

        private void raamatuLaenutus_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page5.xaml", UriKind.Relative));
        }

        private void RInfoClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page6.xaml", UriKind.Relative));
        }

        private void KInfoClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page7.xaml", UriKind.Relative));
        }
    }
}
