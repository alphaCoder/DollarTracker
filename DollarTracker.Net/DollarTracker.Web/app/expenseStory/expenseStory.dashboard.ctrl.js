app.controller('expenseStoryDashboardCtrl', ['$http', '$modal', '$scope', 'expenses', '$stateParams', 'expenseService',
    function ($http, $modal, $scope, expenses, $stateParams, expenseService) {

    $scope.expenses = expenses;

    //$scope.deleteExp = function () {
    //    alert('delete:');
    //}

    $scope.deleteExpense = function (expense) {
        
      expenseService.deleteExpense(expense.expenseId)
        .then(function (result) {
            var idx = $scope.expenses.indexOf(expense);
            $scope.expenses.splice(idx, 1);
            console.log('deleted successfully');
        }, function (reason) {
            console.log('error in expense card directive');
            console.log(reason);
        });
    }
    $scope.newExpense = function () {
        var modalInstance = $modal.open({
            templateUrl: 'app/expense/newExpenseModal.html',
            controller: 'newExpenseCtrl',
            size: 'sm',
            resolve: {
                expenseStoryId: function () { return $stateParams['id']; }
            }
        });

        modalInstance.result.then(function (expense) {
            $scope.expenses.push(expense);
        })
    }
    
    $scope.addCollaborator = function () {
        alert('add collaborator');
    }

}]);

app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('expenseStory', {
    url : '/expenseStory/{id}',
    templateUrl:'app/expenseStory/expenseStory.dashboard.html?r='+RANDOM,
    controller: 'expenseStoryDashboardCtrl',
    resolve: {
        expenses: ['expenseStory', '$stateParams', function (expenseStory, $stateParams) {
            var expenseStoryId = $stateParams['id'];
            return expenseStory.getAllExpenses(expenseStoryId).then(function (results) {
                console.log("getall expenses resolve:");
                console.log(results);
                return results.data.data;
            });
        }]
    }
    })
}]);