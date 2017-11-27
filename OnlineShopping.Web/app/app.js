/// <reference path="D:\hoc\AngularJS\Git\OnlineShopping.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shopping',
        [
         'shopping.product',
         'shopping.productCategory',
         'shopping.common'
        ]).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];  
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise("/admin");
    }
})();