using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Microsoft.Web.WebView2.Wpf;
using RevitGuide.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RevitGuide.Commands
{
    public  class CommandBinder
    {
        private SettingsManager _settingsManager;
        public  WebView2 WebView { get; set; }
        private List<ItemSetting> _triggerSettings;

        public  Dictionary<PostableCommand, string> PostableCommandDict = new Dictionary<PostableCommand, string>()
        {
            { PostableCommand.Filters , "https://www.youtube.com/embed/_EGaTbs5olM" },
            { PostableCommand.VisibilityOrGraphics , "https://camilion.eu/en/blog/2021-revit-30-reasons-if-you-cant-see-an-object/" },
            { PostableCommand.ExportIFC , @"file:///U:/Kejun_L/_934_DT/230426_RevitAssist/pdfs/IFC.pdf" }
        };

        public CommandBinder(Document doc)
        {
            _settingsManager = new SettingsManager(doc);
            _triggerSettings = _settingsManager.TriggerSettings;
        }

        public  void Register()
        {
            try
            {
                foreach (ItemSetting triggerSetting in _triggerSettings)
                {
                    PostableCommand? postableCommand = triggerSetting.PostableCommand;
                    if (postableCommand == null)
                    {
                        continue;
                    }
                    RevitCommandId commandId = RevitCommandId.LookupPostableCommandId(postableCommand.Value);
                    UIControlledApplication uIControlledApplication = App.UICtrlApp;
                    AddInCommandBinding commandBinding = uIControlledApplication.CreateAddInCommandBinding(commandId);
                    
                    void BeforeCommandExecute(object sender, BeforeExecutedEventArgs e)
                    {
                        try
                        {
                            if (WebView != null)
                            {
                                string target = triggerSetting.ValidatedUri.ToString();
                                if (WebView.CoreWebView2.Source == null || WebView.CoreWebView2.Source.ToString() != target)
                                {
                                    WebView.CoreWebView2.Navigate(target);
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex);
                        }
                    }

                    commandBinding.BeforeExecuted += BeforeCommandExecute;
                    Debug.WriteLine($"registered {triggerSetting.Key}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
