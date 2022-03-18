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
        public int CardNumber { get; set; }
        public string CardStatus { get; set; }

        public PracticeDeckPage(string deckName, List<Card> cards)
        {
            InitializeComponent();
            DeckName = deckName;
            Cards = cards;
            Count = 0;
            ListLength = Cards.Count;
            ShownCard = Cards[Count];
            CardBackName.IsVisible = false;
            CardNumber = Count + 1;
            CardStatus = $"{CardNumber}/{ListLength}";
        }

        protected override void OnAppearing()
        {
            DeckTitle.Text = DeckName;
            CardFrontName.Text = ShownCard.FrontText;
            CardBackName.Text = "blank";
            NextItem.Text = CardStatus;
            
        }

        // Navigates back to deck
        private async void GoBackToDeck_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void NextItem_Clicked(object sender, EventArgs e)
        {
            if (Count < ListLength-1) // alternate between revealing back and iterating through deck
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
                    CardFrontName.Text = ShownCard.FrontText;

                    CardNumber = Count + 1;
                    CardStatus = $"{CardNumber}/{ListLength}";
                    NextItem.Text = CardStatus;
                }
            }
            else if (Count == ListLength-1) // Reveal back side of last card
            {
                CardBackName.IsVisible = true;
                Count++;
            }
            else // Reset deck to first card
            {
                Count = 0;
                ShownCard = Cards[Count];
                CardBackName.IsVisible = false;
                CardFrontName.Text = ShownCard.FrontText;

                CardNumber = Count + 1;
                CardStatus = $"{CardNumber}/{ListLength}";
                NextItem.Text = CardStatus;
            }

            
        }
    }
}