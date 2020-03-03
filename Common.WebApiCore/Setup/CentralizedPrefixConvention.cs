using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Common.WebApiCore.Setup
{
    public class CentralizedPrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralizedPrefix;

        public CentralizedPrefixConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            this._centralizedPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        public void Apply(ApplicationModel app)
        {
            foreach (var controller in app.Controllers)
            {
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();

                foreach (var selectorModel in matchedSelectors)
                {
                    selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(this._centralizedPrefix, selectorModel.AttributeRouteModel);
                }

                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                foreach (var selectorModel in unmatchedSelectors)
                {
                    selectorModel.AttributeRouteModel = this._centralizedPrefix;
                }
            }
        }
    }
}