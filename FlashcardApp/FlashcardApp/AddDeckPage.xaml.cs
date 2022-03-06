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

            if (!string.IsNullOrEmpty(deck.FileName))
            {
                CreatedOn.Text = File.GetLastAccessTime(deck.FileName).ToString();
                AccessedOn.Text = File.GetCreationTime(deck.FileName).ToString(); ;
                DeckName.Placeholder = deck.DeckName;
            }
            else
            {
                CreatedOn.IsVisible = false;
                CreatedOnText.IsVisible = false;
                AccessedOn.IsVisible = false;
                AccessedOnText.IsVisible = false;
                DeckName.Placeholder = "Enter Title";
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
            }
            else
            {
                DeckName.Text = deck.DeckName;
            }
            
            File.WriteAllText(deck.FileName, DeckName.Text);


            // Navigate back to Deck Page
            await Navigation.PopModalAsync();
        }


        // Deletes deck and returns to MainPage
        private async void DeckDelete_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;

            // If there is a note
            if (File.Exists(deck.FileName))
            {
                File.Delete(deck.FileName);
            }

            // Text box clears
            DeckName.Text = string.Empty;

            // Navigation back to Deck Page
            await Navigation.PopModalAsync();
        }


        // Opens ManageDeckPage
        private async void ManageDeck_Clicked(object sender, EventArgs e)
        {
            var deck = (Deck)BindingContext;
            DeckName.Text = deck.DeckName;

            // If filename is empty, get new files name
            if (string.IsNullOrEmpty(deck.FileName))
            {
                deck.FileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.decks.txt");
            }

            File.WriteAllText(deck.FileName, DeckName.Text);

            await Navigation.PushModalAsync(new ManageDeckPage
            {
                BindingContext = deck
            });
        }


        // Opens PraticeDeckPage
        private async void PracticeDeck_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PracticeDeckPage
            {
                BindingContext = new Deck()
            });
        }
    }
}