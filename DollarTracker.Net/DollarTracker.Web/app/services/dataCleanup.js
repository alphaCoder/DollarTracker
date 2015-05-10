app.factory('dataCleanup', ['user', '$window', function (user, $window) {
    var dataCleanup = {};
    dataCleanup.cleanAll = function () {
        var tokenName = 'satellizer_token'; //todo: will replace this one
        delete $window.localStorage[tokenName];
        user.removeUser();
    }
    return dataCleanup;
}])