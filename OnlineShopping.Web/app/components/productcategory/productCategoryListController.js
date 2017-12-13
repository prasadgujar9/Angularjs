(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox,$filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCagories = getProductCagories;
        $scope.Keyword = "";

        $scope.search = search;
        $scope.DeleteProductCategory = DeleteProductCategory;
        $scope.deleteMultiple = deleteMultiple;
        $scope.selectAll = selectAll;
        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    listID: JSON.stringify(listId)
                }
            }
            $ngBootbox.confirm("Bạn có chắc muốn xóa " + listId.length + " này không ?").then(function () {
                apiService.del('api/productcategory/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                });
            })
          
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        function DeleteProductCategory(id)
        {
            $ngBootbox.confirm("Bạn có chắc muốn xóa").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/productcategory/delete', config, function () {
                    notificationService.displaySuccess("Xóa thành công !");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công !");
                });
            });
        }
        function search()
        {
           getProductCagories();
        }
        function getProductCagories(page) {
            page = page || 0;
            var config = {
                params: {
                    Keyword: $scope.Keyword,
                    page: page,
                    pageSize:10
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalRow == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
               
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalRow;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getProductCagories();
       
    }
})(angular.module('shopping.productCategory'));