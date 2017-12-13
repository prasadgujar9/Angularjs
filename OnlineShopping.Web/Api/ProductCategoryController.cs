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
using System.Web.Script.Serialization;

namespace OnlineShopping.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiBaseController
    {
        #region intialize
        IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }
        #endregion



        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage    request, string Keyword, int page, int pageSize = 20)
        {

            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _productCategoryService.GetAll(Keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
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

        [Route("getallParents")]
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

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
               
                var oldProductCategory = _productCategoryService.Delete(id);
                _productCategoryService.Save();
                var responseData = Mapper.Map<ProductCategory, ProductCategory>(oldProductCategory);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage deleteMulti(HttpRequestMessage request, string listID)
        {
            HttpResponseMessage response = null;

            try
            {
                var ids = new JavaScriptSerializer().Deserialize<List<int>>(listID);
                foreach (var item in ids)
                {
                    _productCategoryService.Delete(item);
                }
                _productCategoryService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, ids.Count());
            }
            catch (Exception ex)
            {

                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
                var newProductCategory = new ProductCategory();
                newProductCategory.UpdateProductCategory(productCategoryVM);
                newProductCategory.CreatedDate = DateTime.Now;
                _productCategoryService.Add(newProductCategory);
                _productCategoryService.Save();
                var responseData = Mapper.Map<ProductCategory, ProductCategory>(newProductCategory);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }

        [Route("getbyid/{id:int}")]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int Id)
        {

            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(Id);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
                var DbProductCategory = _productCategoryService.GetById(productCategoryVM.ID);
                DbProductCategory.UpdateProductCategory(productCategoryVM);
                DbProductCategory.UpdatedDate = DateTime.Now;
                _productCategoryService.Update(DbProductCategory);
                _productCategoryService.Save();
                var responseData = Mapper.Map<ProductCategory, ProductCategory>(DbProductCategory);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }
    }
}
