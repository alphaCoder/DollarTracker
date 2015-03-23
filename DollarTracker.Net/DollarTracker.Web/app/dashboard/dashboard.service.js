app.factory("dashboard",['$http','dolt', function ($http, dolt) {

    var dashboard = {};
    dashboard.getExpenseStories = function () {
        var userId = '';
        var url = dolt.getApiUrl('expenseStory');
        alert(url);
        return $http.get(url);
    }
    return dashboard;

}]);

app.factory("dashboardInitialData", ['$q', 'dashboard', function ($q, dashboard) {

    var expenseStories = dashboard.getExpenseStories();
    return $q.all([expenseStories]).then(function (results) {
        console.log('---results--');
        console.log(results);
        return {
            expenseStories: results.data
        };
    });
}]);