using System;
using System.Collections.Generic;

namespace Bookify.App.Core.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        public DateTime ExpirationDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Gets or sets the security code.
        /// </summary>
        /// <value>
        /// The security code.
        /// </value>
        public string SecurityCode { get; set; }

        /// <summary>
        /// Gets the expiration date text.
        /// </summary>
        /// <value>
        /// The expiration date text.
        /// </value>
        public string ExpirationDateText => this.ExpirationDate.ToString("MM-yy");

        /// <summary>
        /// Verifies that all properties has a valid value.
        /// If not, returns error messages.
        /// </summary>
        /// <returns></returns>
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