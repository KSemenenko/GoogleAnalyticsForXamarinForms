using System.Collections.Generic;

namespace Plugin.GoogleAnalytics
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

    public sealed class TransactionItem
    {
        public TransactionItem()
        {
        }

        public TransactionItem(string sku, string name, long priceInMicros, long quantity)
        {
            Name = name;
            PriceInMicros = priceInMicros;
            Quantity = quantity;
            SKU = sku;
        }

        public TransactionItem(string transactionId, string sku, string name, long priceInMicros, long quantity)
        {
            TransactionId = transactionId;
            Name = name;
            PriceInMicros = priceInMicros;
            Quantity = quantity;
            SKU = sku;
        }

        public string Name { get; set; }
        public long PriceInMicros { get; set; }
        public long Quantity { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public string TransactionId { get; set; }
        public string CurrencyCode { get; set; }
    }
}