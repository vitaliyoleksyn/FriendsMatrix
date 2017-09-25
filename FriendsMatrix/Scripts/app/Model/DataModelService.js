'use strict';

angular.module('app').factory('dataModelService', ['userApiService', 'userRelationApiService', 'alertService',
    function (userApiService, userRelationApiService, alertService) {
        function DataModel() { }

        var _users = null;
        var _currentUser = null;

        var _loadData = function () {
            return userApiService.getAllUsers()
                        .then(function successCb(users) {
                            _users = users;
                            _currentUser = users[0];

                            if (_currentUser) {
                                return userApiService.getUser({ id: _currentUser.Id });
                            }
                        })
                        .then(function successCb(response) {
                            if (response) {
                                _currentUser.Relations = response.Relations;
                            }
                        }, function errorCb(response) {
                            alertService.showError(response);
                        });
        }

        DataModel.prototype = {
            loadData: function () {
                return _loadData();
            },
            getUsers: function () {
                return _users;
            },
            getCurrentUser: function () {
                return _currentUser
            },
            addUser: function (user) {
                console.log("user", user)
                return userApiService.addUser({ Name: user.name })
                    .then(function successCb(response) {
                        return userApiService.getAllUsers();
                    })
                    .then(function successCb(response) {
                        _users = response;
                        alertService.showSuccess("success");
                    }, function errorCb(response) {
                        alertService.showError(response);
                    });
            },
            addRelation: function (data) {
                console.log(data)
                return userRelationApiService.add({
                    UserId: data.userId,
                    FriendId: data.friendId
                })
                .then(function successCb(response) {
                    alertService.showSuccess("success");
                }, function errorCb(response) {
                    alertService.showError(response);
                });
            },
            removeRelation: function (data) {
                return userRelationApiService.remove([
                       data.userId,
                       data.friendId
                ]).then(function successCb(response) {
                    alertService.showSuccess("success");
                }, function errorCb(response) {
                    alertService.showError(response);
                });
            }

        };

        return DataModel;
    }]);
