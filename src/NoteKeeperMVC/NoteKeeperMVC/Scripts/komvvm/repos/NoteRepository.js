// The Client side Note repository used to make calls to the Server.
(function (nk, $) {
    nk.NoteRepository = (function () {
        var dataController = "/Data";

        //Return all the notes for the board with the given board id
        var getAllNotes = function (boardId, successCallback, failedCallback) {
            // Sets the action on the data controller that returns the JSON formated notes for a board
            var allNotesAction = "/NotesForBoard";
            
            // Stringify the board id to json format to match the expecting parameter on the 
            // action server side
            var data = JSON.stringify({ id: boardId });

            // Setup the ajax params object
            var ajaxObject = {};
            ajaxObject.url = dataController + allNotesAction;
            ajaxObject.type = "POST";
            ajaxObject.dataType = "json";
            ajaxObject.contentType = "application/json";
            ajaxObject.data = data;
            ajaxObject.success = successCallback;
            ajaxObject.errror = failedCallback;

            // Actually start the ajax call
            $.ajax(ajaxObject);
        };

        // Saves the user-note to the  server using JSON and Ajax.
        var saveNote = function (note, successCallback, failedCallback) {
            // Set the action on the data controller that handles saving notes
            var saveNoteAction = "/SaveNote";

            // Stringifty the note data to JSON format
            var noteJson = JSON.stringify(note);

            // Setup the ajax param object
            var ajaxObject = {};
            ajaxObject.url = dataController + saveNoteAction;
            ajaxObject.type = "POST";
            ajaxObject.dataType = "json";
            ajaxObject.contentType = "application/json";
            ajaxObject.data = noteJson;
            ajaxObject.success = successCallback;
            ajaxObject.errror = failedCallback;

            // Actually make the ajax call
            $.ajax(ajaxObject);
        };

        // Dete the note from the server, using the note id, using JSON and AJAX
        var deleteNote = function(note, successCallback, failCallback){
            // Set the action on the data controller that handles note deletion
            var deleteNoteAction = "/DeleteNote";

            // Stringify the note id to be deleted to JSON format before sending it to sever with AJAX
            var noteId = JSON.stringify({ noteId: note.id });

            // Create the ajax object with all the propper values
            var ajaxObject = {};
            ajaxObject.url = dataController + deleteNoteAction;
            ajaxObject.type = "POST";
            ajaxObject.dataType = "json";
            ajaxObject.contentType = "application/json";
            ajaxObject.data = noteId;
            ajaxObject.success = successCallback;
            ajaxObject.errror = failCallback;

            // Make the actual ajax call to the server.
            $.ajax(ajaxObject);
        };

        // The publicly available methods for the Note Repo. Revealing module pattern.
        return {
            GetAllNotes: getAllNotes,
            SaveNote: saveNote,
            DeleteNote: deleteNote
        };
    })();
})(window.NK = window.NK || {}, jQuery);