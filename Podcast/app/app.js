var app = angular.module('app'
    , ['ngRoute'
    , 'Podcast.EpisodioController']);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/novo', {
            templateUrl: 'app/view/podcast/novo.html',
            controller: 'EpisodioNovoCtrl'
        }).
        when('/ouvir', {
            templateUrl: 'app/view/podcast/index.html',
            controller: 'EpisodioIndexCtrl'
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
        template: '<audio ng-src="{{url}}" preload="none" controls></audio>',
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
        template: '<img ng-src="{{url}}" class="img-rounded" ></img>',
        link: function (scope) {
            scope.$watch('code', function (newVal, oldVal) {
                if (newVal !== undefined) {
                    scope.url = $sce.trustAsResourceUrl("media/image/" + newVal);
                }
            });
        }
    };
});

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

app.directive("fileread", [function () {
        return {
            restrict: 'EA',
            required: 'ngModel',
            template: '<div class="row" ng-if="showFile">' +
                            '<img src="{{fileB64}}" a alt="..." class="img-thumbnail col-sm-4" />' +
                        '</div>' +
                        '<div class="row">' +
                            '<div class="input-group col-sm-4">' +
                                '<input type="file" class="form-control input-file" accept=".jpg, .png" style="display: none;" />' +
                                '<input type="text" class="form-control" readonly="readonly" ng-model="fileread.name"/>' +
                                '<span class="group-span-filestyle input-group-btn" tabindex="0" ng-click="selectFile()">' +
                                    '<label for="filestyle-9" class="btn btn-default" >' +
                                        '<span class="icon-span-filestyle glyphicon glyphicon-folder-open"></span>' +
                                        '<span class="buttonText">Selecione</span>' +
                                    '</label>' +
                                '</span>' +
                            '</div>' +
                        '</div>',
            scope: {
                ngModel: "=",
                fileType: "=",
            },
            controller: function ($scope, $element) {
                $scope.fileread = {};
                $scope.selectFile = function () {
                    $element.find('[type="file"]').click();
                };
                $scope.showFile = false;
            },
            link: function (scope, element, attributes) {
                element.bind("change", function (changeEvent) {
                    scope.$apply(function () {
                        scope.fileread = changeEvent.target.files[0];

                        var FR = new FileReader();
                        FR.onload = function ( e ) {
                            scope.$apply(function () {
                                scope.showFile = true;
                                scope.fileB64 = e.target.result;
                            });
                        };
                        FR.readAsDataURL(scope.fileread);
                    });
                });

                scope.$watch('ngModel', function () {
                    if (angular.isDefined(scope.ngModel) && scope.ngModel != "") {
                        scope.showFile = true;
                        scope.fileB64 = 'data:image/jpeg;base64,' + scope.ngModel;
                    }
                });

            }
        }
    }])

app.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })

        .success(function () {
        })

        .error(function () {
        });
    }
}]);