// The board repository is used to make server side ajax calls for retrieving and storing
// user note-boards.
(function (nk, $) {
    nk.BoardRepositorty = (function () {
        // Return all the boards for the logged in user.
        // The logged in user is checked on the server using the set cookie.
        // So no user parameters are sent via the ajax call.
        var getUserBoards = function (successCallback, failCallback) {
            var action = "BoardsForUser";
            var controller = "Data\\";

            var url = controller + action;

            var ajaxParams = {
                url: url,
                type: "POST",
                success: successCallback,
                error: failCallback,
                dataType: "json"
            };

            $.ajax(ajaxParams);
        };

        // Save the user board given with the data parameter.
        // The data parameter should be a pure JS object.
        var saveBoard = function (data, successCallback, failedCallback) {
            // The url parameters
            var controller = "/Data";
            var action = "/SaveBoard";

            // Compose the url
            var url = controller + action;

            // Create the json data
            // We stringify the board sent as the data parameter
            var stringifiedData = JSON.stringify({ boardViewModel: ko.toJS(data) });

            // Create the ajax parameters object.
            var ajaxParams = {
                url: url,
                type: "POST",
                data: stringifiedData,
                success: successCallback,
                error: failedCallback,
                dataType: "json",
                contentType: "application/json"
            };

            // actually start the ajax call to the server
            $.ajax(ajaxParams);
        };
        
        // Remove the user note-board
        var removeBoard = function (board, successCallback, failedCallback) {
            // The url parameters
            var controller = "/Data";
            var action = "/RemoveBoard";

            // Compose the url
            var url = controller + action;

            // stringify the user board id
            var data = JSON.stringify({ boardId: board.id });

            // setup the ajax parameter object
            var ajaxParams = {
                url: url,
                type: "POST",
                data: data,
                success: successCallback,
                error: failedCallback,
                dataType: "json",
                contentType: "application/json"
            };

            // Actually start the ajax call
            $.ajax(ajaxParams);
        };

        // The returned object with all the public functionality reveladed
        return {
            GetUserBoards: getUserBoards,
            SaveBoard: saveBoard,
            RemoveBoard: removeBoard
        };
    })();
})(NK = window.NK || {}, jQuery);