(function () {
	angular.module('shopping.productCategory', ['shopping.common']).config(config)
	config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];
	function config($stateProvider, $urlRouterProvider, $locationProvider) {
	    $stateProvider.state('productCategory', {
	        url: "/productCategory",
	        templateUrl: "/app/components/productcategory/productCategoryListView.html",
	        controller: "productCategoryListController"
	    }).state('AddproductCategory', {
	        url: "/AddproductCategory",
	        templateUrl: "/app/components/productcategory/productCategoryAddView.html",
	        controller: "productCategoryAddController"
	    }).state('EditproductCategory', {
	        url: "/EditproductCategory/:id",
	        templateUrl: "/app/components/productcategory/productCategoryEditView.html",
	        controller: "productCategoryEditController"
	    });
	   // $locationProvider.html5Mode(true);
	}
	
})();