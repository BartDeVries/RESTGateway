(function () {
    'use strict';

    angular
        .module('RESTGatewayApp')
        .controller('FeedListController', FeedListController)
        .controller('FeedAddController', FeedAddController)
        .controller('FeedEditController', FeedEditController)
        .controller('FeedDeleteController', FeedDeleteController);

    FeedListController.$inject = ['$scope', 'Feed'];

    function FeedListController($scope, Feed) {
        $scope.feeds = Feed.query();
    }

    /* Feed Create Controller */
    FeedAddController.$inject = ['$scope', '$location', 'Feed'];

    function FeedAddController($scope, $location, Feed) {
        $scope.feed = new Feed();
        $scope.add = function () {
            $scope.feed.$save(function () {
                $location.path('/');
            });
        };
    }




    /* Feed Edit Controller */
    FeedEditController.$inject = ['$scope', '$routeParams', '$location', 'Feed'];

    function FeedEditController($scope, $routeParams, $location, Feed) {
        $scope.feed = Feed.get({ id: $routeParams.id });
        $scope.edit = function () {
            $scope.feed.$update(function () {
                $location.path('/');
            });
        };
    }

    /* Feed Delete Controller  */
    FeedDeleteController.$inject = ['$scope', '$routeParams', '$location', 'Feed'];

    function FeedDeleteController($scope, $routeParams, $location, Feed) {
        $scope.feed = Feed.get({ id: $routeParams.id });
        $scope.remove = function () {
            $scope.feed.$remove({ id: $scope.feed.Id }, function () {
                $location.path('/');
            });
        };
    }


})();
