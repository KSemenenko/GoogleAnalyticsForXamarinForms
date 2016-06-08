using System.IO;
using System.Xml;

namespace GoogleAnalytics.Core
{
    public sealed class AppReceipt
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string LicenseType { get; set; }

        public static AppReceipt Load(string receipt)
        {
            using(XmlReader reader = XmlReader.Create(new StringReader(receipt)))
            {
                if(reader.ReadToFollowing("AppReceipt"))
                {
                    var result = new AppReceipt();
                    result.Id = (string)reader.GetAttribute("Id");
                    result.AppId = (string)reader.GetAttribute("AppId");
                    result.LicenseType = (string)reader.GetAttribute("LicenseType");
                    return result;
                }
            }
            return null;
        }
    }
}