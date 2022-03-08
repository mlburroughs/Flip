using System;
using FlashcardApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace FlashcardApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Deck List View
        protected override void OnAppearing()
        {
            var decks = new List<Deck>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "*.decks.txt");

            foreach (var filename in files)
            {
                var deck = new Deck
                {
                    DeckName = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename),
                    FileName = filename
                };
                decks.Add(deck);
            }
            DecksListView.ItemsSource = decks.OrderByDescending(n => n.Date);
        }

        // Add new deck
        private async void NewDeck_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddDeckPage
            {
                BindingContext = new Deck()
            });
        }

        // Open existing deck in ListView
        private void DecksListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushModalAsync(new AddDeckPage
            {
                BindingContext = (Deck)e.SelectedItem
            });
        }
    }
}
