'use strict';

app.factory('authToken', ['$window', function ($window) {
    var storage = $window.localStorage;
    var cachedToken;
    var userToken = 'satellizer_token';
    var authToken = {
        getToken: function () {
            if (!cachedToken) {
                cachedToken = storage.getItem(userToken);
            }
            return cachedToken;
        }
    }

    return authToken;
}]);
