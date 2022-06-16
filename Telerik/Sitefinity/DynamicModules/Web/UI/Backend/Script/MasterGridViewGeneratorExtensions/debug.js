var masterGridView;

// called by the MasterGridView when it is loaded
function OnModuleMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    masterGridView = sender;
    // the sender here is MasterGridView
    sidebar = sender.get_sidebar();

    var binder = sender.get_binder();
    binder.add_onDataBound(handleBinderDataBound);
}

function handleBinderDataBound(sender, args) {
    shortenDescriptions();
    resizeWorkArea();
    handleImageSizes(sender,args);
}

function handleImageSizes(sender, args) {
    var grid = args.get_target().Control;
    var img = $(grid).find(".sfImgTmb img");
    img.each(function (i) {
        var img = this;
        if (img) {
            var width = $(img).prop("width");
            var height = $(img).prop("height");
            resizeImage($(img), width, height, 60);
        }
    });
}

function resizeWorkArea() {
    if (typeof masterGridView != 'undefined') {
        var grid = $(masterGridView.get_element());
        var gridTable = $(grid).find(".rgMasterTable");
        var dlgSidebar = null;
        var dlgContent = null;
        var dlgMain = null;
        var wndWidth = null;
        if (gridTable.is(":visible")) {
            var gridWidth = gridTable.width();

            if (gridWidth + 80 > $(window).width() * 0.8) {
                //if the content is wider than the viewport
                dlgSidebar = $(".sfSidebar");
                dlgContent = $(".sfContent");
                dlgMain = $(".sfMain");

                dlgContent.width(gridWidth + 80);
                dlgSidebar.width(Math.floor(dlgContent.width() / 4));
                wndWidth = dlgSidebar.width() + dlgContent.width();
                dlgMain.width(wndWidth);
                $("body").width(wndWidth);
            }
        }
        if ($("body").width() < $(window).width()) {
            //if the window is resized from smaller to wider
            dlgSidebar = $(".sfSidebar");
            dlgContent = $(".sfContent");
            dlgMain = $(".sfMain");

            dlgContent.width("80%");
            dlgSidebar.width("20%");
            wndWidth = dlgSidebar.width() + dlgContent.width();
            dlgMain.width("auto");
            $("body").width("auto");
        }
    }
}

function handleBinderItemDataBound(sender, args) {
}

function resizeImage(img, w, h, size) {
    if (h > size || w > size) {
        if (h == w) {
            img.attr("height", size);
            img.attr("width", size);
        }
        else if (h > w) {
            img.attr("width", size);
            // IE fix
            img.removeAttr("height");
        }
        else {
            img.attr("height", size);
            // IE fix
            img.removeAttr("width");
        }
    }
}

jQuery.fn.stripTags = function () {
    return this.each(function () {
        var $this = $(this);
        var contentText = $this.context.innerText;
        var escapedContentText = contentText.replace(/<\/?[^>]+>/gi, '');
        $this.context.innerText = escapedContentText;
    });
};

jQuery.fn.decHTML = function () {
    return this.each(function () {
        var me = jQuery(this);
        me.html(me.text()).text();        
    });
};

function shortenDescriptions() {
    $(".dmDescriptionHTML").stripTags();
    $(".dmDescriptionHTML").decHTML();
    $(".dmDescription").shorten({
        "showChars" : 256,
        "showMoreLess" : false
    });
}

function joinArray(array, propertyName) {
    var result = [];
    for (var i = 0; i < array.length; i++) {
        result.push(array[i][propertyName]);
    }
    return result;
}

function getBool(value, yes, no) {
    if (value) {
        return yes;
    }

    return no;
}

$(window).bind('resize', resizeWorkArea);