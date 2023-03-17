using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiRuleta
{
   // <httpProtocol>
		 // <customHeaders>
			//  <add name = "Access-Control-Allow-Origin" value="*" />
			//  <add name = "Access-Control-Allow-Headers" value="*" />
			//  <add name = "Access-Control-Allow-Methods" value="*" />
		 // </customHeaders>
	  //</httpProtocol>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Rutas de Web API
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
