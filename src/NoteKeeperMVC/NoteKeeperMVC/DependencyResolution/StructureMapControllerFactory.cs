using System;
using System.Web.Mvc;
using StructureMap;

namespace NK.Web.DependencyResolution
{
    /// <summary>
    /// SM controller factory used to work around the Api Controller issues with Structure Map
    /// </summary>
    public class StructureMapControllerFactory:DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new ArgumentException("controllerType");
            }

            return ObjectFactory.GetInstance(controllerType) as IController;
        }
    }
}