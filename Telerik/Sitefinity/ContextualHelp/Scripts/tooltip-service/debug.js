(function() {
    "use strict";

    var TOOLTIP_SELECTOR = "sf-tooltip-element";
    var CONTEXTUAL_HELP_KEY = "sf.config.contextualHelp";
    var LOAD_EVENT = "sf-load-tooltips";
    var RESET_EVENT = "reset-tooltip-service";
    var LOAD_TIMEOUT = 1000;
    var NEXT_TIP_TEXT = "Next tip";
    var MARK_TOOLTIPS_ROUTE = "{{MARK_TOOLTIPS_ROUTE}}";
    var GET_DATA_ROUTE = "{{GET_DATA_ROUTE}}";
    var TOOLTIP_ID_SEPARATOR = ",";

    var TOOLTIP_CONFIG = "{{TOOLTIP_CONFIG}}";

    var onboardingTipsEnabled = null;
    var tooltipData = null;
    var loadEventHandler = function () {
        setTimeout(function () {
            try {
                var currentUrl = window.location.href;
                createAll(currentUrl);
            } catch (ex) {
                console.error("An error occurred while trying to load contextual help and on-boarding tooltips." + ex.message + ex.stack);

                window.removeEventListener(LOAD_EVENT, loadEventHandler);
            }
        }, LOAD_TIMEOUT);
    };

    var resetServiceHandler = function () {
        tooltipData = null;
        onboardingTipsEnabled = null;
        cleanupTooltips();
    };
    
    initialize();

    function initialize() {
        // remove outdated tooltips that are no longer used to reduce stored data size
        cleanupTooltips();
        window.addEventListener(LOAD_EVENT, loadEventHandler);
        window.addEventListener(RESET_EVENT, resetServiceHandler);
    }

    function getTooltipData() {
        if (!tooltipData) {
            return getContextualHelpData().then(function(dataString) {
                var data = [];
                if (dataString !== "") {
                    data = dataString.ids.split(TOOLTIP_ID_SEPARATOR)
                        .map(function (s) {
                            return s.trim();
                        });
                }

                tooltipData = data;
                onboardingTipsEnabled = dataString.isEnabled;

                return { tooltipData: tooltipData, onboardingTipsEnabled: onboardingTipsEnabled };
            });
        }

        return Promise.resolve({ tooltipData: tooltipData, onboardingTipsEnabled: onboardingTipsEnabled });
    }

    function createAll(url) {
        getTooltipData().then(function (data) {
            if (data.onboardingTipsEnabled) {
                var tooltipsForCurrentUrl = getTooltipsForCurrentUrl(url);
                removeNonMatchingTooltips(tooltipsForCurrentUrl);

                var processedTooltips = getProcessedTooltips(tooltipsForCurrentUrl);
                processedTooltips.forEach(function (config) {
                    var configCopy = JSON.parse(JSON.stringify(config));
                    var isSequence = !!configCopy.collection;
                    if (isSequence) {
                        handleSequentialTooltip(configCopy, 0);
                        return;
                    }

                    configCopy.seen = getIsSeenByUser(configCopy, data.tooltipData);

                    if (!configCopy.seen) {
                        // insert tooltip only if it can be visible
                        // if the tooltip is seen and should not be shown anymore
                        // then there is no point appending it to the DOM
                        createAndInsertTooltipElement(configCopy);
                    }
                });
            }
        });
    }

    function getProcessedTooltips(tooltipsForCurrentUrl) {
        var sequences = tooltipsForCurrentUrl.filter(function (x) { return !!x.collection; });
        var faqTooltips = tooltipsForCurrentUrl.filter(function (x) { return x.showOnce === false; });
        var standaloneTooltips = tooltipsForCurrentUrl.filter(function (x) { return x.showOnce && !x.collection; });

        var hasSequence = sequences.length > 0;
        var hasMixedTooltips = hasSequence && standaloneTooltips.length > 0;
        if (hasMixedTooltips) {
            // handle case with 1 sequence and many standalone tooltips
            // append standalone tooltips to the sequence
            var sequence = sequences[0];
            sequence.collection.push(standaloneTooltips);

            var result = [sequence].concat(faqTooltips);
            return result;
        }

        return tooltipsForCurrentUrl;
    }

    function getTooltipsForCurrentUrl(url) {
        var tooltipConfigCopy = JSON.parse(JSON.stringify(TOOLTIP_CONFIG));
        var result = tooltipConfigCopy.filter(function (item) {
            if (Array.isArray(item.urlPattern)) {
                return item.urlPattern.some(function (p) { return url.match(p); });
            }

            return url.match(item.urlPattern);
        });

        return result;
    }

    function removeNonMatchingTooltips(tooltipsForCurrentUrl) {    
        var tooltipIdsForCurrentUrl = extractTooltipIds(tooltipsForCurrentUrl);

        Array.prototype.filter
            .call(
                document.querySelectorAll(TOOLTIP_SELECTOR),
                function (x) { return tooltipIdsForCurrentUrl.indexOf(x.id) === -1; })
            .forEach(function (x) { return x.parentNode.removeChild(x); });
    }

    function extractTooltipIds(tooltips) {
        return tooltips
            .map(function (t) {
                var isSequence = !!t.collection;
                if (isSequence) {
                    return t.collection.map(function (c) { return c.id; });
                }

                return [t.id];
            })
            .reduce(function (arr, item) {
                return arr.concat(item);
            }, []);
    }

    function handleSequentialTooltip(parentConfig, current) {
        getTooltipData().then(function(data) {
            var elementsCount = parentConfig.collection.length;
            if (elementsCount <= current) {
                return;
            }

            var childConfig = parentConfig.collection[current];

            // a sequence is always composed of show-once tooltips
            childConfig.showOnce = true;

            if (Array.isArray(childConfig)) {
                // handle mixed tooltips
                childConfig.forEach(function (c) {                
                    c.seen = getIsSeenByUser(c, data.tooltipData);
                    if (c.seen) {
                        return;
                    }

                    // config is a stanalone hint and should be treated as last element
                    c.isLastElement = true;
                    createAndInsertTooltipElement(c);
                });
            
                return;
            }

            var elementsInSequenceCount = elementsCount;
            if (Array.isArray(parentConfig.collection[elementsCount - 1])) {
                // last element in the sequence can be a collection of standalone tooltips
                // we need to exclude them to determine the got it link text
                elementsInSequenceCount = elementsCount - 1;
            }

            if (!isElementLastInSequence(current, elementsInSequenceCount, parentConfig.collection)) {
               childConfig.gotItLinkText = NEXT_TIP_TEXT;
            } else {
               childConfig.isLastElement = true;
            }

            childConfig.urlPattern = parentConfig.urlPattern;

            var handleNext = function () {
                handleSequentialTooltip(parentConfig, current + 1);
            };

            childConfig.seen = getIsSeenByUser(childConfig, data.tooltipData);
            if (childConfig.seen) {
                handleNext();
                return;
            }

            var isCreated = createAndInsertTooltipElement(childConfig, handleNext);
            if (!isCreated) {
                // proceed with next tooltip from sequence in case target element could not be found
                handleNext();
                return;
            }
        });
    }

    function isElementLastInSequence(currentElementPosition, elementsInSequenceCount, elementsCollection) {
        if (currentElementPosition === elementsInSequenceCount - 1) {
             return true;
        }

        // recursively check if child element can be placed
        var nextElementPosition = currentElementPosition + 1;
        var targetHtmlElement = getTargetHtmlElementOfTooltipElement(elementsCollection[nextElementPosition]);
        var canChildElementBePlaced = !!targetHtmlElement;
        if (!canChildElementBePlaced) {
           return isElementLastInSequence(nextElementPosition, elementsInSequenceCount, elementsCollection);
        } 

        return false;
    }

    function getIsSeenByUser(config, tooltipData) {
        return tooltipData.indexOf(config.id) > -1;
    }

    function createAndInsertTooltipElement(config, closeHandler) {
        var targetElement = getTargetHtmlElementOfTooltipElement(config);
        if (!targetElement) {
            return false;
        }

        var appendToParent = config.appendToParent;
        if (!config.attachTo) {
            appendToParent = true;
        }

        var allTooltips = Array.from(document.querySelectorAll(TOOLTIP_SELECTOR));
        var exists = allTooltips.findIndex(function (e) { return e.id === config.id; }) > -1;
        if (exists) {
            return true;
        }

        window.customElements.whenDefined(TOOLTIP_SELECTOR).then(function () {
            var elementBoundingRect = targetElement.getBoundingClientRect();

            // element should be visible
            var shouldInsertTooltip = !(
                elementBoundingRect.bottom === 0 &&
                elementBoundingRect.top === 0 &&
                elementBoundingRect.left === 0 &&
                elementBoundingRect.right === 0
            );

            if (shouldInsertTooltip) {
                var tooltipElement = createTooltipElement(config, elementBoundingRect, closeHandler);
                insertTooltipElement(tooltipElement, targetElement, appendToParent);
            }
        });

        return true;
    }

    function getTargetHtmlElementOfTooltipElement(tooltipConfig) {
        var targetElement = null;
        if (tooltipConfig.attachTo) {
            targetElement = document.querySelector(tooltipConfig.attachTo);
        } else {
            targetElement = document.querySelector("sf-app") || document.body;
        }

        return targetElement;
    }

    function createTooltipElement(config, elementBoundingRect, closeHandler) {
        var tooltipElement = document.createElement(TOOLTIP_SELECTOR);
        if (config.classes) {
            var classesArray = config.classes.split(" ");
            classesArray.forEach(function (cls) {
                return tooltipElement.classList.add(cls);
            });
        }

        tooltipElement.id = config.id;
        tooltipElement.content = config.content;
        if (config.gotItLinkText) {
            tooltipElement.gotItLinkText = config.gotItLinkText;
        }

        tooltipElement.showOnce = config.showOnce;
        tooltipElement.seen = config.seen;
        tooltipElement.look = config.look;
        tooltipElement.boxModelDimensions = config.boxModelDimensions;
        tooltipElement.zIndex = config.zIndex;
        tooltipElement.isLastElement = config.isLastElement;
        tooltipElement.fixedDimensions = config.fixedDimensions;
        tooltipElement.targetDimensions = elementBoundingRect;

        tooltipElement.addEventListener("close", function () {
            markTooltips([config.id]);

            if (closeHandler) {
                closeHandler();
            }
        });

        tooltipElement.addEventListener("skipAll", function () {
            var sequence = TOOLTIP_CONFIG
                .filter(function (t) {
                    return !!t.collection && t.collection.find(function(c) {
                        return c.id === config.id;
                    });
                });

            var allTooltipIds = extractTooltipIds(sequence);
            markTooltips(allTooltipIds);
        });

        return tooltipElement;
    };

    function insertTooltipElement(tooltipElement, targetElement, appendToParent) {
        if (appendToParent) {
            targetElement.appendChild(tooltipElement);
        } else if (targetElement.nextSibling) {
            targetElement.parentNode.insertBefore(tooltipElement, targetElement.nextSibling);
        } else {
            targetElement.parentElement.appendChild(tooltipElement);
        }
    }

    function markTooltips(tooltipIds) {
        getTooltipData().then(function(data) {
            var hasChange = false;
            tooltipIds.forEach(function (id) {
                // make sure we do not add duplicate ids
                if (data.tooltipData.indexOf(id) === -1) {
                    data.tooltipData.push(id);
                    hasChange = true;
                }
            });

            // if no change has been made, do nothing afterwards
            if (!hasChange) {
                return;
            }

            tooltipData = data.tooltipData;
            setContextualHelpPreference(data.tooltipData);
        });
    }

    function cleanupTooltips() {
        getTooltipData().then(function(data) {
        var allCurrentIds = extractTooltipIds(TOOLTIP_CONFIG);
            for (var i = 0; i < data.tooltipData.length; i++) {
                var storedId = data.tooltipData[i];
                if (allCurrentIds.indexOf(storedId) > -1) {
                    continue;
                }

                // remove element
                data.tooltipData = data.tooltipData.splice(i, 1);
            }
        });
    }

    function setContextualHelpPreference(data) {
        var request = new XMLHttpRequest();
        request.open("POST", MARK_TOOLTIPS_ROUTE);

        request.setRequestHeader("Content-Type", "application/json");

        var data = JSON.stringify({
            ids: data.join(TOOLTIP_ID_SEPARATOR)
        });

        request.send(data);
    }

    function getContextualHelpData() {
        return new Promise(function(resolve, reject) {
            var request = new XMLHttpRequest();
            request.onreadystatechange = function() {
                if (this.readyState == 4) {
                    if (this.status == 200) {
                        var response = JSON.parse(this.response);
                        resolve(response);
                    } else {
                        console.error("Error while trying to get tooltips: " + this.responseText);
                        window.removeEventListener(LOAD_EVENT, loadEventHandler);
                    }
                }
            };

            request.open("GET", GET_DATA_ROUTE);
            request.setRequestHeader("Content-Type", "application/json");
            request.send();
        });
    }
})();