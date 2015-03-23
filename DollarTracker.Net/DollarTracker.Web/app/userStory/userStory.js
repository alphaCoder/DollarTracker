app.directive('userStoryCard', function () {
    return {
        restrict: "E",
        templateUrl: 'app/userStory/userStory.html',
        scope: {
            userStory: "="
        } //todo: need to add some stuff
    };
})