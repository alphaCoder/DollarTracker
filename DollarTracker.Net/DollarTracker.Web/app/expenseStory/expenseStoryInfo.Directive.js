app.directive('expenseStoryInfo', [function () {
    return {
        restrict: "E",
        scope: {
            expenseStory: '='
        },
        templateUrl: 'app/expenseStory/expenseStoryInfo.html?random=' + RANDOM,
        controller: 'expenseStoryInfoCtrl'
    }
}])
.controller('expenseStoryInfoCtrl', ['$scope', function ($scope) {

}]);
