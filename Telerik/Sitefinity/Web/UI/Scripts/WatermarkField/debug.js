(function ($) {

    $.fn.watermarkField = function (options) {
        var settings = $.extend({
            defaultText: "",
            isInline: false
        }, options);

        var renderLabel = function ($this) {
            var data = $this.data("watermarkField");
            var wrapperDiv = $('<div class="sfWatermarkField" />');          
            if (data.isInline)
                wrapperDiv.addClass("sfInlineBlock");
            $this.wrap(wrapperDiv);
                
            var lblWatermarkText = $('<label class="sfTxtLbl" />').html(data.defaultText);
            $this.addClass("sfTxt");
            $this.addClass("sfNavClassesInput");
            $this.addClass("sfMRight5");
            $this.before(lblWatermarkText);

            $this.focus(function () {
                //lblWatermarkText.css("text-indent", "-99999px");
                lblWatermarkText.addClass("sfFocusedTxtLbl");
            });

            $this.blur(function () {
                if ($(this).val() === '') {
                    //lblWatermarkText.css("text-indent", "0");
                    lblWatermarkText.removeClass("sfFocusedTxtLbl");
                }
                else {
                    //lblWatermarkText.css("text-indent", "-99999px");
                    lblWatermarkText.addClass("sfFocusedTxtLbl");
                }
            });

            lblWatermarkText.click(function () {
                $(this).parent().find("input").focus();
            });

            $this.blur();
        };

        return this.each(function () {
            var $this = $(this);
            $this.data("watermarkField", settings);
            renderLabel($this);
        });
    };

})(jQuery);