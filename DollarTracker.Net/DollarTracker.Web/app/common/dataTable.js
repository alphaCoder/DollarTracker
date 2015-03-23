app.directive("dollarTrackerTable", [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            setTimeout(function () {
                var options = {};

                if (attrs['hideFilter']) {
                    options.searching = false;
                }
                element.DataTable(options);
            }, 0)
        }
    }
}]);