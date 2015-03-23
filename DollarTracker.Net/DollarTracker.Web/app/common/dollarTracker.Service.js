app.factory('dolt', [function () {

    var dolt = {};
    dolt.baseUrl = "http://localhost:54973/api/"
    dolt.getApiUrl = function (s) {
        // strip any leading forward slash
        if (s.charAt(0) == '/') s = s.substring(1);
        return dolt.baseUrl + s;
    }

    return dolt;
}]);