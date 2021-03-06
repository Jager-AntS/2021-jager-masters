using Sitecore.Diagnostics;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;
using SitecoreHackathon2021.Serialization.FieldSerializers;

namespace SitecoreHackathon2021.Pipelines.GetFieldSerializer
{
    public class GetInternalLinkFieldSerializer : BaseGetFieldSerializer
    {
        public GetInternalLinkFieldSerializer(IFieldRenderer fieldRenderer)
            : base(fieldRenderer)
        {
        }

        /// <summary>
        /// Set internal link serializer.
        /// </summary>
        /// <param name="args">Get field serializer pipeline args.</param>
        protected override void SetResult(GetFieldSerializerPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            args.Result = new InternalLinkFieldSerializer(args.ItemSerializer, FieldRenderer);
        }
    }
}