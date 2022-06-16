var constants = {};
var instance = null;
var scriptControlInitialized;

Type.registerNamespace("Telerik.Sitefinity.TrackingConsent.UI");

Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView = function (element) {
    Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView.initializeBase(this, [element]);
}

Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView.prototype = {
    initialize: function () {
        constants.webServiceUrl = this.get_webServiceUrl();
        constants.contractType = this.get_contractType();
        constants.contractTypeShortName = this.get_contractTypeShortName();
        constants.contractTypeNamespace = this.get_contractTypeNamespace();
        constants.appPath = this.get_appPath();
        scriptControlInitialized = true;

        instance = this;
    },

    get_contractTypeNamespace: function () {
        return this._contractTypeNamespace;
    },
    set_contractTypeNamespace: function (value) {
        this._contractTypeNamespace = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_contractType: function () {
        return this._contractType;
    },
    set_contractType: function (value) {
        this._contractType = value;
    },
    get_contractTypeShortName: function () {
        return this._contractTypeShortName;
    },
    set_contractTypeShortName: function (value) {
        this._contractTypeShortName = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_domainConfirmationDialog: function () {
        return this._domainConfirmationDialog;
    },
    set_domainConfirmationDialog: function (value) {
        this._domainConfirmationDialog = value;
    },
    get_consentDetailsDialog: function () {
        return this._consentDetailsDialog;
    },
    set_consentDetailsDialog: function (value) {
        this._consentDetailsDialog = value;
    },
    get_appPath: function () {
        return this._appPath;
    },
    set_appPath: function (value) {
        this._appPath = value;
    },
}

Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView.registerClass('Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView', Sys.UI.Control);

(function () {
    "use strict";
    var trackingConsent = angular.module('trackingConsent', []);
    trackingConsent.controller('trackingConsentPage', ['$scope', '$timeout', 'basicService',
        function ($scope, $timeout, basicService) {

            $scope.acceptedCharsRegEx = new XRegExp('^[\\p{L},\\d, \\-,\\., :]+$');
            $scope.noSpacesRegEx = /^\S*$/;
            $scope.dotAtTheEndRegEx = new XRegExp('[.]$');

            $scope.loading = true;

            function InitController() {
                $scope.loading = false;
                basicService.getSettings()
                    .then(function (data) {
                        $scope.trackingConsents = addApplicationPathToTemplatePaths(data.Item.TrackingConsents);
                    }, function (err) {
                        $scope.error = "Error gettings consent tracking settings from server. See browser console for details.";
                        console.log('Error gettings consent tracking settings from server:');
                        console.log(err);
                    });
            }

            function checkScriptControlInitialized () {
                $timeout(function () {
                    if (scriptControlInitialized) {
                        InitController();
                    } else {
                        checkScriptControlInitialized();
                    }
                }, 100);
            }

            function initNewConsent() {
                return { 
                    Domain: "", 
                    ConsentIsRequired: false, 
                    ConsentDialog: "~" + constants.appPath + "App_Data/Sitefinity/TrackingConsent/consentDialog.html",
                    IsMaster: false
                }
            }

            function validateDomain(data, updatedConsent) {
                if (data.IsMaster) return;

                if (data.Domain == '') {
                    return instance.get_clientLabelManager().getLabel("Labels", "TrackingConsentViewEmptyError")
                }

                if (!$scope.acceptedCharsRegEx.test(data.Domain) || !$scope.noSpacesRegEx.test(data.Domain)) {
                    return instance.get_clientLabelManager().getLabel("Labels", "TrackingConsentViewSpecialCharactersError")
                }

                if ($scope.dotAtTheEndRegEx.test(data.Domain)) {
                    return instance.get_clientLabelManager().getLabel("Labels", "TrackingConsentViewDotAtTheEndError")
                }

                for (var idx = 0; idx < $scope.trackingConsents.length; idx++) {
                    var currentConsent = $scope.trackingConsents[idx];
                    if (currentConsent.Domain == data.Domain && currentConsent != updatedConsent) {
                        return instance.get_clientLabelManager().getLabel("Labels", "TrackingConsentViewUniqueError")
                    }
                }
            }

            function addApplicationPathToTemplatePaths(trackingConsents) {
                var result = [];
                if (trackingConsents) {
                    for (var idx = 0; idx < trackingConsents.length; idx++) {
                        var currentConsent = trackingConsents[idx];
                        result.push({
                            Domain: currentConsent.Domain,
                            ConsentIsRequired: currentConsent.ConsentIsRequired,
                            ConsentDialog: currentConsent.ConsentDialog.replace("~/", "~" + constants.appPath),
                            IsMaster: currentConsent.IsMaster
                        });
                    }
                }

                return result;
            }

            function removeApplicationPathFromTemplatePaths(trackingConsents) {
                var result = [];
                if (trackingConsents) {
                    for (var idx = 0; idx < trackingConsents.length; idx++) {
                        var currentConsent = trackingConsents[idx];
                        result.push({
                            Domain: currentConsent.Domain,
                            ConsentIsRequired: currentConsent.ConsentIsRequired,
                            ConsentDialog: currentConsent.ConsentDialog.replace("~" + constants.appPath, "~/"),
                            IsMaster: currentConsent.IsMaster
                        });
                    }
                }

                return result;
            }

            checkScriptControlInitialized();

            $scope.save = function () {
                $scope.loading = true;

                var data = {
                    Item: {
                        __type: constants.contractTypeShortName + ":#" + constants.contractTypeNamespace, // it is important that this property comes first!!!
                        TrackingConsents: removeApplicationPathFromTemplatePaths($scope.trackingConsents),
                    }
                }

                basicService.putSettings(data)
                    .then(function () {
                        InitController();
                    }, function (err) {
                        $scope.error = "Error saving consent tracking settings from server. See browser console for details.";
                        console.log('Error saving consent tracking settings from server:')
                        console.log(err);
                        $scope.loading = false;
                    });
            }

            $scope.boolToText = function (value) {
                return value ?
                    instance.get_clientLabelManager().getLabel("Labels", "Yes") :
                    instance.get_clientLabelManager().getLabel("Labels", "No");
            }

            $scope.getConsentLabel = function (consent) {
                return consent.IsMaster ?
                    instance.get_clientLabelManager().getLabel("Labels", "TrackingConsentDefaultDomainDisplay") :
                    consent.Domain;
            }

            $scope.askForDeletion = function (consent) {
                if (consent.IsMaster) return;

                var promptCallback = function (sender, args) {
                    if (args.get_commandName() === "delete") {
                        var index = $scope.trackingConsents.indexOf(consent);
                        $scope.trackingConsents.splice(index, 1);
                        $scope.save();
                    }
                }
                instance.get_domainConfirmationDialog().show_prompt(null, null, promptCallback);
            }

            $scope.showDetails = function (consent) {
                instance.get_consentDetailsDialog().open(false, consent, validateDomain, function () {
                    $scope.save();
                });
            }

            $scope.createNewDomain = function () {
                instance.get_consentDetailsDialog().open(true, initNewConsent(), validateDomain, function (newConsent) {
                    $scope.trackingConsents.push(newConsent);
                    $scope.save();
                });
            }
        }
    ]);

    trackingConsent.factory("basicService",
        ['$http',
            function ($http) {
                var config = {
                    headers: {
                        'If-Modified-Since': new Date().toGMTString(),
                        'Cache-Control': 'no-cache',
                        'Pragma': 'no-cache'
                    }
                };

                var service = {
                    getSettings: function () {
                        return $http.get(constants.webServiceUrl + "/generic/?itemType=" + constants.contractType, config)
                                    .then(function (response) {
                                        return response.data;
                                    });
                    },
                    putSettings: function (data) {
                        // the url key parameter is not used in the service
                        return $http.put(constants.webServiceUrl + "/generic/00000000-0000-0000-0000-000000000000/?itemType=" + constants.contractType, data)
                                    .then(function (response) {
                                        return response.data;
                                    });
                    }
                };

                return service;
            }]);
})();