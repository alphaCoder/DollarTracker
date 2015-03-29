app.controller('expenseStoryDashboardCtrl', ['$http', '$modal', '$scope', 'expenses', '$stateParams',
    function ($http, $modal, $scope, expenses, $stateParams) {

    $scope.expenses = expenses;
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