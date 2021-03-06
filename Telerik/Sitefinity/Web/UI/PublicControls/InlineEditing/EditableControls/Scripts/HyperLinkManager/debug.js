define(["DialogBase", "PageSelector", "text!HyperLinkManagerTemplate!strip"], function (DialogBase, PageSelector, template) {
    function HyperLinkManager(options) {
        var that = this;

        this.siteBaseUrl = options.siteBaseUrl;
        this.culture = options.culture;
        this.hyperLinkManagerTemplate = template;
        this.dialog = new DialogBase();
        this.pageSelector = null; 
        this.linkAttributes = null;
        this.pageSelectorSelectPageInfo = null;

        this.viewModel = kendo.observable({
            showView: function (e) {
                this.set("textToDisplay", "");
                that.showView(e.currentTarget.value);
            },
            moreOptionsClick: function (e) {
                that.moreOptionsClick(e);
            },
            webAddress: "",
            textToDisplay: "",
            linkTooltip: "",
            linkCss: "",
            openLinkInNewWindow: true,
            isMoreOptionsVisible: false,
            currentMode: "PageLink",
            emailAddress: "",
            textToDisplayExample: "",
            isMultisite: "",
            isMultilingual: "",
            currentSiteId: "",
            currentSiteRootNodeId: ""
        });

        return (this);
    }

    HyperLinkManager.prototype = {

        show: function (selectedNode, nodes) {
            this.resolveCurrentMode(selectedNode);
            this.loadLinkAttributes(selectedNode, nodes);
            this.initialize();
            this.showView(this.viewModel.currentMode);
            this.dialog.open();
        },

        initialize: function () {
            var contentPlaceHolder = $(this.dialog.getContentPlaceHolder());
            contentPlaceHolder.html(this.hyperLinkManagerTemplate);
            kendo.bind($(this.dialog.getContentPlaceHolder()), this.viewModel);

            $(this.dialog).on("doneSelected", jQuery.proxy(this.onDone, this));
            $(this.dialog).on("closeSelected", jQuery.proxy(this.onClose, this));
            
            this.initializePageSelector();
        },

        initializePageSelector: function () {
            var that = this;
            var contentPlaceHolder = $(this.dialog.getContentPlaceHolder());
            var pageSelectorOptions = {
                siteBaseUrl: this.siteBaseUrl,
                culture: this.culture,
                parentElement: contentPlaceHolder.find("#pageSelector"),
                isMultisite: this.viewModel.isMultisite.toLowerCase() == "true",
                currentSiteRootNodeId: this.viewModel.currentSiteRootNodeId,
                setTextToDisplay: function (title) {
                    that.setTextToDisplay(title);
                },
                selectPageInfo: this.pageSelectorSelectPageInfo
            };
            this.pageSelector = new PageSelector(pageSelectorOptions);
            this.pageSelector.init();
        },

        resolveCurrentMode: function (selectedNode) {
            var currentMode = "WebAddressLink";
            if (selectedNode && selectedNode.getAttribute("sfref") && selectedNode.getAttribute("sfref").startsWith("[")) {
                currentMode = "PageLink";
            }
            else if (selectedNode && selectedNode.href.indexOf("mailto:") > -1) {
                currentMode = "EmailLink";
            }
            this.viewModel.currentMode = currentMode;
        },

        showView: function (viewToShow) {
            var contentPlaceHolder = $(this.dialog.getContentPlaceHolder());
            var views = contentPlaceHolder.find("input[name=linkType]");
            $.each(views, function (index, view) {
                if (view.value == viewToShow) {
                    contentPlaceHolder.find("#" + view.value).show();
                } else {
                    contentPlaceHolder.find("#" + view.value).hide();
                }
            });
        },

        moreOptionsClick: function (e) {
            this.viewModel.set("isMoreOptionsVisible", !this.viewModel.isMoreOptionsVisible);
            var lnk = e.srcElement || e.target;
            if (this.viewModel.isMoreOptionsVisible && lnk) {
                $(lnk).addClass("sfExpandedLnk");
            } else {
                $(lnk).removeClass("sfExpandedLnk");
            }
        },

        loadLinkAttributes: function (selectedNode, nodes) {
            this.linkAttributes = {};

            var textToDisplayValue = "";
            for (var i = 0; i < nodes.length; i++) {
                textToDisplayValue += nodes[i].nodeValue;
            }
            this.viewModel.textToDisplay = textToDisplayValue;
            
            // set title, className and target properties
            this.viewModel.linkTooltip = selectedNode ? selectedNode.title : "";
            this.viewModel.linkCss = selectedNode ? selectedNode.className : "";
            this.viewModel.openLinkInNewWindow = selectedNode && selectedNode.target === "_blank" ? true : false;

            var idx;
            switch (this.viewModel.currentMode) {
                case "WebAddressLink":
                    this.viewModel.webAddress = selectedNode && selectedNode.getAttribute("href") ? selectedNode.getAttribute("href") : "http://";
                    this.pageSelectorSelectPageInfo = null;
                    break;
                case "PageLink":
                    var sfref = selectedNode ? selectedNode.getAttribute("sfref") : "";
                    idx = sfref.indexOf("]");
                    if (idx > -1) {
                        var rootNodeId = sfref.substr(1, idx - 1);
                        if (rootNodeId == "") {
                            rootNodeId = this.viewModel.currentSiteRootNodeId;
                        }
                        var nodeId = sfref.substring(idx + 1);
                        this.pageSelectorSelectPageInfo = { RootNodeId: rootNodeId, PageNodeId: nodeId };
                    }
                    break;
                case "EmailLink":
                    idx = selectedNode.href.indexOf(":");
                    if (idx > -1) {
                        this.viewModel.emailAddress = selectedNode.href.substring(idx + 1);
                    }
                    break;
                default:
                    break;
            }
        },

        setLinkAttributes: function () {
            switch (this.viewModel.currentMode) {
                case "WebAddressLink":
                    this.linkAttributes.href = this.viewModel.webAddress;
                    if (this.linkAttributes.sfref && this.linkAttributes.sfref.startsWith("[pages]")) {
                        delete this.linkAttributes.sfref;
                    }
                    break;
                case "PageLink":
                    var selectedItem = this.pageSelector.getSelectedDataItems()[0];
                    if (selectedItem) {
                        this.linkAttributes.href = selectedItem.FullUrl;
                    }
                    var selectedItemId = selectedItem.Id;
                    if (selectedItemId) {
                        var key;
                        var rootNodeId = selectedItem.RootNodeId;
                        if (rootNodeId && rootNodeId != Telerik.Sitefinity.getEmptyGuid()) {
                            key = rootNodeId;
                        }
                        else if (selectedItem) {
                            key = selectedItem.RootId;
                        }
                        else {
                            key = "";
                        }
                        var sfref = "[" + key + "]" + selectedItemId;
                        this.linkAttributes.sfref = sfref;
                    }
                    break;
                case "EmailLink":
                    this.linkAttributes.href = "mailto:" + this.viewModel.emailAddress;
                    delete this.linkAttributes.sfref;
                    break;
            }

            this.linkAttributes.innerHTML = this.viewModel.textToDisplay;

            // set title, className and target properties
            this.linkAttributes.title = this.viewModel.linkTooltip;
            this.linkAttributes.className = this.viewModel.linkCss;
            this.linkAttributes.target = this.viewModel.openLinkInNewWindow ? "_blank" : "";
        },

        getLinkAttributes: function () {
            return this.linkAttributes;
        },

        onDone: function (event, sender) {
            this.setLinkAttributes();
            $(this).trigger("linkSelected", this);
            sender.close();
        },

        onClose: function (event, sender) {
            sender.close();
        },

        setTextToDisplay: function (title) {
            this.viewModel.set("textToDisplay", title.PersistedValue);
        }
    };

    return (HyperLinkManager);
});