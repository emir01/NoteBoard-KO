// A Simple dialog Module that handles the details of creating and opening 
// jQuery dialogs and abstracts lower level jquery ui code.
(function (nk, $) {
    nk.DialogModule = (function () {
        // Opens a simple dialog based on the simple options object
        // and the simple buttons object.
        // The dilog is checked/opened on the passed in domElement selector
        var openDialog = function (domeElementSelector, optionsParameters, buttons) {
            // Extend the options with some default data.
            var options = $.extend(
                {
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    draggable: false,
                    title: "Default Title",
                    width: 255,
                    height: 200,
                }, optionsParameters
            );

            // Create the basic dialog on the selected dom element
            $(domeElementSelector).dialog({
                title: options.title,
                autoOpen: false,
                modal: options.modal,
                resizable: options.resizable,
                draggable: options.draggable,
                width: options.width,
                height: options.height,
                buttons:buttons,
                close: options.closeFunction
            });
   
            // open the above created dialog
            $(domeElementSelector).dialog("open");
        };

        var closeDialog = function (dialogDomSelector) {
            $(dialogDomSelector).dialog("close");
        };
        
        return {
            OpenDialog: openDialog,
            CloseDialog : closeDialog
        };
    })();
})(NK = window.NK || {}, jQuery);