app.factory('expenseService', ['$http', 'dolt', '$timeout', function ($http, dolt) {
    var expenseService = {};
    var cachedExpenseCategories;
    expenseService.addExpense = function (expense) {
        var url = dolt.getApiUrl('addExpense');
        return $http.post(url, expense);
    }

    expenseService.deleteExpense = function (expenseId) {
        var url = dolt.getApiUrl('expense') + '/' + expenseId;
        return $http.delete(url);
    }

    expenseService.expenseCategories = function () {
        if (!cachedExpenseCategories) {
            var url = dolt.getApiUrl('expenseCategory');
            $http.get(url).then(function (result) {
                cachedExpenseCategories = [];
                angular.forEach(result.data, function (obj, v) {
                    cachedExpenseCategories.push({ name: obj.description, id: obj.expenseCategoryId });
                });
                console.log("categories");
                console.log(JSON.stringify(cachedExpenseCategories));
                return cachedExpenseCategories;
            }, function (err) {
                console.log("error");
                console.log(err);
            });

        }
        return cachedExpenseCategories;
    }
    return expenseService;
}]);