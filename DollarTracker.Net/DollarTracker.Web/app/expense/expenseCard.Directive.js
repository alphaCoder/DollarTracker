app.directive('expenseCard', function () {
    return {
        restrict: "E",
        templateUrl: 'app/expense/expenseCard.html',
        scope: {
            expense: "=",
            deleteExpense: "&"
        }
    };
})