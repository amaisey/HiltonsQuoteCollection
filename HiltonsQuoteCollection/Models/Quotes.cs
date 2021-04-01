using System;
using System.ComponentModel.DataAnnotations;

namespace HiltonsQuoteCollection.Models
{
    public class Quotes
    {
        [Key]
        public int QuoteId { get; set; }

        [Required(ErrorMessage = "Please Enter a Quote")]
        public string Quote { get; set; }

        [Required(ErrorMessage = "Please Enter an Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please Enter a Date")]
        public string Date { get; set; }

        public string Subject { get; set; }

        public string Citation { get; set; }
    }
}
