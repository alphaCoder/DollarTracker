app.factory("dashboard",['$http','dolt', function ($http, dolt) {

    var dashboard = {};
    dashboard.getExpenseStories = function () {
        var url = dolt.getApiUrl('expenseStory');
        return $http.get(url);
    }

    dashboard.summary = function () {
        var url = dolt.getApiUrl('dashboard');
        return $http.get(url);
    }
    return dashboard;

}]);
