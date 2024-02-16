using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using RevitGuide.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitGuide.Helpers
{
    public class SettingsHelper
    {
        private Schema _tabSchema;
        private Schema _triggerSchema;
        private Document _doc;

        public SettingsHelper(Document doc)
        {
            _doc = doc;
            _tabSchema = GetSchema("TabSchema");
            _triggerSchema = GetSchema("TriggerSchema");
        }

        public List<ItemSetting> GetTabSettings()
        { 
            return GetSettings(_tabSchema, new ItemSetting("First Tab",""));
        }

        public List<ItemSetting> GetTriggerSettings()
        {
            return GetSettings(_triggerSchema);
        }

        public void UpdateAllSettings(List<ItemSetting> tabSettings, List<ItemSetting> triggerSettings)
        {
            using (Transaction t = new Transaction(_doc, "Save Revit Guide settings"))
            {
                t.Start();
                SetSettings(_tabSchema, tabSettings);
                SetSettings(_triggerSchema, triggerSettings);
                t.Commit();
            }
        }

        private void SetSettings(Schema schema, List<ItemSetting> settings)
        {
            Entity entity = _doc.ProjectInformation.GetEntity(schema);
            if (!entity.IsValid())
            {
                entity = new Entity(schema);
            }

            entity.Set<IList<string>>("Keys", settings.Select(x => x.Key).ToList());
            entity.Set<IList<string>>("Uris", settings.Select(x => x.Uri).ToList());
            _doc.ProjectInformation.SetEntity(entity);
        }

        private List<ItemSetting> GetSettings(Schema schema, ItemSetting defaultSetting = null)
        {
            Entity entity = _doc.ProjectInformation.GetEntity(schema);

            if (!entity.IsValid())
            {
                if (defaultSetting == null)
                {
                    return new List<ItemSetting> { };
                }
                else
                {
                    return new List<ItemSetting> { defaultSetting };
                }
            }
            List<ItemSetting> settings = new List<ItemSetting> { };
            var entityKeys = entity.Get<IList<string>>("Keys").ToList();
            var entityUris = entity.Get<IList<string>>("Uris").ToList();
            foreach (var item in entityKeys.Zip(entityUris, (key, uri) => (key, uri)))
            {
                settings.Add(new ItemSetting(item.key, item.uri));
            }
            return settings;
        }

        private Schema GetSchema(string schemaName)
        {
            Schema existingSchema = Schema.ListSchemas().FirstOrDefault(s => s.SchemaName == schemaName);
            if (existingSchema != null)
            {
                return existingSchema;
            }
            else
            {
                SchemaBuilder schemaBuilder = new SchemaBuilder(Guid.NewGuid());
                schemaBuilder.SetSchemaName(schemaName);
                schemaBuilder.SetReadAccessLevel(AccessLevel.Public);
                schemaBuilder.SetWriteAccessLevel(AccessLevel.Public);
                schemaBuilder.AddArrayField("Keys", typeof(string));
                schemaBuilder.AddArrayField("Uris", typeof(string));
                return schemaBuilder.Finish();
            }
        }
    }
}
