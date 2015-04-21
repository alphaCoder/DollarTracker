app.factory('authInterceptor', ['$injector', '$q', 'dataCleanup', function ($injector, $q, dataCleanup) {

    var checkStatus = function (status) {
        var state = $injector.get('$state');
        if (status >= 400) {
            if (status == 404) {
                state.go('err.404');
            } else if (status == 401) {
                dataCleanup.cleanAll();
                state.go('login');
            } else {
                state.go('err.oops');
            }
        }
    }

    return {
        response: function (response) {
            checkStatus(response.status);
            return response || $q.when(response);
        },
        responseError: function (rejection) {
            checkStatus(rejection.status);
            return $q.reject(rejection);
        }
    }

}])
