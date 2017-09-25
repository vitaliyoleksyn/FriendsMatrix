'use strict';

angular.module('app').factory('userRelationApiService', function ($resource) {

    var URL = '/api/Relation/:id1/:id2';

    return {
        userRelationApiService: $resource(URL,
            null,
            {
                'post': {
                    method: 'POST',
                    timeout: 30000

                },
                'delete': {
                    method: 'DELETE',
                    timeout: 30000
                }

            }
        ),

        add: function (data) {
            return this.userRelationApiService.post(data).$promise;
        },
        remove: function (data) {
            return this.userRelationApiService.delete({ id1: data[0], id2: data[1] }).$promise;
        },
    }
});
