;(function () {
    "use strict";

    var shieldModule = angular.module('ShieldModule', ['kendo.directives']);
    shieldModule.controller("ShieldController", ['$scope', '$window', 'ShieldService', 'EmailInvitationService', 'AccessTokensService', 'accessTokensServiceUrl', 'isShieldEnabled', 'smtpSettingsAreValid', 'accessStatusesJson', 'isReadOnlyModeEnabled', 'enabledForAllSites', 
        function ($scope, $window, ShieldService, EmailInvitationService, AccessTokensService, accessTokensServiceUrl, isShieldEnabled, smtpSettingsAreValid, accessStatusesJson, isReadOnlyModeEnabled, enabledForAllSites) {
            var _statuses = JSON.parse(accessStatusesJson);

            $scope.shieldOptions = {
                smtpSettingsAreValid: smtpSettingsAreValid === 'True',
                isShieldEnabled: isShieldEnabled === 'True',
                isReadOnlyModeEnabled: isReadOnlyModeEnabled === 'True',
                enabledForAllSites: enabledForAllSites === 'True',
                autoBindKendo: isShieldEnabled === 'True' && smtpSettingsAreValid === 'True',
                invitedEmailsList: null,
                containsValidEmails: false,
                isRemoveAccessAboveOnIsChecked: false
            };

            $scope.accessTokensGridOptions = initializeKendoGrid(accessTokensServiceUrl, $scope.shieldOptions.autoBindKendo);
            initializeKendoDatePicker();

            // ----------------------------------------- Activate shield ---------------------------------------

            $scope.activateShield = function () {
                var data = {
                    activate: true
                };

                ShieldService.post(data).success(function (data) {
                    loadShieldOptions(data);
                    refreshKendoGrid();
                });
            };

            // ----------------------------------------- Deactivate shield ---------------------------------------

            $scope.openDeactivateShieldDialog = function () {
                $scope.deactivateConfirmDialog.center();
                $scope.deactivateConfirmDialog.wrapper[0].style.top = "50px";
                $scope.deactivateConfirmDialog.open();
            };

            $scope.confirmDeactivateDialog = function () {
                var data = {
                    activate: false
                };

                ShieldService.post(data).success(function (data) {
                    loadShieldOptions(data);
                });

                $scope.deactivateConfirmDialog.close();
            };

            $scope.cancelDeactivateDialog = function () {
                $scope.deactivateConfirmDialog.close();
            };

            // ----------------------------------------- Block access token ---------------------------------------

            $scope.openBlockAccessTokenDialog = function (token) {
                $scope.blockAccessConfirmDialog.token = token;

                $scope.blockAccessConfirmDialog.center();
                $scope.blockAccessConfirmDialog.wrapper[0].style.top = "50px";
                $scope.blockAccessConfirmDialog.open();
            };

            $scope.unblockAccessToken = function (token) {
                var data = {
                    token: token
                };

                AccessTokensService.unblockAccessToken(data).success(function (data) {
                    changeAccessTokenStatusText(data.Token, data.Status);
                }).error(function (data) {
                    changeAccessTokenStatusText(data.Token, data.Status);
                });
            }

            $scope.confirmBlockAccessTokenDialog = function () {
                var data = {
                    token: $scope.blockAccessConfirmDialog.token
                };

                AccessTokensService.blockAccessToken(data).success(function (data) {
                    changeAccessTokenStatusText(data.Token, data.Status);
                }).error(function (data) {
                    changeAccessTokenStatusText(data.Token, data.Status);
                });

                $scope.blockAccessConfirmDialog.close();
                delete $scope.blockAccessConfirmDialog.token;
            };

            $scope.cancelBlockAccessTokenDialog = function () {
                $scope.blockAccessConfirmDialog.close();
            };

            // ----------------------------------------- Remove access token ---------------------------------------

            $scope.openRemoveAccessTokenDialog = function (token) {
                $scope.removeAccessTokenConfirmDialog.token = token;

                $scope.removeAccessTokenConfirmDialog.center();
                $scope.removeAccessTokenConfirmDialog.wrapper[0].style.top = "50px";
                $scope.removeAccessTokenConfirmDialog.open();
            };

            $scope.confirmRemoveAccessTokenDialog = function () {
                var data = {
                    token: $scope.removeAccessTokenConfirmDialog.token
                };

                AccessTokensService.removeAccessToken(data).success(function (data) {
                    refreshKendoGrid();
                });

                $scope.removeAccessTokenConfirmDialog.close();
                delete $scope.removeAccessTokenConfirmDialog.token;
            };

            $scope.cancelRemoveAccessTokenDialog = function () {
                $scope.removeAccessTokenConfirmDialog.close();
            };

            // ----------------------------------------- Send email invitations ---------------------------------------

            $scope.openInviteUsersByEmailDialog = function () {
                clearInviteUsersByEmailVariables();

                $scope.sendEmailInvitationsDialog.center();
                $scope.sendEmailInvitationsDialog.wrapper[0].style.top = "50px";
                $scope.sendEmailInvitationsDialog.open();
            };

            $scope.confirmInviteUsersByEmailsDialog = function () {
                var validEmailsList = getCollectionOfValidEmails($scope.shieldOptions.invitedEmailsList);
                if (!validEmailsList || !validEmailsList.length) {
                    return;
                }

                var data = {
                    emails: validEmailsList
                };

                if ($scope.shieldOptions.isRemoveAccessAboveOnIsChecked === true) {
                    data.expiresOn = $("#scheduleDatePicker").data("kendoDatePicker").value();
                }

                EmailInvitationService.sendEmailInvitations(data).success(function () {
                    refreshKendoGrid();
                });

                clearInviteUsersByEmailVariables();
                $scope.sendEmailInvitationsDialog.close();
            };

            $scope.cancelInviteUsersByEmailsDialog = function () {
                $scope.sendEmailInvitationsDialog.close();
            };

            $scope.resendEmailInvitationAgain = function (token) {
                var data = {
                    token: token
                };

                // Change status to "Sending..."
                changeAccessTokenStatusText(token, "Sending");

                EmailInvitationService.resendEmailInvitation(data).success(function (data) {
                    setTimeout(function () { changeAccessTokenStatusText(token, data.Status); }, 500);

                }).error(function (data) {
                    setTimeout(function () { changeAccessTokenStatusText(token, data.Status); }, 500);
                });
            }

            $scope.shouldShowSendAgainMsg = shouldShowSendAgainMsg;

            $scope.shouldShowExpiresOn = shouldShowExpiresOn;

            $scope.shouldShowBlockOption = shouldShowBlockOption;

            $scope.setStatusStyleColor = function (status) {
                return getStatusStyleColor(status);
            }

            $scope.resolveTokenStatus = function (status) {
                var actualStatus = _statuses[status];
                return actualStatus;
            };

            $scope.containsValidEmails = function () {
                var validEmailsList = getCollectionOfValidEmails($scope.shieldOptions.invitedEmailsList);
                $scope.shieldOptions.containsValidEmails = validEmailsList && validEmailsList.length;
            };

            // ----------------------------------------- Functions ---------------------------------------

            function loadShieldOptions(data) {
                $scope.shieldOptions.isShieldEnabled = data.IsActiveForCurrentSite === true;
            }

            function getCollectionOfValidEmails(emailsList) {
                if (!emailsList || !emailsList.trim()) {
                    return null;
                }

                var validEmails = [];
                var trimmedEmailList = emailsList.trim();

                var separators = [ ' ', ';', ',', '\\n' ];
                var parsedEmails = trimmedEmailList.split(new RegExp('[' + separators.join('') + ']', 'g'));

                for (var i = 0; i < parsedEmails.length; i++) {
                    var value = parsedEmails[i];
                    if (!value || !value.trim()) {
                        continue;
                    }

                    var trimmedValue = value.trim();
                    if (isValidEmail(value) && validEmails.indexOf(trimmedValue) === -1) {
                        validEmails.push(value);
                    }
                }

                return validEmails;
            }

            function isValidEmail(email) {
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(email);
            }

            function initializeKendoGrid(accessTokensServiceUrl, isShieldEnabled) {
                var defaultColumnWidth = 450;

                var kendoGridOptions = {
                    dataSource: {
                        transport: {
                            read: {
                                url: accessTokensServiceUrl,
                                contentType: 'application/json; charset=utf-8',
                                type: "GET",
                                dataType: "json"
                            }
                        },

                        serverPaging: true,
                        serverFiltering: true,

                        schema: {
                            data: "Items",
                            total: "TotalCount"
                        },

                        pageSize: 20
                    },

                    dataBound: function () {
                        if (this.dataSource.total() != 0) {
                            toggleKendoGridContainer(true);
                            var menuItems = $("body .sfMoreActions .actionsMenu");
                            if (menuItems && menuItems.length > 0) {
                                for (var i = 0; i < menuItems.length; i++) {
                                    $(menuItems[i]).clickMenu();
                                }
                            }
                        }
                        else {
                            toggleKendoGridContainer(false);
                        }

                        if (this.dataSource.totalPages() === 1) {
                            this.pager.element.hide();
                        }
                        else {
                            this.pager.element.show();
                        }
                    },

                    sortable: true,
                    pageable: true,
                    scrollable: false,
                    autoBind: isShieldEnabled,
                    rowTemplate: jQuery.proxy(kendo.template(jQuery('#siteShieldGridRowTemplate').html())),

                    columns: [{
                        field: "Email", width: defaultColumnWidth
                    }, {
                        field: "Status", width: defaultColumnWidth
                    }, {
                        field: "NumberOfUsedDevices", width: 100
                    }, {
                        field: "Actions", width: 50, sortable: false
                    }]
                };

                return kendoGridOptions;
            }

            function refreshKendoGrid() {
                var dataSource = $("#accessTokensGrid").data('kendoGrid').dataSource;
                dataSource.read();

                var showGrid = dataSource.total() != 0;
                toggleKendoGridContainer(showGrid);
            }

            function toggleKendoGridContainer(showGrid) {
                $('#accessTokensHistoryArea').toggle(showGrid);
            }

            function initializeKendoDatePicker() {
                var dateTimeNow = new Date();
                var datePickerMaxYear = dateTimeNow.getFullYear() + 50;

                $("#scheduleDatePicker").kendoDatePicker({
                    value: dateTimeNow,
                    max: new Date(datePickerMaxYear, 11, 31, 0, 0, 0, 0),
                    animation: false,
                    change: scheduleDatePickerChangeHandler,
                    disableDates: function (date) {
                        if (date < dateTimeNow) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                });
            }

            function scheduleDatePickerChangeHandler(e) {
                var todayDate = new Date();
                var datePickerCurrentDate = e.sender.value();

                if (todayDate.getDate() == datePickerCurrentDate.getDate() &&
                    todayDate.getMonth() == datePickerCurrentDate.getMonth() &&
                    todayDate.getFullYear() == datePickerCurrentDate.getFullYear()) {
                }
            }

            function changeAccessTokenStatusText(token, newStatus) {
                var selector = "#accessTokenStatus_" + getSelectorFromIdentificator(token);

                var element = $(selector);
                element.html(_statuses[newStatus]);

                changeStatusStyleColor(element, newStatus);
                toggleAccessTokenSendAgainMsg(token, newStatus);
                toggleAccessTokenBlockOption(token, newStatus);
                toggleAccessTokenResendEmailOption(token, newStatus);
                toggleAccessTokenUnblockOption(token, newStatus);
                toggleAccessTokenExpiresOnOption(token, newStatus);
            }

            function toggleAccessTokenSendAgainMsg(token, status) {
                var isVisible = shouldShowSendAgainMsg(status);

                var selector = "#accessTokenSendAgainMsg_" + getSelectorFromIdentificator(token);
                var element = $(selector);
                element.toggle(isVisible);
            }

            function toggleAccessTokenBlockOption(token, status) {
                var isVisible = shouldShowBlockOption(status);

                var selector = "#accessTokenBlockOption_" + getSelectorFromIdentificator(token);
                var element = $(selector);
                element.toggle(isVisible);
            }

            function toggleAccessTokenUnblockOption(token, status) {
                var isVisible = !shouldShowBlockOption(status);

                var selector = "#accessTokenUnblockOption_" + getSelectorFromIdentificator(token);
                var element = $(selector);
                element.toggle(isVisible);
            }

            function toggleAccessTokenResendEmailOption(token, status) {
                var isVisible = shouldShowBlockOption(status);

                var selector = "#accessTokenResendEmailOption_" + getSelectorFromIdentificator(token);
                var element = $(selector);
                element.toggle(isVisible);
            }

            function toggleAccessTokenExpiresOnOption(token, status) {
                var isVisible = shouldShowExpiresOn(status);

                var selector = "#accessTokenExpirationDate_" + getSelectorFromIdentificator(token);
                var element = $(selector);
                element.toggle(isVisible);
            }

            function shouldShowSendAgainMsg(status) {
                var actualStatus = _statuses[status];

                var shouldShow = actualStatus != _statuses.Allowed && actualStatus != _statuses.Blocked && actualStatus != _statuses.Sending;
                return shouldShow;
            }

            function shouldShowExpiresOn(status) {
                var actualStatus = _statuses[status];

                var shouldShow = actualStatus == _statuses.Allowed;
                return shouldShow;
            }

            function shouldShowBlockOption(status) {
                var shouldShow = _statuses[status] != _statuses.Blocked;
                return shouldShow;
            }

            function getStatusStyleColor(status) {
                var actualStatus = _statuses[status];

                switch (actualStatus) {
                    case _statuses.Allowed: return { 'color': '#38AB63' };
                    case _statuses.Blocked: return { 'color': '#FF4848' };
                    case _statuses.Sending: return { 'color': '#999999' };
                    default: return { 'color': '#FF4848' };
                }
            }

            function changeStatusStyleColor(element, status) {
                var color = getStatusStyleColor(status);
                element.css(color);
            }

            function clearInviteUsersByEmailVariables() {
                $scope.shieldOptions.invitedEmailsList = null;
                $scope.shieldOptions.containsValidEmails = false;
                $scope.shieldOptions.isRemoveAccessAboveOnIsChecked = false;
            }
        }
    ]);

    // ----------------------------------------- Services ---------------------------------------

    shieldModule.factory('ShieldService', ['$http', 'shieldServiceUrl', function ($http, shieldServiceUrl) {
        var service = {
            post: function (data) {
                return $http.post(shieldServiceUrl, data);
            }
        };

        return service;
    }]);

    shieldModule.factory('AccessTokensService', ['$http', 'blockAccessTokenServiceUrl', 'removeAccessTokenServiceUrl', 'unblockAccessTokenServiceUrl', function ($http, blockAccessTokenServiceUrl, removeAccessTokenServiceUrl, unblockAccessTokenServiceUrl) {
        var service = {
            blockAccessToken: function (data) {
                return $http.post(blockAccessTokenServiceUrl, data);
            },
            
            removeAccessToken: function (data) {
                return $http.post(removeAccessTokenServiceUrl, data);
            },

            unblockAccessToken: function (data) {
                return $http.post(unblockAccessTokenServiceUrl, data);
            }
        };

        return service;
    }]);

    shieldModule.factory('EmailInvitationService', ['$http', 'emailInvitationServiceUrl', 'resendEmailInvitationServiceUrl', function ($http, emailInvitationServiceUrl, resendEmailInvitationServiceUrl) {
        var service = {
            sendEmailInvitations: function (data) {
                return $http.post(emailInvitationServiceUrl, data);
            },

            resendEmailInvitation: function (data) {
                return $http.post(resendEmailInvitationServiceUrl, data);
            }
        };

        return service;
    }]);
})();

function getSelectorFromIdentificator(identificator) {
    var selector = "sel" + identificator.replace(/[^\w\d]+/g, '');
    return selector;
}