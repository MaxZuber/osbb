angular.module('osbb')
    .controller('generalInfo', generalInfo)
    .controller('editGeneralInfo', editGeneralInfo)
    .controller('navigationController', navigationController)
    .controller('monitoringController', monitoringController)
    .controller('visualisationController', visualisationController)
    .controller('modalInstanceController', modalInstanceController);


angular.module('login')
    .controller('registerController', registerController)
    .controller('loginController', loginController);


function loginController() {
    var vm = this;
    vm.email = '';
    vm.password = '';
}

function registerController() {
    console.log("asd");
    var vm = this;
    vm.email = "";
    vm.password = "";
    vm.confirmPassword = "";

};

generalInfo.$inject = ['$http'];
function generalInfo($http) {
    var vm = this;
    $http.get('api/userprofile/?id=null').then(function (data) {
        console.log(data);
        vm.user = data.data;
    });
    console.log("am in");
}

navigationController.$inject = ['$location'];
function navigationController($location) {
    var vm = this;
    vm.activeTab = 1;
    vm.changeActiveTab = function (t) {
        vm.activeTab = t;
    };
    vm.isActive = function (path) {
        return $location.$$path === path;
    }
}

editGeneralInfo.$inject = ['$http', '$state', '$location'];
function editGeneralInfo($http, $state, $location) {
    var vm = this;

    function submitForm() {
        console.log("form Submited");

        var model = {};
        model.FirstName = vm.user.FirstName;
        model.LastName = vm.user.LastName;
        model.FlatId = vm.flat.Id;
        console.log(model);

        $http({
                method: 'PUT',
                url: '/api/userprofile',
                data: model
            })
            .then(function (data) {
                $state.go('generalinfo.view', $state.params, { reload: true });
            });
    }

    $http.get('api/Building').then(function (data) {
        console.log(data);

        vm.user = data.data.Account;
        vm.entrances = data.data.Entrances;
        if (vm.user.Flat !== null) {
            vm.entrances.selectedEntrance = data.data.Entrances[vm.user.Flat.EntranceId - 1];
            vm.flat = data.data.Entrances[vm.user.Flat.EntranceId - 1].Flats[vm.user.FlatId -1];
        }
    });

    vm.submitForm = submitForm;
}

monitoringController.$inject = ['$http', '$scope', '$uibModal'];
function monitoringController($http, $scope, $uibModal) {
    var vm = this;
    vm.fsa = null;
    vm.fsaNumber = null;

    vm.showFSA = function(path, entranceNumber) {
        console.log(path);
        vm.fsa = path;
        vm.fsaNumber = entranceNumber;

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'modalInstanceController',
            scope:  $scope,
            size: 'lg',
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });
    };



    vm.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(2015, 5, 22),
        startingDay: 1
    };

    $scope.data = [];

    $scope.entrances = {
        "0": {
            selectedDate: new Date(2016, 03, 03)
        },
        "1": {
            selectedDate: new Date(2016, 03, 03)
        }
    }

    function updateChart(index, date) {
        $http({
            method: 'GET',
            url: '/api/values/' + '?date=' + date.toISOString() + "&entranceId=" + 1
        }).then(function (data) {
            console.log(data);
            $scope.data[index] = [];
            $scope.data[index].series = [];
            $scope.data[index].labels = [];


            data.data[0].Sensors.forEach(function (item1, i1, arr) {
                $scope.data[index].series.push(item1.Description + ', ' + item1.Dimension);
                var vals = item1.SensorValuesViewModels.map(function (e) { return e.Value });
                $scope.data[index].push(vals);
                $scope.data[index].labels = item1.SensorValuesViewModels.map(function (e) { return e.DateTime.slice(-8); });
            });
        });
    }

    $scope.$watch('entrances[0].selectedDate', function (newValue, oldValue) {
        console.log(newValue.toISOString());
        if (newValue != oldValue) {
            updateChart(0, newValue);
        }

    });

    $scope.$watch('entrances[1].selectedDate', function (newValue, oldValue) {
        console.log(newValue.toISOString());


        if (newValue != oldValue) {
            updateChart(1, newValue);
        }
    });


    $scope.series = [];
    $http({
        method : 'GET',
        url : '/api/values/' + '?date=' +  new Date(2016, 03, 03).toISOString() + "&entranceId=" + null
    }).then(function(data) {
        console.log(data);
        data.data.forEach(function(item, i, arr) {
            item.selectedDate = new Date(2016, 03, 03);
            item.opened = false;
            $scope.data[i] = [];
            $scope.data[i].series = [];
            $scope.data[i].labels = [];

            item.Sensors.forEach(function (item1, i1, arr) {
                $scope.data[i].series.push(item1.Description + ', ' + item1.Dimension);
                var vals = item1.SensorValuesViewModels.map(function (e) { return e.Value });
                $scope.data[i].push(vals);
                $scope.data[i].labels = item1.SensorValuesViewModels.map(function (e) { return e.DateTime.slice(-8); });
            });
        });

        vm.entrances = data.data;
    });

}

modalInstanceController.$inject = ['$scope', '$uibModalInstance'];
function modalInstanceController($scope, $uibModalInstance) {
    $scope.ok = function () {
        $uibModalInstance.close();
    };
}

function visualisationController() {
    var vm = this;
    vm.ent = '1';
    vm.changedEnt = function () {
        randomizeValues();
    }
    var c = document.getElementById("canva");
    var c2 = document.getElementById("canva2");
    var c3 = document.getElementById("canva3");
    var ctx = c.getContext("2d");
    var ctx2 = c2.getContext("2d");
    var ctx3 = c3.getContext("2d");
    var img3 = new Image();
    img3.src = '/Content/visual_kotel.png';
    var img2 = new Image();
    img2.src = '/Content/v_detail.png';
    var img = new Image();
    img.src = '/Content/visal_2.png';
    var temperature = getRandomArbitary(17, 19).toFixed(1);
    img.onload = function () {
        draw();
    }
    var entranceTemp = getRandomArbitary(15, 20);
    var baseRoom = 0;
    var floor = null;
    var f_floor = null;

    c.addEventListener('mousemove', function (evt) {
        var mousePos = getMousePos(c, evt);
        console.log('Mouse position: ' + mousePos.x + ',' + mousePos.y);
        var tmp = null;
        for (var p in map) {
            if (map.hasOwnProperty(p)) {
                if (inside([mousePos.x, mousePos.y], map[p])) {
                    tmp = map[p];
                    f_floor = p;
                }
            }
        }
        floor = tmp;
        draw(mousePos.x, mousePos.y);
    }, false);
    c.addEventListener('click', function(evt) {
        var mousePos = getMousePos(c, evt);
        console.log('Mouse position Click: ' + mousePos.x + ',' + mousePos.y);
        for (var p in map) {
            if (map.hasOwnProperty(p)) {
                if (inside([mousePos.x, mousePos.y], map[p])) {
                    c.style.display = 'none';
                    if (p == 0) {
                        c3.style.display = 'block';
                        draw3();
                    } else {
                        c2.style.display = 'block';
                        baseRoom = (parseInt(p) - 1) * 4;
                        randomizeValues();
                        draw2();
                    }
                }
            }
        }
    });

    c2.addEventListener('mousemove', function (evt) {
        var mousePos = getMousePos(c2, evt);
        console.log('Mouse position: ' + mousePos.x + ',' + mousePos.y);
    }, false);
    c2.addEventListener('click', function (evt) {
        var mousePos = getMousePos(c2, evt);
        if (inside([mousePos.x, mousePos.y], [[16, 630], [50, 630], [50, 657], [16, 655], [16, 630]])) {
            c2.style.display = 'none';
            c.style.display = 'block';
            randomizeValues();
        }
    });

    c3.addEventListener('click', function (evt) {
        var mousePos = getMousePos(c3, evt);
        console.log('Mouse position: ' + mousePos.x + ',' + mousePos.y);

        if (inside([mousePos.x, mousePos.y], [[30, 36], [30, 73], [84, 73], [87, 38], [30, 36]])) {
            c2.style.display = 'none';
            c.style.display = 'block';
            c3.style.display = 'none';
            randomizeValues();
        }
    }, false);

    function draw(x, y) {
        ctx.drawImage(img, 0, 0);
        ctx.fillStyle = "black";
        ctx.font = "14pt sans-serif";
        ctx.fillText('Температура повіртря:' + temperature + ' °С', 550, 630);

        if (floor != null) {
            ctx.fillStyle = "rgba(255, 255, 255, 0.1)";
            ctx.beginPath();
            ctx.moveTo(floor[0][0], floor[0][1]);

            for (var i = 1; i < floor.length; ++i) {
                ctx.lineTo(floor[i][0], floor[i][1]);
            }
            ctx.closePath();
            ctx.fill();
            if (f_floor != null) {
                ctx.fillStyle = "#D4DEF6";
                ctx.fillRect(x + 20, y, 120, 30);
                // draw font in red
                ctx.fillStyle = "black";
                ctx.font = "14pt sans-serif";
                ctx.fillText("Поверх #" + f_floor, x + 30, y + 20);
            }
        }
    }
    var detailMap = [
{
    order: 1,
    baseGas: 79.4,
    currentGas: 3.1,
    baseHot: 33.33,
    baseCold: 64.32,
    electricity: 16.55,
    orederXOffset: 300,
    orderYOffset: 25,
    currentGasXOffset: 300,
    currentGasYOffset: 45,
    baseGasXOffset: 300,
    baseGasYOffset: 65,
    baseHotXOffset: 300,
    baseHotYOffset: 85,
    baseColdXOffset: 300,
    baseColdYOffset: 105,
    electricityXOffset: 300,
    electricityYOffset: 125,
    temperatureXOffset: 305,
    temperatureYOffset: 205,
    startTemperature: 16

},
{
        order: 2,
        baseGas: 66.4,
        currentGas: 4.5,
        baseHot: 22.33,
        baseCold: 13.32,
        electricity: 79.55,
        orederXOffset: 690,
        orderYOffset: 25,
        currentGasXOffset: 690,
        currentGasYOffset: 45,
        baseGasXOffset: 690,
        baseGasYOffset: 65,
        baseHotXOffset: 690,
        baseHotYOffset: 85,
        baseColdXOffset: 690,
        baseColdYOffset: 105,
        electricityXOffset: 690,
        electricityYOffset: 125,
        temperatureXOffset: 690,
        temperatureYOffset: 205,
        startTemperature: 28

    },
{
            order: 3,
            baseGas: 16.4,
            currentGas: 1.1,
            baseHot: 222.33,
            baseCold: 321.32,
            electricity: 154.55,
            orederXOffset: 300,
            orderYOffset: 400,
            currentGasXOffset: 300,
            currentGasYOffset: 425,
            baseGasXOffset: 300,
            baseGasYOffset: 445,
            baseHotXOffset: 300,
            baseHotYOffset: 465,
            baseColdXOffset: 300,
            baseColdYOffset: 485,
            electricityXOffset: 300,
            electricityYOffset: 505,
            temperatureXOffset: 305,
            temperatureYOffset: 570,
            startTemperature: 20

        },
{
        order: 4,
        baseGas: 789.4,
        currentGas: 3.0,
        baseHot: 654.33,
        baseCold: 456.32,
        electricity: 164.55,
        orederXOffset: 690,
        orderYOffset: 400,
        currentGasXOffset: 690,
        currentGasYOffset: 425,
        baseGasXOffset: 690,
        baseGasYOffset: 445,
        baseHotXOffset: 690,
        baseHotYOffset: 465,
        baseColdXOffset: 690,
        baseColdYOffset: 485,
        electricityXOffset: 690,
        electricityYOffset: 505,
        temperatureXOffset: 690,
        temperatureYOffset: 570,
        startTemperature: 21

    }];

    function randomizeValues() {
        for (var k = 0; k < detailMap.length; k++) {
            detailMap[k].baseGas = getRandomArbitary(0, 1000);
            detailMap[k].baseHot = getRandomArbitary(0, 1000);
            detailMap[k].baseCold = getRandomArbitary(0, 1000);
            detailMap[k].electricity = getRandomArbitary(0, 1000);
        }
    };

    randomizeValues();


    (function() {
        for (var i = 0; i < detailMap.length; ++i) {
            var random = getRandomArbitary(0, 1);
            detailMap[i].baseGas += random;
            random = getRandomArbitary(0, 1);
            detailMap[i].baseHot += random;
            random = getRandomArbitary(0, 1);
            detailMap[i].baseCold += random;
            random = getRandomArbitary(0, 1);
            detailMap[i].electricity += random;
            random = getRandomArbitary(-1, 1);
            detailMap[i].startTemperature += random;
            random = getRandomArbitary(-0.1, 0.1);
            entranceTemp += random;
        }
        setTimeout(arguments.callee, 3000);
    })();

    function draw2() {
        requestAnimationFrame(draw2);
        ctx2.drawImage(img2, 0, 0);
        for (var i = 0; i < detailMap.length; ++i) {


            ctx2.font = "14px sans-serif";
            ctx2.fillText('' + (detailMap[i].order + baseRoom + (36 * (vm.ent - 1))).toFixed(0), detailMap[i].orederXOffset, detailMap[i].orderYOffset);
            ctx2.fillText('' + (detailMap[i].baseGas).toFixed(2) + ' куб, м', detailMap[i].baseGasXOffset, detailMap[i].baseGasYOffset);
            ctx2.fillText('' + (detailMap[i].baseGas + detailMap[i].currentGas).toFixed(2) + ' куб, м', detailMap[i].currentGasXOffset, detailMap[i].currentGasYOffset);
            ctx2.fillText('' + (detailMap[i].baseHot).toFixed(2) + ' куб, м', detailMap[i].baseHotXOffset, detailMap[i].baseHotYOffset);
            ctx2.fillText('' + (detailMap[i].baseCold).toFixed(2) + ' куб, м', detailMap[i].baseColdXOffset, detailMap[i].baseColdYOffset);
            ctx2.fillText('' + (detailMap[i].electricity).toFixed(2) + ' кВт', detailMap[i].electricityXOffset, detailMap[i].electricityYOffset);

            if (detailMap[i].startTemperature > 23) {
                ctx2.fillStyle = "#D4A190";
            } else if (detailMap[i].startTemperature < 18) {
                ctx2.fillStyle = "#90C3D4";
            } else {
                ctx2.fillStyle = "white";
            }

            ctx2.fillRect(detailMap[i].temperatureXOffset - 5, detailMap[i].temperatureYOffset - 30, 70, 50);
            ctx2.font = "24px sans-serif";
            ctx2.fillStyle = "black";
            ctx2.fillText('' + (detailMap[i].startTemperature).toFixed(2), detailMap[i].temperatureXOffset, detailMap[i].temperatureYOffset);
            ctx2.font = "46px sans-serif";
            ctx2.fillText('' + entranceTemp.toFixed(1)+ ' °С', 490, 325);
        }
    }

    function draw3() {
        requestAnimationFrame(draw3);
        ctx3.drawImage(img3, 0, 0);
        ctx3.font = "14px sans-serif";
        for (var position in processMap) {
            if (processMap.hasOwnProperty(position)) {
                ctx3.fillStyle = "black";
                var pos = processMap[position];
                ctx3.fillText(pos.text, pos.textPoint[0], pos.textPoint[1]);

                if (typeof (pos.value) === typeof (1)) {
                    ctx3.fillStyle ="lightgreen";
                    ctx3.fillRect(pos.valuePoint[0] - 5, pos.valuePoint[1] - 20, 35, 25);
                    ctx3.fillStyle = "black";
                    var rnd = getRandomArbitary(-0.01, 0.01);
                    pos.value += rnd;
                    ctx3.fillText(pos.value.toFixed(1), pos.valuePoint[0], pos.valuePoint[1]);
                } else {
                    if (pos.value) {
                        ctx3.fillStyle = "darkgreen";
                        ctx3.fillText("Вкл", pos.valuePoint[0], pos.valuePoint[1]);
                    } else {
                        ctx3.fillStyle = "darkred";
                        ctx3.fillText("Викл", pos.valuePoint[0] -5, pos.valuePoint[1]);
                    }
                }

            }
        }
    }

    function getMousePos(c, evt) {
        var rect = c.getBoundingClientRect();
        return {
            x: evt.clientX - rect.left,
            y: evt.clientY - rect.top
        };
    }

    var map = {
        0: [
    [257, 577],
    [309, 592],
    [570, 480],
    [568, 519],
    [312, 651],
    [259, 623],
    [257, 577]
        ],
        1: [
            [255, 527],
            [307, 543],
            [568, 444],
            [568, 480],
            [308, 595],
            [257, 577],
            [255, 527]
        ],
        2: [
            [254, 466],
            [312, 479],
            [568, 401],
            [568, 444],
            [307, 543],
            [255, 527],
            [254, 466]
        ],
        3: [
            [252, 418],
            [310, 426],
            [568, 363],
            [568, 401],
            [312, 479],
            [254, 466],
            [252, 418]
        ],
        4: [
    [251, 360],
    [303, 368],
    [570, 318],
    [570, 363],
    [312, 426],
    [252, 418],
    [251, 360]
        ],
        5: [
            [248, 297],
            [303, 301],
            [570, 277],
            [570, 318],
            [303, 368],
            [250, 360],
            [248, 297]
        ],
        6: [
    [245, 236],
    [300, 240],
    [570, 233],
    [570, 277],
    [303, 301],
    [248, 297],
    [245, 236]
        ],
        7: [
            [245, 182],
            [300, 185],
            [570, 191],
            [570, 233],
            [300, 240],
            [245, 236],
            [245, 182]
        ],
        8: [
            [243, 124],
            [295, 124],
            [570, 147],
            [570, 191],
            [300, 185],
            [245, 182],
            [243, 124]
        ],
        9: [
        [242, 73],
        [301, 59],
        [572, 108],
        [570, 147],
        [295, 124],
        [243, 124],
        [242, 73]
        ]
    }

    var processMap = {
        "P61": {
            value: 4.4,
            text: '(P 6-1)',
            textPoint: [40, 698],
            valuePoint: [113, 703]
        },
        "T41": {
            value: 68.5,
            text: '(T 4-1)',
            textPoint: [69, 290],
            valuePoint: [137, 294]
        },
        "P11-1": {
            value: 6.3,
            text: '(P 11-1)',
            textPoint: [227, 258],
            valuePoint: [295, 266]
        },
        "Н-1": {
            value: true,
            text: 'ЦН-1',
            textPoint: [297, 342],
            valuePoint: [365, 345]
        },
        "Н-2": {
            value: true,
            text: 'ЦН-2',
            textPoint: [515, 463],
            valuePoint: [585, 468]
        },
        "T33": {
            value: 75.2,
            text: '(T 3-3)',
            textPoint: [576, 170],
            valuePoint: [639, 176]
        },
        "P51": {
            value: 69.3,
            text: '(P 5-1)',
            textPoint: [452, 136],
            valuePoint: [516, 141]
        },
        "Н-3": {
            value: true,
            text: 'МН-1',
            textPoint: [703, 241],
            valuePoint: [771, 247]
        },
        "Н-4": {
            value: true,
            text: 'МН-2',
            textPoint: [820, 241],
            valuePoint: [889, 247]
        },
        "T31": {
            value: 70.7,
            text: '(T 3-1)',
            textPoint: [899, 128],
            valuePoint: [960, 134]
        },
        "T21": {
            value: 69.9,
            text: '(T 2-1)',
            textPoint: [835, 549],
            valuePoint: [897, 554]
        },
        "T11": {
            value: 71.0,
            text: '(T 1-1)',
            textPoint: [986, 480],
            valuePoint: [1045, 486]
        },
        "Н-5": {
            value: true,
            text: 'НЗ',
            textPoint: [987, 574],
            valuePoint: [1055, 574]
        },
        "B-3": {
            value: true,
            text: 'BS',
            textPoint: [1196, 644],
            valuePoint: [1260, 650]
        },
        "NS": {
            value: false,
            text: 'Газ',
            textPoint: [1338, 518],
            valuePoint: [1401, 524]
        },


    };

    function inside(point, vs) {
        // ray-casting algorithm based on
        // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html

        var x = point[0], y = point[1];

        var inside = false;
        for (var i = 0, j = vs.length - 1; i < vs.length; j = i++) {
            var xi = vs[i][0], yi = vs[i][1];
            var xj = vs[j][0], yj = vs[j][1];

            var intersect = ((yi > y) != (yj > y))
                && (x < (xj - xi) * (y - yi) / (yj - yi) + xi);
            if (intersect) inside = !inside;
        }

        return inside;
    };

    function getRandomArbitary(min, max) {
        return Math.random() * (max - min) + min;
    }
}