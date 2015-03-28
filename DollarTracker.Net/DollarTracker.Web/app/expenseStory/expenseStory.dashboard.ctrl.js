app.controller('expenseStoryDashboardCtrl', ['$http', function($http){

}]);

app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('expenseStory', {
    url : '/expenseStory/{id}',
    templateUrl:'app/expenseStory/expenseStory.dashboard.html?r='+RANDOM,
    controller:'expenseStoryDashboardCtrl'
    })
}]);