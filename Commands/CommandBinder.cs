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
        private List<ItemSetting> _triggerSettings;
        public  WebView2 WebView { get; set; }

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
                                if (WebView.CoreWebView2.Source == null || 
                                    WebView.CoreWebView2.Source.ToString().Replace("www.","").Replace("WWW.", "") != target.Replace("www.", "").Replace("WWW.", ""))
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
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
