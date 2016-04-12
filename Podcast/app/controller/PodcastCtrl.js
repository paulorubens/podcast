var PodcastController = angular.module('Podcast.PodcastController', []);

PodcastController.controller('PodcastCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Podcasts: {}
    };

    $scope.states = {
        ShowForm: false
    };

    $scope.novo = {};

    $scope.selecionado = {};

    $http.get('/podcast/podcast/IndexJSON').success(function (data) {
        $scope.model.Podcasts = data;
    });

    $scope.getTemplate = function (time) {
        if (time.PodcastBaseID == $scope.selecionado.PodcastBaseID)
            return 'edit';
        else
            return 'display';
    };

    $scope.returnDate = function (podcast, data) {
        var date = new Date(parseInt(data.substr(6)));
        podcast.dtGravacao = angular.copy(date);
        podcast.dtGravacaoVW = $scope.formataData(date);
    };

    $scope.formataData = function dataAtualFormatada(data) {
        var dia = data.getDate();
        if (dia.toString().length == 1)
            dia = "0" + dia;
        var mes = data.getMonth() + 1;
        if (mes.toString().length == 1)
            mes = "0" + mes;
        var ano = data.getFullYear();
        return dia + "/" + mes + "/" + ano;
    }
}]);