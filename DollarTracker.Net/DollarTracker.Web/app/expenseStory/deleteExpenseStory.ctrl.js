app.controller('deleteExpenseStoryCtrl', ['$modalInstance', '$scope', 'expenseStory', '$stateParams',
    function ($modalInstance, $scope, expenseStory, $stateParams) {

        $scope.delete = function () {
            $modalInstance.close(1);
        }
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
  }]);