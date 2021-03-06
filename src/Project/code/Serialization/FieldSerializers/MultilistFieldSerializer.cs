using Newtonsoft.Json;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.ItemSerializers;
using SitecoreHackathon2021.Extensions;

namespace SitecoreHackathon2021.Serialization.FieldSerializers
{
    public class MultilistFieldSerializer : Sitecore.LayoutService.Serialization.FieldSerializers.MultilistFieldSerializer
    {
        public MultilistFieldSerializer(IItemSerializer itemSerializer, IFieldRenderer fieldRenderer)
            : base(itemSerializer, fieldRenderer)
        {
        }

        /// <summary>
        /// Serialize value from sitecore multilist field to json response.
        /// </summary>
        /// <param name="field">Sitecore field.</param>
        /// <param name="writer">Json text writer.</param>
        public override void Serialize(Field field, JsonTextWriter writer)
        {
            Assert.ArgumentNotNull(field, nameof(field));
            Assert.ArgumentNotNull(writer, nameof(writer));

            using (var recursionLimit = new RecursionLimit($"{GetType().FullName}|{field.Item.ID}|{field.ID}", 1))
            {
                if (recursionLimit.Exceeded)
                {
                    return;
                }

                Item[] items = ((MultilistField)field).GetItems();
                if (items == null || items.Length == 0)
                {
                    writer.WritePropertyName(field.Name);
                    writer.WriteStartArray();
                    writer.WriteEndArray();
                }
                else
                {
                    writer.WritePropertyName(field.Name);
                    writer.WriteStartArray();
                    foreach (Item targetItem in items)
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("id");
                        writer.WriteValue(targetItem.ID.Guid.ToString());

                        if (targetItem.Versions.Count > 0 && !string.IsNullOrWhiteSpace(targetItem.Fields[FieldIDs.LayoutField]?.Value))
                        {
                            writer.WritePropertyName("url");
                            writer.WriteValue(targetItem.GetItemUrl());
                        }

                        writer.WritePropertyName("fields");
                        writer.WriteRawValue(ItemSerializer.Serialize(targetItem));
                        writer.WriteEndObject();
                    }

                    writer.WriteEndArray();
                }
            }
        }
    }
}