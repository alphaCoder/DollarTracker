app.controller('expenseStoryDashboardCtrl', ['$http', '$modal', '$scope', function($http, $modal, $scope){

    $scope.newExpense = function () {
        var modalInstance = $modal.open({
            templateUrl: 'app/expense/newExpenseModal.html',
            controller: 'newExpenseCtrl',
            size: 'sm'
        });
    }
}]);

app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('expenseStory', {
    url : '/expenseStory/{id}',
    templateUrl:'app/expenseStory/expenseStory.dashboard.html?r='+RANDOM,
    controller:'expenseStoryDashboardCtrl'
    })
}]);