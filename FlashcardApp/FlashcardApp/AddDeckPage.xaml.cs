using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlashcardApp.Models;
using System.IO;

namespace FlashcardApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class AddDeckPage : ContentPage
    {

        public object myDeck { get; internal set; }

        public AddDeckPage()
        {
            InitializeComponent();
        }


        // Updates time variables if deck is saved
        protected override void OnAppearing()
        {
            var deck = (Deck)BindingContext;


            // Gets cards and places them in deck.CardsInList
            var cards = new List<Card>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), $"*.{deck.DeckName}.front.cards.txt");

            foreach (var filename in files)
            {
                var card = new Card
                {
                    FrontText = File.ReadAllText(filename),
                    FileNameFront = filename
                };
                cards.Add(card);
            }

            deck.CardsInDeck = cards;


            // Sets up visibility based on deck attributes
            if (!string.IsNullOrEmpty(deck.FileName))
            {
                CreatedOn.Text = File.GetCreationTime(deck.FileName).ToString();
                DeckName.Placeholder = deck.DeckName;
                DeckName.IsVisible = false;
                TitleText.Text = deck.DeckName;
                TitleText.IsVisible = true;

                PracticeDeck.IsVisible = deck.CardsInDeck?.Count > 0;
            }
            else
            {
                CreatedOn.IsVisible = false;
                CreatedOnText.IsVisible = false;
                TitleText.IsVisible = false;
                DeckName.Placeholder = "Enter Title";
                PracticeDeck.IsVisible = false;
            }
        }


        // Saves deck and returns to MainPage
        private async void DeckSave_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;

            // If filename is empty, get new files name
            if (string.IsNullOrEmpty(deck.FileName))
            {
                deck.FileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.decks.txt");
                deck.DeckName = DeckName.Text;
                File.WriteAllText(deck.FileName, deck.DeckName);
            }
            
            await Navigation.PopModalAsync();// Navigates back to Deck Page
        }


        // Deletes deck and returns to MainPage
        private async void DeckDelete_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;

            // If there is a deck
            if (File.Exists(deck.FileName))
            {
                // Deletes front card filenames
                var frontCardFiles = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), $"*.{deck.DeckName}.front.cards.txt");

                foreach (var filename in frontCardFiles)
                {
                    File.Delete(filename);
                }

                // Deletes back card filenames
                var backCardFiles = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), $"*.{deck.DeckName}.back.cards.txt");

                foreach (var filename in backCardFiles)
                {
                    File.Delete(filename);
                }

                File.Delete(deck.FileName);// Deletes deck name filename
            }

            
            DeckName.Text = string.Empty;// Text box clears
            await Navigation.PopModalAsync();// Navigates back to Deck Page
        }


        // Opens ManageDeckPage
        private async void ManageDeck_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;

            // If filename is empty, get new filename
            if (string.IsNullOrEmpty(deck.FileName))
            {
                deck.FileName = Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                        $"{Path.GetRandomFileName()}.decks.txt");
                deck.DeckName = DeckName.Text;
                File.WriteAllText(deck.FileName, DeckName.Text);
            }
                
            
            await Navigation.PushModalAsync(new ManageDeckPage
                {
                    BindingContext = deck
                });
            
        }


        // Opens PraticeDeckPage
        private async void PracticeDeck_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;
            var card = deck.CardsInDeck;

            await Navigation.PushModalAsync(new PracticeDeckPage(deck.DeckName, deck.CardsInDeck)
            {
            }) ;
        }
    }
}