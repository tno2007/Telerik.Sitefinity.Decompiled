define(["text!ImageEditorTemplate!strip", "ImageSelector"], function (html, ImageSelector) {
    function ImageEditor(options) {
        var that = this;

        this.isInitialized = false;
        this.parentElement = options.parentElement;
        this.template = html;
        this.dateFormat = "dddd, MMM d, yyyy";
        
        this.serviceUrl = "Sitefinity/Services/Content/ImageService.svc/live";
        this.thumbnailServiceUrl = "Sitefinity/Services/ThumbnailService.svc";
        this.siteBaseUrl = options.siteBaseUrl;
        this.culture = options.culture;
        this.imageSelector = new ImageSelector({ siteBaseUrl: this.siteBaseUrl, culture: options.culture });
        this.imgTagSfRef = null;
        this._thumbnailExtensionPrefix = "tmb-";
        this._customSizeOptionValue = "--custom--size--";
        this._originalSizeOptionValue = "--original--size--";
        this.customRatio = null;

        this.viewModel = kendo.observable({
            titleText: '',
            dataItem: {},
            changeImage: function (e) {
                e.preventDefault();
                that.imageSelector.siteBaseUrl = that.siteBaseUrl;
                that.imageSelector.show();
            },
            
            dateString: function (e) {
                if (this.dataItem.DateCreated) {
                    var wcfDate = this.dataItem.DateCreated;
                    var jsDate = new Date(wcfDate.match(/\d+/)[0] * 1);

                    return jsDate.format(that.dateFormat);
                }
                return '';
            },
            onMarginOptionsClick: function (e) {
                var lnk = e.srcElement || e.target;
                if (this.customMarginOptionsVisible) {
                    this.set('customMarginOptionsVisible', false);
                    if (lnk)
                        $(lnk).removeClass("sfExpandedLnk");
                } else {
                    this.set('customMarginOptionsVisible', true);
                    if (lnk)
                        $(lnk).addClass("sfExpandedLnk");
                }
            },
            onTieRatioClick: function (e) {
                var cb = e.srcElement || e.target;
                if (cb.checked) {
                    //that.customRatio = parseInt(this.customWidth) / parseInt(this.customHeight);
                }
            },
            
            tieRatioChecked: true,
            customMarginOptionsVisible: false,
            marginTop: 0,
            marginRight: 0,
            marginBottom: 0,
            marginLeft: 0,
            title: '',
            alternativeText: '',
            selectedAlignment: "None",
            selectedThumbnailName: ''
        });

        return (this);
    }
    // This is how are images represented in a news item
    //<img title="Chrysanthemum" alt="Alt text"
    //src="http://localhost:8600/images/default-source/default-album/chrysanthemum.jpg"
    //sfref="[images|OpenAccessDataProvider]421cd286-91ce-6389-872c-ff000086d5aa" />
    ImageEditor.prototype = {

        init: function () {
            if (!this.isInitialized) {
                $(this.parentElement).html(this.template);
                kendo.bind(this.parentElement, this.viewModel);
                jQuery(this.imageSelector).on("doneSelected", jQuery.proxy(this.onImageSelected, this));
                this.isInitialized = false;
            }
        },

        refresh: function (imgTag) {
            this.imgTagSfRef = $(imgTag).attr('sfref');

            this.viewModel.set('alternativeText', $(imgTag).attr('alt'));
            this.viewModel.set('title', $(imgTag).attr('title'));
            this.viewModel.set('marginTop', this.parseCssNumber($(imgTag).css('marginTop')));
            this.viewModel.set('marginRight', this.parseCssNumber($(imgTag).css('marginRight')));
            this.viewModel.set('marginBottom', this.parseCssNumber($(imgTag).css('marginBottom')));
            this.viewModel.set('marginLeft', this.parseCssNumber($(imgTag).css('marginLeft')));

            if (imgTag.css('vertical-align') == "middle") {
                this.viewModel.set('selectedAlignment', 'Center');
            }
            else {
                switch (imgTag.css('float')) {
                    case "left":
                        this.viewModel.set('selectedAlignment', 'Left');
                        break;
                    case "right":
                        this.viewModel.set('selectedAlignment', 'Right');
                        break;
                    default:
                        this.viewModel.set('selectedAlignment', 'None');
                        break;
                }
            }

            var provider = this.getProviderFromSfrefAttr(this.imgTagSfRef);
            var id = this.getIdFromSfrefAttr(this.imgTagSfRef);

            this.rebind(id, provider);
        },
        rebind: function(id, provider) {
            var url = String.format('{0}/{1}/?provider={2}', this.siteBaseUrl + this.serviceUrl, id, provider);
            jQuery.ajax({
                type: 'GET',
                url: url,
                contentType: "application/json",
                processData: false,
                success: jQuery.proxy(this.onSuccess, this)
            });
        },
        onImageSelected: function (event, files) {
            var selectedFile = files;
            if (files && files.length) {
                selectedFile = files[0];
            }
            if (selectedFile) {
                this.rebind(selectedFile.LiveContentId, selectedFile.ProviderName);
            }
        },

        onSuccess: function (data) {
            this.viewModel.set('dataItem', data.Item);
            this.viewModel.set('alternativeText', data.Item.AlternativeText.Value);
            this.viewModel.set('title', data.Item.Title.Value);
            this.init();
        },

        formatImgTag: function (imgTag, imageItem) {

            var sfref = this.getSfrefAttribute(imageItem.LiveContentId || imageItem.Id, this.resolveProvider(imageItem), this.viewModel.selectedThumbnailName);
            imgTag.attr('sfref', sfref);
            imgTag.attr('alt', this.viewModel.alternativeText);
            imgTag.attr('title', this.viewModel.title);

            if (sfref.indexOf("tmb%3A") != -1) {
            	imgTag.attr("src", this.resolveThumbnailUrl(imageItem));
            }
            else {
            	imgTag.attr("src", imageItem.MediaUrl);
            }

            switch (this.viewModel.selectedAlignment) {
                case "Left":
                    imgTag.css("float", "left");
                    break;
                case "Right":
                    imgTag.css("float", "right");
                    break;
                case "Center":
                    imgTag.css("float", "");
                    imgTag.css("vertical-align", "middle");
                    break;
                default:
                    imgTag.css("float", "");
                    if (imgTag.css('vertical-align') == "middle") {
                        imgTag.css('vertical-align', '');
                    }
                    break;
            }
            imgTag.css('margin', "");
            if (this.viewModel.marginTop != null) {
                imgTag.css('marginTop', this.viewModel.marginTop + "px");
            }
            if (this.viewModel.marginRight != null) {
                imgTag.css('marginRight', this.viewModel.marginRight + "px");
            }
            if (this.viewModel.marginBottom != null) {
                imgTag.css('marginBottom', this.viewModel.marginBottom + "px");
            }
            if (this.viewModel.marginLeft != null) {
                imgTag.css('marginLeft', this.viewModel.marginLeft + "px");
            }
            return imgTag;
        },
        getSfrefAttribute: function (id, provider, thumbnailName) {
            sfref = '[images';
            if (provider) {
                sfref += '|' + provider;
            }
            if (thumbnailName && thumbnailName != "" && this.isThumbnailSelected(thumbnailName)) {
                sfref += '|tmb%3A' + thumbnailName;
            }
            sfref += ']' + id;
            return sfref;
        },
        getIdFromSfrefAttr: function (sfref) {
            if (sfref) {
                var idx = sfref.indexOf("]");
                if (idx > -1) {
                    return sfref.substring(idx + 1);
                }
            }
            return null;
        },
        resolveThumbnailUrl: function (imageItem) {
        	if (this.isThumbnailSelected(this.viewModel.selectedThumbnailName)) {
        		var tmbUrl = imageItem.ThumbnailUrl;
        		if (tmbUrl && tmbUrl != "") {
        			if (this.viewModel.selectedThumbnailName != "") {
        				var parts = tmbUrl.split(".");
        				if (parts.length > 1) {
        					var url = "";
        					for (var i = 0; i < parts.length; i++) {
        						if (url.length > 0)
        							url = url + "."
        						if (parts[i].indexOf(this._thumbnailExtensionPrefix) == 0)
        							url = url + this._thumbnailExtensionPrefix + this.viewModel.selectedThumbnailName;
        						else
        							url = url + parts[i];
        					}
        					return url;
        				}
        			}
        			return tmbUrl;
        		}
        	}
        	return imageItem.MediaUrl;
        },
        getProviderFromSfrefAttr: function (sfref) {
            if (sfref) {
                var startIdx = sfref.indexOf("[");
                var endIdx = sfref.indexOf("]");
                if (startIdx > -1 && endIdx > -1) {
                    var parts = sfref.substring(startIdx + 1, endIdx).split(new RegExp(['%7C', '\\\|'].join('|'), 'ig'));
                    if (parts.length > 1) {
                        for (var i = 1; i < parts.length; i++) {
                            if (parts[i].indexOf(":") === -1 || parts[i].toLowerCase().indexOf("%3a") === -1)
                                return parts[i];
                        }
                    }
                }
            }
            return null;
        },
        resolveProvider: function (dataItem) {
            return dataItem.Provider ||
                dataItem.ProviderName ||
                this.getProviderFromSfrefAttr(this.imgTagSfRef);
        },
        parseCssNumber: function(value) {
            var margin = parseInt(value);
            if (isNaN(margin)) {
                return null;
            }
            return margin;
        },
        getSelectedImage: function () {
            return this.viewModel.dataItem;
        },

        isThumbnailSelected: function (thumbnailName) {
        	return thumbnailName != this._customSizeOptionValue &&
                thumbnailName != this._originalSizeOptionValue;
        }
    };

    return (ImageEditor);
});
