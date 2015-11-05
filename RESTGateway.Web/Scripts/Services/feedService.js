(function () {
    'use strict';

    angular
        .module('feedService', ['ngResource'])
        .factory('Feed', Feed);

    Feed.$inject = ['$resource'];

    function Feed($resource) {
        //return $resource('/api/feed/:id');

        return $resource(
               "/api/feed/:id",
               { id: "@Id" },
               { "update": { method: "PUT" } }
          );
    }

})();