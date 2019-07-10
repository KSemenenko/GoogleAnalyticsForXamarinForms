using System.IO;
using System.Xml;
#if ANDROID
using Android.Runtime;
#endif

#if __IOS__ || __MACOS__
using Foundation;
#endif

namespace Plugin.GoogleAnalytics
{
#if !WINDOWS_UWP
    [Preserve(AllMembers = true)]
#endif
    public sealed class ProductReceipt
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductType { get; set; }

        public static ProductReceipt Load(string receipt)
        {
            using(XmlReader reader = XmlReader.Create(new StringReader(receipt)))
            {
                if(reader.ReadToFollowing("ProductReceipt"))
                {
                    var result = new ProductReceipt();
                    result.Id = (string)reader.GetAttribute("Id");
                    result.ProductId = (string)reader.GetAttribute("ProductId");
                    result.ProductType = (string)reader.GetAttribute("ProductType");
                    return result;
                }
            }
            return null;
        }
    }
}