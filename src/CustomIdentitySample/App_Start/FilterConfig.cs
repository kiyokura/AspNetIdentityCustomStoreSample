using System.Web.Mvc;

namespace CustomIdentitySample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // 規定でアクセスに認証が必要なようにしておく
            filters.Add(new AuthorizeAttribute());
        }
    }
}