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
        public string DeckName { get; set; }
        public AddCardPage(string deckName)
        {
            InitializeComponent();
            DeckName = deckName;
        }

        protected override void OnAppearing()
        {
            var card = (Card)BindingContext;
            var cardFrontFilename = card.FileNameFront;
            
            if (!string.IsNullOrEmpty(card.FileNameFront))
            {
                var cardBackFilename = cardFrontFilename.Replace("front", "back");
                var cardBackText = File.ReadAllText(cardBackFilename);

                CardFrontTitle.Text = card.FrontText;
                CardBackTitle.Text = cardBackText;
                CardFrontName.Text = card.FrontText;
                CardBackName.Text = cardBackText;

                CardFrontName.IsVisible = false;
                CardBackName.IsVisible = false;
                CardFrontNameFrame.IsVisible = false;
                CardBackNameFrame.IsVisible = false;
            }
            else
            {
                CardFrontName.Text = "";
                CardBackName.Text = "";

                CardFrontTitle.IsVisible = false;
                CardBackTitle.IsVisible = false;
                CardFrontTitleFrame.IsVisible = false;
                CardBackTitleFrame.IsVisible = false;
                CardFrontName.IsVisible = true;
                CardBackName.IsVisible = true;
                CardFrontNameFrame.IsVisible = true;
                CardBackNameFrame.IsVisible = true;

            }
        }


        private async void CardSave_Clicked(object sender, EventArgs e)
        {
            var card = (Card)BindingContext;

            // Create new Card if filename front is empty and CardFrontName editor is not empty
            if (string.IsNullOrEmpty(card.FileNameFront) && !string.IsNullOrEmpty(CardFrontName.Text))
            {
                var rpath = Path.GetRandomFileName();

                card.FileNameFront = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                $"{rpath}.{DeckName}.front.cards.txt");
                File.WriteAllText(card.FileNameFront, CardFrontName.Text);

                card.FileNameBack = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                $"{rpath}.{DeckName}.back.cards.txt");
                File.WriteAllText(card.FileNameBack, CardBackName.Text);
            }

            // Navigate back to ManageDeck Page
            await Navigation.PopModalAsync();
        }

        private async void CardDelete_Clicked(object sender, EventArgs e)
        {
            var card = (Card)BindingContext;

            // If there is a note
            if (File.Exists(card.FileNameFront))
            {
                File.Delete(card.FileNameFront);
                File.Delete(card.FileNameBack);
            }


            // Navigation back to ManageDeck Page
            await Navigation.PopModalAsync();
        }
    }
}