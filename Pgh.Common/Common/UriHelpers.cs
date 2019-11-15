using Microsoft.AspNetCore.Mvc;
using Pgh.Common.Enumeration;

namespace Pgh.Common.Common
{
    public class UriHelpers
    {
        protected IUrlHelper UrlHelper { get; set; }
        public UriHelpers(IUrlHelper urlHelper)
        {
            UrlHelper = urlHelper;
        }


        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceUriType type, string methodName)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:

                    return UrlHelper.Action(methodName, new
                    {
                        pageNumber = resourceParameters.PageNumber - 1,
                        pageSize = resourceParameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return UrlHelper.Action(methodName, new
                    {
                        pageNumber = resourceParameters.PageNumber + 1,
                        pageSize = resourceParameters.PageSize
                    });
                default:
                    return UrlHelper.Action(methodName, new
                    {
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize
                    });
            }
        }
    }
}