using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShopping.Model.Models;
using OnlineShopping.Service;
using OnlineShopping.Web.Infrastructure.Core;
using OnlineShopping.Web.Infrastructure.Extentions;
using OnlineShopping.Web.Mappings;
using OnlineShopping.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShopping.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiBaseController
    {
       
        IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
          
            return CreateHttpResponse(request, () =>
            {

                var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
    }
}
