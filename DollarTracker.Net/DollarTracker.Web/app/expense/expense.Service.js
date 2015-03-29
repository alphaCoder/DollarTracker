app.factory('expenseService', ['$http','dolt', function ($http, dolt) {
    var expenseService = {};
        
    expenseService.addExpense = function (expense) {
        var url = dolt.getApiUrl('addExpense');
        return $http.post(url, expense);
    }

    return expenseService;
}]);