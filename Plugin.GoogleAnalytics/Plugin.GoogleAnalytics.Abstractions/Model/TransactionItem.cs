namespace Plugin.GoogleAnalytics.Abstractions.Model
{
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