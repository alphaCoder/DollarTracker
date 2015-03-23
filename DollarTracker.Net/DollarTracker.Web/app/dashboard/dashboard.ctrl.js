app.controller("dashboardCtrl", ['$http', 'dashboard', '$scope', 'expenseStories', function ($http, $dashboard, $scope, expenseStories) {

    $scope.expenseStories = expenseStories;
    console.log("--1--");
    console.log(expenseStories);
}]);