app.controller('newExpenseCtrl', ['$modalInstance', '$scope', 'expenseService', 'expenseStoryId',
    function ($modalInstance, $scope, expenseService, expenseStoryId) {
        $scope.expense = {};
        $scope.expense.title = 'Walmart';
        $scope.expense.amount = 100;
        $scope.expense.comments = 'Test 123';
        $scope.expense.expenseStoryId = expenseStoryId;
        
        $scope.ok = function (expense) {
            $modalInstance.close(expense);
        };

        $scope.expenseCategories = [
            { name: 'Gas', id: 'Gas' },
            { name: 'Groceries', id: 'Groceries' }
        ];

        $scope.selectedExpenseCategory = $scope.expenseCategories[0];
       
        $scope.create = function () {
            
            $scope.expense.expenseCategoryId = $scope.selectedExpenseCategory.id;
            expenseService.addExpense($scope.expense).then(function (result) {
                $scope.ok(result.data.data);
            }, function (reason) {
                console.log('error create fn in newExpenseCtrl');
                console.log(reason);
            });
        }
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);