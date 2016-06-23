angular
    .module('osbb', [
        'ui.router',
        'ui.bootstrap',
        "chart.js"
    ]).config(function (ChartJsProvider) {
        // Configure all charts
        ChartJsProvider.setOptions({
            colours: ['#D49090', '#9096D4', '#23DE52', '#D49090', '#FDB45C', '#949FB1', '#4D5360'],
            responsive: true
        });
    });

angular.module('login',[]);