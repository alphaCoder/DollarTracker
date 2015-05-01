app.controller("dashboardCtrl", ['$http', 'dashboard', '$scope', 'expenseStories','$modal', 'user', 'summary',
    function ($http, $dashboard, $scope, expenseStories, $modal, user, summary) {

    $scope.expenseStories = expenseStories;
    $scope.user = user.getUser();

    $scope.summary = summary;
    console.log('dashboard scope');
    console.log($scope);
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