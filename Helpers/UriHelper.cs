using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RevitGuide.Helpers
{
    public class UriHelper
    {
        public static Uri FirstPageUri = new Uri(App.DataFolderPath23 + "first_page.html");
        public static Uri InvalidPageUri = new Uri(App.DataFolderPath23 + "invalid_page.html");
        public static Uri LiveGuidePageUri = new Uri(App.DataFolderPath23 + "live_guide_page.html");

        public static Uri StringToUri(string uriString)
        {
            if (File.Exists(uriString))
            {
                return new Uri(uriString);
            }

            if (uriString == "")
            {
                return FirstPageUri;
            }
            else if (Uri.TryCreate(uriString, UriKind.Absolute, out Uri result))
            {
                return result;
            }
            else if (Uri.TryCreate("https://" + uriString, UriKind.Absolute, out result))
            {
                return result;
            }
            else if (Uri.TryCreate("http://" + uriString, UriKind.Absolute, out result))
            {
                return result;
            }
            else
            {
                return InvalidPageUri;
            }

        }
    }
}
