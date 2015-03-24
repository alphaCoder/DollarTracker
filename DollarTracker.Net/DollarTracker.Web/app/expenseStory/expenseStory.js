app.directive('expenseStoryCard', function () {
    return {
        restrict: "E",
        templateUrl: 'app/expenseStory/expenseStory.html',
        scope: {
            expenseStory: "="
        } //todo: need to add some stuff
    };
})