angular
    .module('osbb')
    .config(['$locationProvider', '$httpProvider', '$stateProvider', '$urlRouterProvider', cfg]);

function cfg($locationProvider, $httpProvider, $stateProvider, $urlRouterProvider) {
    //$urlRouterProvider.otherwise('/visualisation');
    $urlRouterProvider.otherwise('/generalinfogeneralinfo.view');
    $stateProvider
        .state('generalinfo', {
            url: '/generalinfo',
            templateUrl: function(stateParametr) {
                return '/dashboard/UserInfoMenu';
            }
        })
        .state('generalinfo.view', {
            url: 'generalinfo.view',
            parent: 'generalinfo',
            templateUrl: '/dashboard/generalinfo',
            controller: 'generalInfo as c'
})
        .state('generalinfo.edit', {
            url: '/generalinfo.edit',
            parent: 'generalinfo',
            templateUrl: '/dashboard/EditGeneraInfo/',
            controller: 'editGeneralInfo as c'
        })
        .state('monitoring', {
            url: '/home/monitoring',
            templateUrl: '/dashboard/Monitoring/',
            controller: 'monitoringController as c'
        })
        .state('visualisation', {
        url: '/visualisation',
        templateUrl: '/Content/visulaisation.html',
        controller: 'visualisationController as c'
        });
}