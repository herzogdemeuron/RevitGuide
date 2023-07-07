using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;

namespace RevitGuide
{
    [Transaction(TransactionMode.Manual)]
    public class ExternalEventHandler : IExternalEventHandler
    {
        private readonly ExternalEvent exEvent;
        private Action<UIApplication> ActionToExecute { get; set; }

        public ExternalEventHandler()
        {
            exEvent = ExternalEvent.Create(this);
        }

        public void Execute(UIApplication uiapp)
        {
            try
            {
                ActionToExecute?.Invoke(uiapp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public void RaiseWithUIApp(Action<UIApplication> actionToExecute)
        {
            ActionToExecute = actionToExecute;
            exEvent.Raise();
        }

        public void Raise(Action actionToExecute)
        {
            ActionToExecute = (uiapp) => actionToExecute();
            exEvent.Raise();
        }

        public string GetName()
        {
            return "GeneralEventHandler";
        }
    }
}
