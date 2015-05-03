app.factory('user', ['$window', function ($window) {
    var storage = $window.localStorage;
    var cachedUser;
    var userKey = "dollarTrackerUser";

    var userProfile = {
        setUser: function (user) {
            cachedUser = user;
            storage.setItem(userKey,JSON.stringify( cachedUser))
        },
        getUser: function () {
          // console.log('called user service:');
            if (!cachedUser) {
                cachedUser = JSON.parse(storage.getItem(userKey));
            }
          //  console.log(cachedUser);
            return cachedUser;
        },
        removeUser: function(){
            cachedUser = null;
            storage.removeItem(userKey);
        },
        
    }
    return userProfile;
}])