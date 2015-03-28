app.controller("dashboardCtrl", ['$http', 'dashboard', '$scope', 'expenseStories','$modal',
    function ($http, $dashboard, $scope, expenseStories, $modal) {

    $scope.expenseStories = expenseStories;

    $scope.newExpenseStory = function () {

      var modalInstance =  $modal.open({
            templateUrl: 'app/expenseStory/createExpenseStoryModal.html',
            controller: 'createExpenseStoryCtrl',
            size: 'md'
      });

      modalInstance.result.then(function (story) {
          $scope.expenseStories.push(story);
          console.log('total expense stories:' + $scope.expenseStories.length);
      });
    }
}]);