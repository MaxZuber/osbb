angular.module('osbb')
    .controller('registerController', registerController);

function registerController() {
    console.log("asd");
    var vm = this;
    vm.email = "";
    vm.password = "";
    vm.confirmPassword = "";

};