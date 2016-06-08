//using GoogleAnalytics.Core;

//namespace GoogleAnalytics
//{
//    public static class TransactionBuilder
//    {
//        static TransactionBuilder()
//        {
//            StoreName = "Windows Phone 8 App Store";
//        }

//        /// <summary>
//        /// Gets or sets the default strore name to be used when logging transactions
//        /// </summary>
//        public static string StoreName { get; set; }

//        /// <summary>
//        /// Constructs a transaction object with a transaction item for a product purchase
//        /// </summary>
//        /// <param name="listingInformation">The product listing information for the app</param>
//        /// <param name="receipt">The receipt from the purchase</param>
//        /// <returns>A transaction object all ready to get passed to Tracker.sendTransaction</returns>
//        public static Transaction GetProductPurchaseTransaction(ListingInformation listingInformation, string receipt)
//        {
//            var productReceipt = ProductReceipt.Load(receipt);
//            var transactionId = productReceipt.Id;
//            var productId = productReceipt.ProductId;
//            var productType = productReceipt.ProductType;

//            var product = listingInformation.ProductListings[productId];
//            var regionInfo = System.Globalization.RegionInfo.CurrentRegion;
//            var currencyCode = regionInfo.ISOCurrencySymbol;
//            var cost = double.Parse(product.FormattedPrice, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture);
//            var costInMicrons = (long)(cost * 1000000);

//            var transaction = new Transaction(transactionId, costInMicrons);
//            transaction.Affiliation = StoreName;
//            transaction.CurrencyCode = currencyCode;
//            var transactionItem = new TransactionItem(productId, product.Name, costInMicrons, 1);
//            transactionItem.Category = productType;
//            transaction.Items.Add(transactionItem);
//            return transaction;
//        }

//        /// <summary>
//        /// Constructs a transaction object with a transaction item for an app purchase
//        /// </summary>
//        /// <param name="listingInformation">The product listing information for the app</param>
//        /// <param name="receipt">The receipt from the purchase</param>
//        /// <returns>A transaction object all ready to get passed to Tracker.sendTransaction</returns>
//        public static Transaction GetAppPurchaseTransaction(ListingInformation listingInformation, string receipt)
//        {
//            var appReceipt = AppReceipt.Load(receipt);
//            var transactionId = appReceipt.Id;
//            var appId = appReceipt.AppId;
//            var licenseType = appReceipt.LicenseType;

//            var regionInfo = System.Globalization.RegionInfo.CurrentRegion;
//            var currencyCode = regionInfo.ISOCurrencySymbol;
//            var cost = double.Parse(listingInformation.FormattedPrice, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture);
//            var costInMicrons = (long)(cost * 1000000);

//            var transaction = new Transaction(transactionId, costInMicrons);
//            transaction.Affiliation = StoreName;
//            transaction.CurrencyCode = currencyCode;
//            var transactionItem = new TransactionItem(appId, listingInformation.Name, costInMicrons, 1);
//            transactionItem.Category = licenseType;
//            transaction.Items.Add(transactionItem);
//            return transaction;
//        }
//    }
//}

