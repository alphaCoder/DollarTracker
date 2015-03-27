app.factory("expenseStory", ['$http', 'dolt', function ($http, dolt) {

    var expenseStory = {};
    expenseStory.Add = function (story) {
        var url = dolt.getApiUrl('addExpenseStory');
        return $http.post(url, story);
    }
    return expenseStory;
}]);