app.directive('expenseStoryCard', function () {
    return {
        restrict: "E",
        templateUrl: 'app/expenseStory/expenseStoryCard.html',
        scope: {
            expenseStory: "="
        } //todo: need to add some stuff
    };
})