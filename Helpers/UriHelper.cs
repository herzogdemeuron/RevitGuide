using System;
using System.IO;
using System.Reflection;

namespace RevitGuide.Helpers
{
    public class UriHelper
    {
        private static string dataDirectory = App.DataFolderPath;
        public static Uri IconUri = new Uri("pack://application:,,,/RevitGuide;component/Resources/icon.png", UriKind.Absolute);
        public static Uri FirstPageUri = new Uri(Path.Combine(dataDirectory,"first_page.html"));
        public static Uri InvalidPageUri = new Uri(Path.Combine(dataDirectory,"invalid_page.html"));
        public static Uri LiveGuidePageUri = new Uri(Path.Combine(dataDirectory,"live_guide_page.html"));
        
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

        public static void OpenGithubPage()
        {
            string url = "https://github.com/herzogdemeuron/revit-guide";
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") { CreateNoWindow = true }
                );
        }
    }
}
