using Sitecore.Data.Items;
using Sitecore.Links;

namespace SitecoreHackathon2021.Extensions
{
    public static class ItemExtensions
    {
        public static string GetItemUrl(this Item item)
        {
            return LinkManager.GetItemUrl(item);
        }
    }
}