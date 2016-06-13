using System.Collections.Generic;

namespace Plugin.GoogleAnalytics.Abstractions.Model
{
    public sealed class Transaction
    {
        public Transaction()
        {
            Items = new List<TransactionItem>();
        }

        public Transaction(string transactionId, long totalCostInMicros)
            : this()
        {
            TransactionId = transactionId;
            TotalCostInMicros = totalCostInMicros;
        }

        public string TransactionId { get; set; }
        public string Affiliation { get; set; }
        public long TotalCostInMicros { get; set; }
        public long ShippingCostInMicros { get; set; }
        public long TotalTaxInMicros { get; set; }
        public string CurrencyCode { get; set; }
        public IList<TransactionItem> Items { get; private set; }
    }
}