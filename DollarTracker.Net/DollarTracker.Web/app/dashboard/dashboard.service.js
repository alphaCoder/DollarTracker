app.factory("dashboard",['$http','dolt', function ($http, dolt) {

    var dashboard = {};
    dashboard.getExpenseStories = function () {
        var userId = '';
        var url = dolt.getApiUrl('expenseStory');
        return $http.get(url);
    }

}]);