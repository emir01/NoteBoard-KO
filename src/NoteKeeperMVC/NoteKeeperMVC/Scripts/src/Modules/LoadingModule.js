(function (nk, $) {
    // Simple module that handles the functionality for the loading ajax indicator
    nk.LoadingModule = (function () {
        // Start a masking on the dom element given with the given selector
        var starMask = function (domSelector) {
            $(domSelector).mask("Loading...");
        };

        // Stop the masking on a dom element with the given selector
        var stopMask = function(domSelector) {
            $(domSelector).unmask("Loading...");
        };

        return {
            StartMask:starMask,
            StopMask: stopMask
        };

    })();

})(NK = window.NK || {}, jQuery);