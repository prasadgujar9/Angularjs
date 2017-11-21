(function () {
    angular.module('shopping.product', ['shopping.common']).config(config)
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product', {
            url: "/product",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('productAdd', {
            url: "/productAdd",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        });
    }
})();