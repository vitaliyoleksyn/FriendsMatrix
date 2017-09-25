'use strict';

angular.module('app').factory('userApiService', function ($resource) {

    var URL = '/api/User/:id/:level';

    return {
        userApiService: $resource(URL,
            null,
            {
                'query': {
                    method: 'GET',
                    timeout: 30000,
                    isArray: true
                },
                'queryByLevels': {
                    method: 'GET',
                    timeout: 30000,
                    isArray: true
                },
                'get': {
                    method: 'GET',
                    timeout: 30000
                },
                'post': {
                    method: 'POST',
                    timeout: 30000
                }
            }
        ),

        addUser: function (data) {
            console.log(data)
            return this.userApiService.post(data).$promise;
        },

        getAllUsers: function () {
            return this.userApiService.query().$promise;
        },

        getUser: function (data) {
            console.log(data)
            return this.userApiService.get({ id: data.id }).$promise;
        },
        getNestingFriens: function (data) {
            console.log(data)
            return this.userApiService.queryByLevels({ id: data.id, level: data.level }).$promise;
        }
    }
});
