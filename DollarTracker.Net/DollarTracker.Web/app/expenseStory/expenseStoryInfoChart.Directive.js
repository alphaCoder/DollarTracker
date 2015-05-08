app.directive('expenseStoryInfoChart', function()  {
    return {
        restrict: 'E',
        templateUrl: 'app/expenseStory/expenseStoryInfoChart.html?r=' + RANDOM,
        controller: 'expenseStoryInfoChartCtrl',
        scope: {
            expensesStatsByCategory: '='
        }
    }
})

app.controller('expenseStoryInfoChartCtrl', ['$scope', function ($scope) {

    $scope.labels = [];
    $scope.data = [];
    angular.forEach($scope.expensesStatsByCategory, function (k, v) {
        console.log("k:" + k.label + ", v:" + k.value);
        $scope.labels.push(k.label);
        $scope.data.push(k.value);
    })
}])