﻿; (function () {

    var appStatusApp = angular.module('appStatusApp', []);

    appStatusApp.config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode(true);
    }]);

    appStatusApp.controller('appStatusController', ['$scope', '$http', '$timeout', '$window', '$location',
        function ($scope, $http, $timeout, $window, $location) {

            jQuery('.angular-container').show();

            $scope.initialized = false;
            $scope.upgradeMode = false;
            $scope.appInfoCollection = [];
            $scope.errorsCount = 0;
            $scope.warningsCount = 0;

            var appVirtualPath = document.getElementById('applicationVirtualPath') ? document.getElementById('applicationVirtualPath').value : '';
            var timer;

            var getSitefinityVersion = function () {
                $http({
                    method: 'GET',
                    url: appVirtualPath + '/appstatus?info=versioning'
                })
                .then(
                    function success(res) {
                        if (res.data.Current) {
                            $scope.currentVersion = res.data.Current;
                        }
                })
                ["catch"](function (error) {
                    // using [catch] is workaround for yuicompressor issue when minify javascript with catch syntax
                    console.log(error);
                });
            };

            var getAppStatus = function () {
                $http({
                    method: 'GET',
                    url: $scope.appInfoCollection.length > 0 ? appVirtualPath + '/appstatus?count=' + $scope.appInfoCollection.length : appVirtualPath + '/appstatus'
                })
                    .then(function success(res) {
                        if (!res.data.State) {
                            redirectToReturnUrl();
                            return;
                        }

                        $scope.initialized = true;
                        $scope.currentAppState = res.data.State;
                        $scope.adminMode = res.data.AdminMode;
                        if (res.data.State === 'Upgrading') {
                            $scope.upgradeMode = true;
                        }

                        if (res.data.Info && res.data.Info.length > 0) {
                            angular.forEach(res.data.Info, function (infoItem) {
                                var item = {
                                    message: infoItem.Message,
                                    stackTrace: infoItem.StackTrace,
                                    displayStackTrace: false,
                                    severity: getSeverity(infoItem.SeverityString),
                                    date: new Date(Date(infoItem.TimestampString)),
                                    longMessage: infoItem.Message.length > 500 || infoItem.StackTrace !== null
                                };

                                $scope.appInfoCollection.unshift(item);

                                if (item.severity === 'error') {
                                    $scope.currentAppState = 'Failed';
                                    $scope.errorsCount++;
                                }

                                if (item.severity === 'warning') {
                                    $scope.warningsCount++;
                                }
                            });

                            $scope.currentAction = res.data.Info[0].Message;
                        }

                        if ($scope.currentAppState !== 'Failed') {
                            getAppStatusInterval();
                        }
                    },
                    function error(res) {
                        if (res.status === 404 || res.status === 0) {
                            if (!$scope.upgradeMode || !$scope.adminMode) {
                                redirectToReturnUrl();
                            }
                            else {
                                $scope.initialized = true;
                                $scope.currentAppState = 'Done';
                            }
                        }
                    })
                    ["catch"](function (error) {
                        // using [catch] is workaround for yuicompressor issue when minify javascript with catch syntax
                        console.log(error);
                    });
            };

            var getAppStatusInterval = function () {
                timer = $timeout(function () {
                    getAppStatus();
                }, 3000);
            };

            getSitefinityVersion();
            getAppStatus();

            var redirectToReturnUrl = function () {
                // The redirect is handled and validated server side
                $window.location.reload();
            };

            var getSeverity = function (severityRaw) {
                if (severityRaw === 'Warning' || severityRaw === 'Error') {
                    return 'warning';
                }

                if (severityRaw === 'Critical') {
                    return 'error';
                }

                return severityRaw;
            };

            $scope.showReport = function () {
                $scope.detailsVisible = true;
            };

            $scope.hideReport = function () {
                $scope.detailsVisible = false;
            };

            $scope.goToSite = function () {
                redirectToReturnUrl();
            };

            $scope.toggleExpand = function (infoItem) {
                infoItem.displayStackTrace = !infoItem.displayStackTrace;
            };
        }
    ]);

    appStatusApp.directive('expandCollapse', [function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {

                element.bind('click', function () {
                    element.prev().toggleClass("active");
                    if (element.prev().hasClass("active")) {
                        element.html("Collapse");
                    } else {
                        element.html("Expand");
                    }
                });
            }
        };
    }]);

})();