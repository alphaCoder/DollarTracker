app.controller("dashboardCtrl", ['$http', 'dashboard', '$scope', 'expenseStories','$modal',
    function ($http, $dashboard, $scope, expenseStories, $modal) {

    $scope.expenseStories = expenseStories;
    console.log("--1--");
    console.log(expenseStories);

    $scope.newExpenseStory = function () {

        $modal.open({
            templateUrl: 'app/expenseStory/createExpenseStoryModal.html',
            controller: 'createExpenseStoryCtrl',
            size: 'md'
        });
        console.log('new expense story')
    }
}]);