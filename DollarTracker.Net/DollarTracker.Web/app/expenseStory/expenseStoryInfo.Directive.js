app.directive('expenseStoryInfo', [function () {
    return {
        restrict: "E",
        scope: {
            expenseStory: '=',
            expensesStatsByCategory: '=',
            totalExpenses: '=',
            deleteExpenseStorySummary: '&'
        },
        templateUrl: 'app/expenseStory/expenseStoryInfo.html?random=' + RANDOM,
        controller: 'expenseStoryInfoCtrl'
    }
}])
.controller('expenseStoryInfoCtrl', ['$scope', '$modal', 'expenseStory', function ($scope, $modal, expenseStory) {
    $scope.deleteExpenseStory = function (story) {
        var modalInstance = $modal.open({
            templateUrl: 'app/expenseStory/deleteExpenseStoryModal.html?storyId='+story.expenseStoryId,
            controller: 'deleteExpenseStoryCtrl',
            size: 'sm'
        });

        modalInstance.result.then(function (data) {
            if (data == 1) {
                expenseStory.deleteExpenseStory(story.expenseStoryId).then(function (result) {
                    $scope.deleteExpenseStorySummary();
                }, function (err) {
                    console.log('an error occured', err);
                });
            }
        });
    }
}]);
