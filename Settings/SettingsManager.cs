using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RevitGuide.Settings
{
    public class SettingsManager
    {
        private readonly Schema _tabSchema;
        public List<TabSetting> TabSettings { get; set; }

        public SettingsManager()
        {
            _tabSchema = GetTabSchema();
            TabSettings = GetTabSettings();
        }

        private Schema GetTabSchema()
        {
            Schema existingSchema = Schema.ListSchemas().FirstOrDefault(s => s.SchemaName == "TabSchema");
            if (existingSchema != null)
            {
                return existingSchema;
            }
            else
            {
                SchemaBuilder schemaBuilder = new SchemaBuilder(Guid.NewGuid());
                schemaBuilder.SetSchemaName("TabSchema");
                schemaBuilder.SetReadAccessLevel(AccessLevel.Public);
                schemaBuilder.SetWriteAccessLevel(AccessLevel.Public);

                schemaBuilder.AddArrayField("TabNames", typeof(string));
                schemaBuilder.AddArrayField("TabUrls", typeof(string));

                return schemaBuilder.Finish();
            }
        }

        public List<TabSetting> GetTabSettings()
        {
            Entity entity = App.Doc.ProjectInformation.GetEntity(_tabSchema);

            if (!entity.IsValid())
            {
                return new List<TabSetting>
                {
                    new TabSetting("First Tab", "")
                };
            }

            var tabNames = entity.Get<IList<string>>("TabNames").ToList();
            var tabUrls = entity.Get<IList<string>>("TabUrls").ToList();

            return tabNames.Zip(tabUrls, (name, url) => new TabSetting(name, url)).ToList();
        }

        public List<TriggerSetting> GetTriggerSettings()
        {

            return new List<TriggerSetting> { };
        }

        public void UpdateTabSettings(List<TabSetting> tabSettings)
        {
            TabSettings = tabSettings;
            App.ExEventHandler.Raise(SetTabSettings);
        }
        private void SetTabSettings()
        {

            Entity entity = App.Doc.ProjectInformation.GetEntity(_tabSchema);
            if (!entity.IsValid())
            {
                  entity = new Entity(_tabSchema);
            }

            using (Transaction t = new Transaction(App.Doc, "Set tab settings"))
            {
                t.Start();

                entity.Set<IList<string>>("TabNames", TabSettings.Select(x => x.TabName).ToList());
                entity.Set<IList<string>>("TabUrls", TabSettings.Select(x => x.TabUri).ToList());

                App.Doc.ProjectInformation.SetEntity(entity);
                t.Commit();
            }
        }
    }
}
