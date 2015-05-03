app.directive('expenseStoryCategoryGraph', [function () {
    return {
        restrict: "E",
        scope: {
            expensesStatsByCategory: '=',
            totalExpenses:'='
        },
        templateUrl: 'app/expenseStory/expenseStoryCategoryGraph.html?random=' + RANDOM
        
    }
}])


app.directive('progressBarColor', ['$parse',function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {
            var idx = parseInt(attrs['progressBarColor']);
               idx = idx%4;
                switch (idx) {
                    case 1:
                        el.addClass('progress-bar-green')
                        break;
                    case 2:
                        el.addClass('progress-bar-green')
                        break;
                    case 3:
                        el.addClass('progress-bar-red')
                        break;
                    case 4:
                    default:
                        el.addClass('progress-bar-aqua')
                        break;
                }
                    
            }

        }
    }
]);