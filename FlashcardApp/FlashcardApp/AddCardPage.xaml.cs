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
    public partial class AddCardPage : ContentPage
    {

        public AddCardPage()
        {
            InitializeComponent();
        }


        private async void CardSave_Clicked(object sender, EventArgs e)
        {
            // Get Deck
            var deck = (Deck)BindingContext;

            // Get Deck Name
            var deckname = deck.DeckName;

            // Create new Card
            var card = new Card();

            card.FileName = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.{deckname}.cards.txt");

            File.WriteAllText(card.FileName, CardName.Text);

            // Navigate back to Deck Page
            await Navigation.PopModalAsync();
        }

        private async void CardDelete_Clicked(object sender, EventArgs e)
        {
            var card = (Card)BindingContext;

            // If there is a note
            if (File.Exists(card.FileName))
            {
                File.Delete(card.FileName);
            }


            // Navigation back to Deck Page
            await Navigation.PopModalAsync();
        }
    }
}