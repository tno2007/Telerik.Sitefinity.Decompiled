Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl.initializeBase(this, [element]);

    this._textBoxWidth = null;
    this._textBoxHeight = null;
    this._divWH = null;
    this._aspectRatioChoiceField = null;
    this._selectedRatio = null;

    this._widthKeyUpDelegate = null;
    this._heightKeyUpDelegate = null;
    this._cbClickDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl.callBaseMethod(this, 'initialize');

        this._attachHandlers(true);
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl.callBaseMethod(this, 'dispose');

        this._attachHandlers(false);
    },

    _attachHandlers: function (toAttach) {
        var txtW = this.get_textBoxWidth();
        var txtH = this.get_textBoxHeight();
        var ratioChoice = this.get_aspectRatioChoiceField();

        if (toAttach) {
            this._widthKeyUpDelegate = Function.createDelegate(this, this._widthKeyUp);
            $addHandler(txtW, "keyup", this._widthKeyUpDelegate);

            this._heightKeyUpDelegate = Function.createDelegate(this, this._heightKeyUp);
            $addHandler(txtH, "keyup", this._heightKeyUpDelegate);

            this._ratioChoiceChangeDelegate = Function.createDelegate(this, this._ratioChoiceChange);
            ratioChoice.add_valueChanged(this._ratioChoiceChangeDelegate);
        }
        else {
            if (this._widthKeyUpDelegate) {
                $removeHandler(txtW, "keyup", this._widthKeyUpDelegate);
                delete this._widthKeyUpDelegate;
            }
            if (this._heightKeyUpDelegate) {
                $removeHandler(txtH, "keyup", this._heightKeyUpDelegate);
                delete this._heightKeyUpDelegate;
            }

            if (this._ratioChoiceChangeDelegate) {
                ratioChoice.remove_valueChanged(this._ratioChoiceChangeDelegate);
                delete this._ratioChoiceChangeDelegate;
            }
        }
    },

    _widthKeyUp: function (args) {
        this._updateWidth();
    },
    _heightKeyUp: function (args) {
        if (this.get_aspectRatioChoiceField().get_value() !== "custom") {
            var txtH = args.target;
            var height = parseInt(txtH.value);
            var width = parseInt(this.get_textBoxWidth().value);

            var currentRatio = width / height;
            var offset = Math.abs(this.getRatio() - currentRatio);

            txtH.value = isNaN(height) ? "" : height;

            if (offset < 0.01) {
                //there is no need to update the ratio;
                return;
            }
           
            var result = Math.round(height * this.getRatio());
            this.get_textBoxWidth().value = isNaN(result) ? "" : result;
        }        
    },

    _updateWidth: function () {
        if (this.get_aspectRatioChoiceField().get_value() !== "custom") {
            var txtW = this.get_textBoxWidth();
            var width = parseInt(txtW.value);
            var height = parseInt(this.get_textBoxHeight().value);

            var currentRatio = width / height;
            var offset = Math.abs(this.getRatio() - currentRatio);

            txtW.value = isNaN(width) ? "" : width;

            if (offset < 0.01) {
                //there is no need to update the ratio;
                return;
            }
            
            var result = Math.round(width / this.getRatio());
            this.get_textBoxHeight().value = isNaN(result) ? "" : result;
        }
    },

    setWidthHeight: function (width, height) {
        width = parseInt(width);
        if (isNaN(width)) width = 0;
        height = parseInt(height);
        if (isNaN(height)) height = 0;
        if (width <= 0 && height <= 0) {
            this.get_aspectRatioChoiceField().set_value("auto");
            return;
        }

        if (width > 0) {
            this.get_textBoxWidth().value = width;
        }
        if (height > 0) {
            this.get_textBoxHeight().value = height;
        }

        var ratio = width / height;
        if (1.7 < ratio && ratio < 1.8) {
            // 16/9 == 1.7777...
            this.get_aspectRatioChoiceField().set_value("16x9");
        }
        else if (1.3 < ratio && ratio < 1.4) {
            // 4/3 == 1.3333...
            this.get_aspectRatioChoiceField().set_value("4x3");
        }
        else {
            this.get_aspectRatioChoiceField().set_value("custom");
        }
    },

    getHeight: function () {
        var value = this.get_aspectRatioChoiceField().get_value();

        if (value === "auto") {
            return null;
        }

        return this.get_textBoxHeight().value;
    },
    getWidth: function () {
        var value = this.get_aspectRatioChoiceField().get_value();

        if (value === "auto") {
            return null;
        }

        return this.get_textBoxWidth().value;
    },
    getRatio: function () {
        return this._selectedRatio;
    },

    _ratioChoiceChange: function () {
        var value = this.get_aspectRatioChoiceField().get_value();

        var showWHdiv = value === "4x3" || value === "16x9" || value === "custom";

        $(this.get_divWH()).toggle(showWHdiv);

        if (value === "4x3") {
            this._selectedRatio = 4 / 3;
            this._updateWidth();
        }
        else if (value === "16x9") {
            this._selectedRatio = 16 / 9;
            this._updateWidth();
        }

        if (dialogBase) {
            dialogBase.resizeToContent();
        }
    },

    //gets the textbox for setting the width
    get_textBoxWidth: function () { return this._textBoxWidth; },
    set_textBoxWidth: function (value) { this._textBoxWidth = value; },

    //gets the textbox for setting the width
    get_textBoxHeight: function () { return this._textBoxHeight; },
    set_textBoxHeight: function (value) { this._textBoxHeight = value; },

    //gets the DIV containing the Width and Height textboxes.
    get_divWH: function () { return this._divWH; },
    set_divWH: function (value) { this._divWH = value; },

    get_aspectRatioChoiceField: function () { return this._aspectRatioChoiceField; },
    set_aspectRatioChoiceField: function (value) { this._aspectRatioChoiceField = value; }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl', Sys.UI.Control);
