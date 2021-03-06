using Sitecore.Diagnostics;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;
using SitecoreHackathon2021.Serialization.FieldSerializers;

namespace SitecoreHackathon2021.Pipelines.GetFieldSerializer
{
    public class GetMultilistFieldSerializer : BaseGetFieldSerializer
    {
        public GetMultilistFieldSerializer(IFieldRenderer fieldRenderer)
            : base(fieldRenderer)
        {
        }

        /// <summary>
        /// Set multilist serializer.
        /// </summary>
        /// <param name="args">Get field serializer pipeline args.</param>
        protected override void SetResult(GetFieldSerializerPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.IsNotNull(args.Field, $"{nameof(args.Field)} is null");
            Assert.IsNotNull(args.ItemSerializer, $"{nameof(args.ItemSerializer)} is null");

            args.Result = new MultilistFieldSerializer(args.ItemSerializer, FieldRenderer);
        }
    }
}