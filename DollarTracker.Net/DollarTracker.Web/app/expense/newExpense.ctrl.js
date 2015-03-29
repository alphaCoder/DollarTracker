app.controller('newExpenseCtrl', ['$modalInstance', '$scope', 
    function ($modalInstance, $scope) {

        $scope.ok = function (expense) {
            $modalInstance.close(expense);
        };

        $scope.expenseCategories = [
            { name: 'Meals', id: 'Meals' },
            { name: 'Taxi', id: 'Taxi' }
        ];

        $scope.expense = {};
        $scope.selectedExpenseCategory = $scope.expenseCategories[0];
        $scope.create = function () {
            //expenseStory.Add($scope.expenseStory).then(function (result) {
            //    console.log('add expenseStory success');
            //    console.log(result.data);
            //    $scope.ok(result.data);
            //}, function (reason) {
            //    console.log('error create fn in createExpenseStoryCtrl');
            //    console.log(reason);
            //});
        }
        $scope.cancel = function () {
            // alert('cancel')
            $modalInstance.dismiss('cancel');
        };
    }]);