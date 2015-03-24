app.controller('createExpenseStoryCtrl', ['$modalInstance', '$scope', function ($modalInstance, $scope) {

    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
       // alert('cancel')
        $modalInstance.dismiss('cancel');
    };
}]);