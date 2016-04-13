var app = angular.module('app'
    , ['ngRoute'
    , 'Podcast.PodcastController']);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/podcast/novo', {
            templateUrl: 'app/view/podcast/novo.html',
            controller: 'PodcastCtrl'
        }).
        when('/ouvir', {
            templateUrl: 'app/view/podcast/index.html',
            controller: 'PodcastCtrl'
        }).
        otherwise({
            redirectTo: 'ouvir'
        });
  }]);

app.directive('audios', function ($sce) {
    return {
        restrict: 'A',
        scope: { code: '=' },
        replace: true,
        template: '<audio ng-src="{{url}}" controls></audio>',
        link: function (scope) {
            scope.$watch('code', function (newVal, oldVal) {
                if (newVal !== undefined) {
                    scope.url = $sce.trustAsResourceUrl("media/audio/" + newVal);
                }
            });
        }
    };
});

app.directive('images', function ($sce) {
    return {
        restrict: 'A',
        scope: { code: '=' },
        replace: true,
        template: '<img height="100%" width="100%" ng-src="{{url}}" ></img>',
        link: function (scope) {
            scope.$watch('code', function (newVal, oldVal) {
                if (newVal !== undefined) {
                    scope.url = $sce.trustAsResourceUrl("media/image/" + newVal);
                }
            });
        }
    };
});