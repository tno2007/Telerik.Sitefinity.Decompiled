(function ($) {

    $.fn.expandableLabel = function (options) {
        var settings = $.extend({
            items: [],
            maxCharSize: 22,
            joinString: ", ",
            moreText: "Show more...",
            lessText: "Show less"
        }, options);


        var setWrapperClass = function (item, wrapperClass) {
            return "<span" + " title='" + item + "' class='" + wrapperClass +"'>" + item + "</span>"
        }

        var renderLabel = function ($this) {
            var data = $this.data("expandableLabel");

            var itemsToShowList = data.items;
            var itemsToShow = itemsToShowList.join(data.joinString);

            if (itemsToShow.length > data.maxCharSize) {

                var moreText = data.moreText;
                var lessText = data.lessText;

                if (!data.IsExpanded) {
                    var labelText = itemsToShow.substring(0, data.maxCharSize);
                    if (data.wrapperClass) {
                        labelText = setWrapperClass(labelText, data.wrapperClass);
                    }

                    $this.html(labelText + "<br> ");

                    var moreAnchor = $("<a />").html(moreText).click(function () {
                        $this.html(itemsToShow + " ");
                        data.IsExpanded = true;
                        renderLabel($this);
                    });
                    $this.append(moreAnchor);
                }
                else {
                    if (data.wrapperClass) {
                        var modifiedList = [];

                        for (var i = 0; i < itemsToShowList.length; i++) {
                            modifiedList.push(setWrapperClass(itemsToShowList[i], data.wrapperClass));
                        }

                        itemsToShow = modifiedList.join(data.joinString);
                    }

                    $this.html(itemsToShow + "<br> ");

                    var lessAnchor = $("<a />").html(lessText).click(function () {
                        data.IsExpanded = false;
                        renderLabel($this);
                    });
                    $this.append(lessAnchor);
                }
            }
            else {
                $this.html(itemsToShow);
            }
        };

        var initializeData = function ($this) {
            var data = $.extend({
                isExpanded: false
            }, settings);
            $this.data("expandableLabel", data);
        };

        return this.each(function () {
            var $this = $(this);
            initializeData($this);
            renderLabel($this);
        });
    };

})(jQuery);