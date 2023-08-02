using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RevitGuide.Commands
{
    public static class CommandBinder
    {
        public static WebView2 WebView = null;

        public static Dictionary<PostableCommand, string> PostableCommandDict = new Dictionary<PostableCommand, string>()
        {
            { PostableCommand.Filters , "https://www.youtube.com/embed/_EGaTbs5olM" },
            { PostableCommand.VisibilityOrGraphics , "https://camilion.eu/en/blog/2021-revit-30-reasons-if-you-cant-see-an-object/" },
            { PostableCommand.ExportIFC , @"file:///U:/Kejun_L/_934_DT/230426_RevitAssist/pdfs/IFC.pdf" }
        };

        public static void Register()
        {
            try
            {
                foreach (KeyValuePair<PostableCommand, String> item in PostableCommandDict)
                {
                    RevitCommandId commandId = RevitCommandId.LookupPostableCommandId(item.Key);
                    UIControlledApplication uIControlledApplication = App.UICtrlApp;
                    AddInCommandBinding commandBinding = uIControlledApplication.CreateAddInCommandBinding(commandId);
                    
                    void BeforeCommandExecute(object sender, BeforeExecutedEventArgs e)
                    {
                        TaskDialog.Show("test", "test");
                        /* try
                         {
                             if (WebView != null)
                             {
                                 Debug.WriteLine("trying navigate");
                                 WebView.CoreWebView2.Navigate(item.Value);
                             }

                         }
                         catch (Exception ex)
                         {

                             Debug.WriteLine(ex);
                         }*/
                    }

                    commandBinding.BeforeExecuted += BeforeCommandExecute;
                    Debug.WriteLine("registered");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
