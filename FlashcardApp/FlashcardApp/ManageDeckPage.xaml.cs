using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using FlashcardApp.Models;

namespace FlashcardApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageDeckPage : ContentPage
    {
        public string DeckName;
        public ManageDeckPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var deck = (Deck)BindingContext;
            DeckTitle.Text = deck.DeckName;

            var deckName = deck.DeckName;

            var deckList = deck.CardsInDeck;

            // Create List of cards
            var cards = new List<Card>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), $"*.{deckName}.front.cards.txt");
            foreach (var filename in files)
            {
                var card = new Card
                {
                    FrontText = File.ReadAllText(filename),
                    FileNameFront = filename
                };
                cards.Add(card);
            }
           
            CardsListView.ItemsSource = cards.OrderBy(n => n.FrontText); 
        }


        // Add new card for deck
        private async void NewCardForDeck_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;
            await Navigation.PushModalAsync(new AddCardPage(deck.DeckName)
            {
                BindingContext = new Card()
            });
        }

        // Click on existing card in deck
        private async void CardsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var deck = (Deck)BindingContext;
            await Navigation.PushModalAsync(new AddCardPage(deck.DeckName)
            {
                BindingContext = (Card)e.SelectedItem
            });

        }

        // Go Back to AddDeckPage
        private async void GoBackToDeck_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}