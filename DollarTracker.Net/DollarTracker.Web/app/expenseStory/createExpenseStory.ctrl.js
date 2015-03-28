app.controller('createExpenseStoryCtrl', ['$modalInstance', '$scope', 'expenseStory',
    function ($modalInstance, $scope, expenseStory) {
    
        var newStories = [];
    $scope.ok = function () {
        $modalInstance.close(newStories);
    };

    $scope.storyTypes = [
        { name: 'Personal', id: 'Personal' },
        { name: 'Shared', id: 'Shared' }
    ];

    $scope.expenseStory = {};
    $scope.selectedStoryType = $scope.storyTypes[0];
    $scope.expenseStory.expenseStoryTypeId = $scope.selectedStoryType.id;
    $scope.create = function () {
        console.log('create expense story');
        console.log($scope.expenseStory);

        expenseStory.Add($scope.expenseStory).then(function (result) {
            console.log('add expenseStory success');
            console.log(result.data);
            newStories.push(result.data);
            $scope.ok();
        }, function (reason) {
            console.log('error create fn in createExpenseStoryCtrl');
            console.log(reason);

        });
    }
    $scope.cancel = function () {
       // alert('cancel')
        $modalInstance.dismiss('cancel');
    };
}]);