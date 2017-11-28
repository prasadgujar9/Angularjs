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
        public HttpResponseMessage GetAll(HttpRequestMessage request,int page,int pageSize = 20)
        {
          
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _productCategoryService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x=>x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(query);
              
                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    TotalRow = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Page = page
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
    }
}
