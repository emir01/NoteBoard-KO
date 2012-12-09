(function (nk, $) {
    // =====================================================
    // =========== **** Main Board View Model **** =========
    // The main Board view model that handles the creation
    // and management of the note view models, that are in turn
    // displayed on the board
    // =====================================================

    // Checking code that creates the proppert namespace under the global namespace
    var viewModels = nk.ViewModels || {};
    nk.ViewModels = viewModels;

    nk.ViewModels.BoardViewModel = (function () {
    
        // ==================================================
        // Main Observables
        // ==================================================

        // The board name observable
        var boardName = ko.observable("");

        // The id of the board that is linked to this main view model board.
        var boardId = ko.observable("");
        
        // List of all notes for the given board
        var notes = ko.observableArray([]);
        
        // The currently active note that is beeing edited or created.
        var activeNote = ko.observable();

        // The note that is currently beeing persisted.
        // Used to make changes to the current note beeing persisted
        // after callback from server
        var persistingNote = ko.observable();
        
        // ==================================================
        // Data and miscelanious utility functions
        // ==================================================
        // The dom selector for the note dialog.
        var dialogDomSelecor = "#NoteDialog";
        
        // The basic dialog options for the note editing/creating dialog.
        var basicNoteDialogOptions = {
            title: "Note Editor",
            width: 300,
            height:420,
            closeFunction: function() {
                activeNote(undefined);
            }
        };

        // The  close dialog handler for the note dialog.
        var closeDialogFunction = function() {
            // Clear the active note element so next time we open the dialog its clear
            // even if the user entered something.
            // There is no longer an active note.
            activeNote(undefined);

            $(this).dialog('close');
        };

        // The save button click handler for the new note dialog.
        var newNoteDialogSaveFunction = function() {
            // Write the changes from the editing observables to the main observables
            var activeNoteVm = activeNote();
            activeNoteVm.text(activeNoteVm.editingText());
            activeNoteVm.title(activeNoteVm.editingTitle());

            // When save is clicked we want to persist the active note to the server but 
            // also add it to the notes collection of the board
            createNewNote(activeNote());

            // There is no longer an active note.
            activeNote(undefined);

            // Close the dialog
            closeDialogFunction.apply(this);
        };

        // The save button click handler for the edit note dialog save function
        var editNoteDialogSaveFunction = function() {
            // Write the changes from the editing observables to the main observables
            var activeNoteVm = activeNote();
            activeNoteVm.text(activeNoteVm.editingText());
            activeNoteVm.title(activeNoteVm.editingTitle());

            // The active note is the note beeing edited
            // So we are going to persist the active note.
            var editedNote = activeNote();

            // save the changes
            persistNoteChanges(editedNote);

            // There is no longer an active note.
            activeNote(undefined);

            closeDialogFunction.apply(this);
        };
        
        // ==================================================
        // Initialization
        // ==================================================
        var init = function (shortBoardViewModel) {
            // On init we load notes if the initializing shortBoardViewModel
            // is NOT undefined
            if(shortBoardViewModel != undefined) {
                boardId(shortBoardViewModel.id);
                boardName(shortBoardViewModel.boardName);
                loadNotes();
            }
        };

        // Loads the notes from the server depending for the set board
        // only if the boardId and boardName have been correclty setup.
        var loadNotes = function () {
            // Clear the current note collection
            notes([]);
            if (boardId() != undefined && boardId() != ""
                && boardName() != undefined && boardName() != "") {
                // Get all notes from the server via the repository.
                nk.LoadingModule.StartMask("body");
                nk.NoteRepository.GetAllNotes(boardId(), getNotesSuccess, getNotesFailed);
            }
        };
        
        // ==================================================
        // Event Handlers
        // ==================================================
        // Create new note dialog
        var newNoteDialog = function () {
            // Set the current active observed note to a empty note with the current board id
            if (boardId() == undefined) {
                alert(" You must select a board before creating a neote!");
                return;
            }
            
            var clearNote = new nk.ViewModels.NoteViewModel("", boardId(), "", "", "60px", "60px");
            activeNote(clearNote);

            // Open a dialog using the dialog module and the specified options
            nk.DialogModule.OpenDialog(
                dialogDomSelecor,
                basicNoteDialogOptions,
                // The button array
                [{
                    text: "Save",
                    click: newNoteDialogSaveFunction
                },
                {
                    text: "Cancel",
                    click: closeDialogFunction
                }]
            );
        };
        
        // Edit note dialog click event handler from the ui button for each note
        var showEditDialog = function (data) {
            // the data attribute should be the view model that is the context over the event initiator
            // in this case its the note view model
            activeNote(data);

            // Open a dialog using the dialog module and the specified options
            nk.DialogModule.OpenDialog(
                dialogDomSelecor,
                basicNoteDialogOptions,
                // The button array
                [{
                    text: "Save",
                    click: editNoteDialogSaveFunction
                },
                {
                    text: "Cancel",
                    click: closeDialogFunction
                }]
            );
        };
        
        // Event handler for pressing the small close (x) button on each of the notes.
        var removeNote = function () {
            notes.remove(this);
            nk.NoteRepository.DeleteNote(this, deleteNoteSuccess, deleteNoteFailed);    
        };

        // the callback called when the we stop dragging a note.
        // Persists the cahnges of the new note positions on the server.
        var dragCallback = function(note) {
            persistNoteChanges(note);
        };
        
        // ==================================================
        // Utility Methods
        // ==================================================
        
        // Create a new note and store it in the boards note collection
        var createNewNote = function (note) {
            // Add it to the notes collection of the current board view model
            notes.push(note);
          
            // Set the note we are creating as the note beeing persisted to the server
            persistingNote(note);
            
            persistNoteChanges(note);
        };
        
        // Stores the changes made to the noteVm object to the sever side
        // Either edits or creates a new note.
        var persistNoteChanges = function (noteVm) {
            nk.NoteRepository.SaveNote(ko.toJS(noteVm), saveNoteSuccess, saveNoteFailed);
        };
        
        // Function that reloads the notes once the board id and name have changes.
        // Can be called from top level view models after changing an active board
        var reloadNotes = function() {
            loadNotes();
        };
        
        // ==================================================
        // Server Communication callbacks :
        // ==================================================
        
        // Callback for successfull retrieval of all board notes from the sever.
        var getNotesSuccess = function (data, status, object) {
            nk.LoadingModule.StopMask("body");
            // Data is the server side json result view model
            if (data.Success) {
                var notesFromServer = data.Data;
                $.each(notesFromServer, function (index, sNote) {
                    var vmNote = new nk.ViewModels.NoteViewModel(sNote.Id, sNote.boardId, sNote.Title, sNote.Text, sNote.Top, sNote.Left);
                    notes.push(vmNote);
                });
            }
            else {
                alert("Failed to get notes for the board");
            }
        };

        // Success for failing to retrieve all the notes from the server.
        var getNotesFailed = function (object, status, thrown) {
            nk.LoadingModule.StopMask("body");
            alert("Get Notes Failed");
        };

        var saveNoteSuccess = function (data, status, object) {
            // If we are persising a new note then we should be setting the persisting note value
            // then get the data returned from the server and update the id of the note.
            if(persistingNote() != undefined) {
                var note = data.Data;
                // set the id of the note that was persistet to the
                // id generated on the server side.
                persistingNote().id = note.Id;
                
                // clear the persising note
                persistingNote(undefined);
            }
        };

        var saveNoteFailed = function (object, status, thrown) {
        };

        var deleteNoteSuccess = function (data, status, object) {
            
        };

        var deleteNoteFailed = function (object, status, thrown) {

        };

        // Revelaing properties
        return {
            init: init,
            boardName: boardName,
            boardId:boardId,
            notes: notes,
            activeNote: activeNote,
            newNoteDialog: newNoteDialog,
            showEditDialog: showEditDialog,
            removeNote: removeNote,
            dragCallback: dragCallback,
            reloadNotes: reloadNotes
        };
    })(); // End of board view model.
})(window.NK = window.NK || {}, jQuery);