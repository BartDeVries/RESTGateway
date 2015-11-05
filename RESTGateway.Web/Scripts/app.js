(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('RESTGatewayApp', [
        'ngRoute', 'feedService'
    ]).config(config);


    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Views/list.html',
                controller: 'FeedListController'
            })
            .when('/index.html', {
                templateUrl: '/Views/list.html',
                controller: 'FeedListController'
            })
            .when('/feed/add/', {
                templateUrl: '/Views/add.html',
                controller: 'FeedAddController'
            })
            .when('/feed/edit/:id', {
                templateUrl: '/Views/edit.html',
                controller: 'FeedEditController'
            })
            .when('/feed/delete/:id', {
                templateUrl: '/Views/delete.html',
                controller: 'FeedDeleteController'
            })
        ;

        $locationProvider.html5Mode(true);
    }

})();
