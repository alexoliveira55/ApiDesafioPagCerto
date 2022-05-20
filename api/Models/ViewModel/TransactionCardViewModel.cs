using api.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models.ViewModel
{
    public class TransactionCardViewModel
    {
        [Required]
        public decimal ValueGross { get; set; }
        [Required]
        public int NumberOfInstallments { get; set; }
        [NumberCard]
        public string NumberCard { get; set; }

    }
}
