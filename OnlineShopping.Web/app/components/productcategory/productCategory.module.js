(function () {
	angular.module('shopping.productCategory', ['shopping.common']).config(config)
	config.$inject = ['$stateProvider', '$urlRouterProvider'];
	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('productCategory', {
			url: "/productCategory",
			templateUrl: "/app/components/productcategory/productCategoryListView.html",
			controller: "productCategoryListController"
	
		});
	}
})();