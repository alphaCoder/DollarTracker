app.directive('expenseStoryCategoryGraph', [function () {
    return {
        restrict: "E",
        scope: {
            stats: '='
        },
        templateUrl: 'app/expenseStory/expenseStoryCategoryGraph.html?random=' + RANDOM
        
    }
}])
