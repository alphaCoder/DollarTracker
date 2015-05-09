app.factory('user', ['$window','dolt', function ($window, dolt) {
    var storage = $window.localStorage;
    var userKey = "dollarTrackerUser";
    
    var userProfile = {
        id: null,
        displayName: '',
        email: '',
        avatarUrl:'',
        setUser: function (user) {
            console.log('calling setUser', JSON.stringify(user));
            var self = this;
            if (user._id) self.id = user._id;
            else self.id = user.id;
            self.displayName = user.displayName;
            self.email = user.email;
            self.avatarUrl = dolt.getApiUrl('profilePic') + "/" + self.id+"?"+ (new Date()).getTime();
            storage.setItem(userKey, JSON.stringify(self))
        },
        removeUser: function(){
            var self = this;
            self.id = null;
            self.displayName = '';
            self.email = '';
            storage.removeItem(userKey);
        },

        autoLogin: function () {
            var user = storage.getItem(userKey);
            var self = this;
            if (user) {
                self.setUser(JSON.parse(user));
            }
        },

        forceReloadImg: function () {
            var self = this;
            self.avatarUrl = dolt.getApiUrl('profilePic') + "/" + self.id + "?" + (new Date()).getTime();
            self.setUser(self);
        }
    }
    return userProfile;
}])