app.controller('createExpenseStoryCtrl', ['$modalInstance', '$scope', function ($modalInstance, $scope) {

    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.storyTypes = [
        { name: 'Personal', id: 'personal' },
        { name: 'Shared', id: 'shared' }
    ];

    $scope.cancel = function () {
       // alert('cancel')
        $modalInstance.dismiss('cancel');
    };
}]);