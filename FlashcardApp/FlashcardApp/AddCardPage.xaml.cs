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
            if (!string.IsNullOrEmpty(card.FileName))
            {
                CardTitle.Text = card.FrontText;
                CardName.IsVisible = false;
                CardName.Text = card.FrontText;
            }
            else
            {
                CardTitle.IsVisible = false;
                CardName.IsVisible = true;
                CardName.Text = "";
            }
        }


        private async void CardSave_Clicked(object sender, EventArgs e)
        {

            var card = (Card)BindingContext;

            // Create new Card

            if (string.IsNullOrEmpty(card.FileName) && !string.IsNullOrEmpty(CardName.Text))
            {
                card.FileName = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.{DeckName}.cards.txt");
                File.WriteAllText(card.FileName, CardName.Text);
            }

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