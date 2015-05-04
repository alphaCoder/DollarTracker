app.controller('createExpenseStoryCtrl', ['$modalInstance', '$scope', 'expenseStory',
    function ($modalInstance, $scope, expenseStory) {
        
    $scope.ok = function (story) {
        $modalInstance.close(story);
    };

    $scope.storyTypes = [
        { name: 'Personal', id: 'Personal' },
        { name: 'Shared', id: 'Shared' }
    ];

    $scope.expenseStory = {};
    $scope.selectedStoryType = $scope.storyTypes[0];
    $scope.expenseStory.expenseStoryTypeId = $scope.selectedStoryType.id;
    $scope.create = function () {
        expenseStory.Add($scope.expenseStory).then(function (result) {
            console.log('add expenseStory success');
            console.log(result.data);
            $scope.ok(result.data);
        }, function (reason) {
            console.log('error create fn in createExpenseStoryCtrl');
            console.log(reason);
        });
    }
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
}]);