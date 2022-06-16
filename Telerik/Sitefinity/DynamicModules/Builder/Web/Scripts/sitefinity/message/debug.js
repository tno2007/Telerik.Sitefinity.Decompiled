/**
* @name Sitefinity jQuery Message Plugin
*
* @description
* This is a jQuery pluging for displaying messages in the UI. It has 4 states
*   positive - showing message in green
*   negative - showing message in red
*   neutral - showing message in gray
*   initialized - message is hidden
*/

(function ($) {

    $.fn.message = function (state) {


        return this.each(function () {

            var $this = $(this);
            var states = ["initialize", "negative", "positive", "normal"];

            function removeLastState() {
                for(i=0; i<states.length; i++) {
                    var currentState = states[i];
                    if ($this.hasClass(currentState)) {
                        $this.removeClass(currentState);
                    }
                }
            };

            if (!state) {
                removeLastState();
                $this.addClass('initialize');
            }
            else {
                if (state == 'negative') {
                    removeLastState();
                    $this.addClass('negative');
                }

                if (state == 'positive') {
                    removeLastState();
                    $this.addClass('positive');
                }

                if (state == 'normal') {
                    removeLastState();
                    $this.addClass('normal');
                }
            }
        });
    };
})(jQuery);