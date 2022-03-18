using FlashcardApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace FlashcardApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeDeckPage : ContentPage
    {
        public string DeckName { get; set; }
        public List<Card> Cards { get; set; }
        public int Count { get; set; }
        public int ListLength { get; set; }
        public Card ShownCard { get; set; }

        public PracticeDeckPage(string deckName, List<Card> cards)
        {
            InitializeComponent();
            DeckName = deckName;
            Cards = cards;
            Count = 0;
            ListLength = Cards.Count;
            ShownCard = Cards[Count];
            CardBackName.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            DeckTitle.Text = DeckName;
            CardFrontName.Text = ShownCard.FrontText;
            CardBackName.Text = "blank";
            NextItem.Text = Count.ToString();
        }

        // Navigates back to deck
        private async void GoBackToDeck_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void NextItem_Clicked(object sender, EventArgs e)
        {
            if (CardBackName.IsVisible == false)
            {
                CardBackName.IsVisible = true;
            }
            else
            {
                Count++;
                ShownCard = Cards[Count];

                CardBackName.IsVisible = false;
            }
        }
    }
}