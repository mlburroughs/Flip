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
            var card = (Card)BindingContext;

            // If filename is empty
            if (string.IsNullOrEmpty(card.FileName))
            {
                card.FileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.cards.txt");
            }
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

            // Text box clears
            CardName.Text = string.Empty;

            // Navigation back to Deck Page
            await Navigation.PopModalAsync();
        }
    }
}