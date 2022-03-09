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
        public PracticeDeckPage(string deckName, List<Card> cards)
        {
            InitializeComponent();
            DeckName = deckName;
            Cards = cards;
            Count = 0;
            
        }

        protected override void OnAppearing()
        {
            

            DeckTitle.Text = DeckName;
            var firstcard = Cards[0];
            CardFrontName.Text = firstcard.FrontText;
            CardBackName.Text = "blank";


        }


        private async void GoBackToDeck_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}