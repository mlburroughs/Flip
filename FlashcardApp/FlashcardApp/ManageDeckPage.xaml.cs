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

            var cards = new List<Card>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), $"*.{deckName}.cards.txt");
            foreach (var filename in files)
            {
                var card = new Card
                {
                    FrontText = File.ReadAllText(filename),
                    FileName = filename
                };
                cards.Add(card);
            }

            CardsListView.ItemsSource = cards.OrderBy(n => n.FrontText); 
        }



        private async void NewCardForDeck_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;
            await Navigation.PushModalAsync(new AddCardPage(deck.DeckName)
            {
                BindingContext = new Card()
            });
        }

        private async void CardsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var deck = (Deck)BindingContext;
            await Navigation.PushModalAsync(new AddCardPage(deck.DeckName)
            {
                BindingContext = (Card)e.SelectedItem
            });

        }

        private async void GoBackToDeck_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PopModalAsync();
        }
    }
}