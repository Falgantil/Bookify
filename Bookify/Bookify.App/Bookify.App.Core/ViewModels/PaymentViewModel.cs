using System;
using System.Collections.Generic;

namespace Bookify.App.Core.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public string CardNumber { get; set; }

        public DateTime ExpirationDate { get; set; } = DateTime.Today;

        public string SecurityCode { get; set; }

        public string ExpirationDateText => this.ExpirationDate.ToString("MM-yy");

        public IEnumerable<string> VerifyData()
        {
            if ((this.CardNumber?.Length ?? 0) < 16)
                yield return "Ugyldig kort nummer. ";
            if (this.ExpirationDate < DateTime.Now)
                yield return "Ugyldig udløbs dato. ";
            if ((this.SecurityCode?.Length ?? 0) < 3)
                yield return "Ugyldig sikkerheds kode. ";
        }
    }
}