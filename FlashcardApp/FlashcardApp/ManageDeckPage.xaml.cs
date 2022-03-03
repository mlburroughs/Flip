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
        public ManageDeckPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var deck = (Deck)BindingContext;
            DeckTitle.Text = deck.DeckName;

        }



        private async void NewCardForDeck_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new AddCardPage
            {
                BindingContext = new Card()
            });
        }

        private async void CardsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new AddCardPage
            {

                BindingContext = (Card)e.SelectedItem
            });

        }
    }
}