app.directive('expenseCard', function () {
    return {
        restrict: "E",
        templateUrl: 'app/expense/expenseCard.html',
        scope: {
            expense: "="
        } //todo: need to add some stuff
    };
})