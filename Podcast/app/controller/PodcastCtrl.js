var PodcastController = angular.module('Podcast.EpisodioController', []);

PodcastController
.controller('EpisodioIndexCtrl', ['$scope', '$http', '$location', function ($scope, $http, $location) {
    $scope.model = {
        Episodios: {}
    };

    $scope.states = {
        ShowForm: false
    };

    $scope.selecionado = {};

    $http.get('episodio/IndexJSON').success(function (data) {
        $scope.model.Episodios = data;
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
    };
}])

.controller('EpisodioNovoCtrl', ['$scope', '$http', '$location', 'fileUpload', function ($scope, $http, $location, fileUpload) {

    $scope.novo = {};

    $scope.limparCampos = function () {
        $scope.novo = {};
    };

    $scope.cancelar = function () {
        $location.url('/ouvir');
    };

    $scope.salvarPodcast = function () {
        $scope.novo.nmArquivoAudio = $scope.audioFile.name;
        $scope.novo.nmArquivoImagem = $scope.imageFile.name;

        $http.post('episodio/Create', $scope.novo).success(function (data) {
            $scope.uploadAudioFile();
            $scope.uploadImageFile();

            $location.url('/ouvir');
        });

        $location.url('/ouvir');
    };

    $scope.uploadAudioFile = function () {
        var file = $scope.audioFile;

        //console.log('file is ');
        //console.dir(file);

        var uploadUrl = "episodio/Upload";
        fileUpload.uploadFileToUrl(file, uploadUrl);
    };

    $scope.uploadImageFile = function () {
        var file = $scope.imageFile;

        //console.log('file is ');
        //console.dir(file);

        var uploadUrl = "episodio/Upload";
        fileUpload.uploadFileToUrl(file, uploadUrl);
    };

}]);