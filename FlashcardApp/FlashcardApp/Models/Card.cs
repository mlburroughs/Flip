using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardApp.Models
{
    public class Card
    {
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public DateTime Date { get; set; }
        public string FileNameFront { get; set; }
        public string FileNameBack { get; set; }
    }
}
