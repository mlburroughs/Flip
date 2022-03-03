using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardApp.Models
{
    class Deck
    {
        public string DeckName { get; set; }
        public DateTime Date { get; set; }
        public List<Card> CardsInDeck { get; set; }
        public string FileName { get; set; }
    }
}
