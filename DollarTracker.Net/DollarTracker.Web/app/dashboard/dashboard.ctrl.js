app.controller("dashboardCtrl", ['$http', 'dashboard', '$scope', 'expenseStories','$modal',
    function ($http, $dashboard, $scope, expenseStories, $modal) {

    $scope.expenseStories = expenseStories;

    $scope.newExpenseStory = function () {

      var modalInstance =  $modal.open({
            templateUrl: 'app/expenseStory/createExpenseStoryModal.html',
            controller: 'createExpenseStoryCtrl',
            size: 'md'
      });

      modalInstance.result.then(function (stories) {
          if (!$scope.expenseStories) stories = $scope.expenseStories;
          $.each(stories, function (i, v) {
              $scope.expenseStories.push(v);
          });
      });
    }
}]);