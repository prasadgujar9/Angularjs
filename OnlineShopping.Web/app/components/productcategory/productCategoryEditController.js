(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams','commonService'];

    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams,commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.EditProductCategory = EditProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle()
        {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function LoadProductCategoryDetail()
        {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (result)
            {
                $scope.productCategory = result.data;
              
            },function (error)
            {
                notificationService.displayError(error.data);
            });
             
        }
        function EditProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('productCategory');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadParentCategory() {
            apiService.get('/api/productcategory/getallParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();
        LoadProductCategoryDetail();
    }


})(angular.module('shopping.productCategory'))