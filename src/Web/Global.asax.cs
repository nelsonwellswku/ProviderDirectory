using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Octogami.ProviderDirectory.Application;
using WebApi.StructureMap;

namespace Octogami.ProviderDirectory.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			// Configure StructureMap for WebAPI
			GlobalConfiguration.Configuration.UseStructureMap<ApplicationRegistry>();
		}
	}
}