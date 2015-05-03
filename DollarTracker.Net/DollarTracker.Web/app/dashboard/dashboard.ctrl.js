app.controller("dashboardCtrl", ['$http', 'dashboard', '$scope', '$modal', 'user', 'summary',
    function ($http, $dashboard, $scope, $modal, user, summary) {

    $scope.user = user.getUser();

    $scope.summary = summary;
    console.log('summary:');
    console.log(JSON.stringify(summary));
    console.log('dashboard scope');
   // console.log($scope);
    $scope.newExpenseStory = function () {

      var modalInstance =  $modal.open({
            templateUrl: 'app/expenseStory/createExpenseStoryModal.html',
            controller: 'createExpenseStoryCtrl',
            size: 'md'
      });

      modalInstance.result.then(function (result) {

          console.log('added new story successfully:');
          console.log(JSON.stringify(result.data));
          $scope.summary.expenseStorySummaries.push(result.data);
      });
    }

    $scope.handleDeletedExpenseStorySummary = function (story) {
        var idx = $scope.summary.expenseStorySummaries.indexOf(story);
        $scope.summary.expenseStorySummaries.splice(idx, 1);
        console.log('deleted successfully');
        console.log(JSON.stringify(story));
    }
 }]);