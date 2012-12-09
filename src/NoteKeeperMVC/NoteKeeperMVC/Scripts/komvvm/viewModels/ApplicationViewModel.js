(function (nk, $) {
    // =====================================================
    // ======= **** Main Application View  Model **** ======
    // =====================================================

    // Namespace checking code
    var viewModels = nk.ViewModels || {};
    nk.ViewModels = viewModels;

    nk.ViewModels.ApplicationViewmodel = (function () {
        // ==================================================
        // Main Observables
        // ==================================================

        // an observable for all the user boards consisting of a collection
        // of short board view models
        var userBoards = ko.observableArray([]);

        // an observable for the currently active short user board
        // This is the more complex board view model that handles note creation.
        var activeBoard = ko.observable(undefined);

        // the board beeing edited or created
        // Short view model, as there are very little editable properties
        // when creatin or editing a board
        var activeEditingBoard = ko.observable(undefined);
        
        // A reference to the board beeing saved to the server, so we can access it when needed
        // Actually does not need to be an observable.
        var persistingBoard = ko.observable(undefined);

        // The global board view model that contains all the notes 
        // and takes care of saving and editing notes on the actual note board.
        var boardViewModel = ko.observable();
        
        // ==================================================
        // Utility Properties
        // ==================================================
        // Short reference to the board repository
        var boardRepository;
        
        // the callback from init. Called after we initialize the board view model.
        // This function should contain a apply Bindings call.
        var initCallback;

        // ==================================================
        // Initialization
        // ==================================================
        // The starting initialization function that loads all the 
        // required data for the user and application at the initialization 
        // of the main application screen
        var init = function (callback) {
            // Set the init callback
            initCallback = callback;
            
            // set the board repository short reference
            boardRepository = nk.BoardRepositorty;

            // Set the  board view model  that controls board handling.
            boardViewModel(nk.ViewModels.BoardViewModel);

            // Get main application and user board/note data from server.
            // Basiccaly retrieve user boards and user information
            boardRepository.GetUserBoards(getUserBoardsSuccess, getUserBoardsFailiure);
            
            // call the init function on the board view model
            nk.ViewModels.BoardViewModel.init(activeBoard());
            
            if (boardViewModel().boardId() != undefined && boardViewModel().boardId() != "") {
                $("#main-container").removeClass("hidden");
            }
        };
        

        // ==================================================
        // Data and utility functions
        // ==================================================

        // The function to be called when hitting the cancel button on the board dialogs
        var boardDialogClear = function() {
            activeEditingBoard(undefined);
            $(this).dialog("close");
        };

        // The functoin to be called when clicking the Save button on the add boar dialog.
        var addBoardDialogSaveFunction = function () {
            // Set the board name to the value of the editing name
            // used to counter issues with editing and canceling boards
            addNewBoard();

            // Clear the board dialog
            // setting the correct this context
            boardDialogClear.apply(this);
        };

        var editBoardDialogSaveFunction = function() {
            // Get the editing board object from the observable
            var editedBoard = activeEditingBoard();

            // set the board name to the editingName
            editedBoard.boardName(editedBoard.editingName());

            // set the board we are persisting to the 
            // edited board
            persistingBoard(editedBoard);

            // save the board using the board repository.
            boardRepository.SaveBoard(ko.toJS(editedBoard), saveBoardSuccess, saveBoardFailiure);

            // Clear the board dialog
            // Make sure to set the 
            // correct this context on the method
            boardDialogClear.apply(this);
        };
        
        // The function that gets called when clicking on the delete board save functionality.
        var deleteBoardDialogSaveFunction = function () {
            // Get the board we need to delete
            var boardToRemove = activeEditingBoard();

            // remove the board
            removeBoardAndViewModel(boardToRemove);

            // Take care or disposing and setting 
            // atributes to the propper state
            activeEditingBoard(undefined);

            // close the dialog, passing the 
            // correct context to the method
            boardDialogClear.apply(this);
        };
        
        // ==================================================
        // Event Handlers
        // ==================================================
        // Event handler for clicking on the any of the board names in the list of boards
        // making the clicked board the active one and loading its notes via the main board view model.
        var setAsActiveBoard = function (data, event) {
            // Set the new active board and get notes and information about this new board
            // The board is represented by the short view model.
            activeBoard(data);
            
            // Changing the active board we must change and reload the main ViewModel for the main board
            // and display the new notes
            var mainBoardViewModel = boardViewModel();
            mainBoardViewModel.boardName(data.boardName);
            mainBoardViewModel.boardId(data.id);

            // reload the notes for the new board with the new id and name
            mainBoardViewModel.reloadNotes();
            
            // Just in case the main container still has the hidden class.
            $("#main-container").removeClass("hidden");

            // call the layout manager to lay out the notes for the new board
            nk.LayoutModule.CalculateLayout();
            
            return false;
        };

        var addBoard = function() {
            var newBoard = new nk.ViewModels.ShortBoardViewModel("", "", "Default Board Name", false);
            activeEditingBoard(newBoard);
            
            // Open a dialog using the dialog module and the specified options
            nk.DialogModule.OpenDialog("#board-edit-dialog",
                {
                    title: "New Board",
                    width: 255,
                    height:200,
                    closeFunction:function () {
                        activeEditingBoard(undefined);
                    }
                },
                // The button array
                [{
                    text:"Save",
                    click: addBoardDialogSaveFunction
                },
                {
                    text:"Cancel",
                    click: boardDialogClear
                }]
            );
        };
        
        // event handler for the edit board button
        var editBoard = function() {
            // Open a dialog for the current active board
            //set the editing name on the active board
            var activeViewModel = activeBoard();
            activeViewModel.editingName(activeViewModel.boardName());
            
            // Set the active editing board top the current active board
            // view model.
            activeEditingBoard(activeViewModel);
            
            // Open a dialog using the dialog module and the specified options
            nk.DialogModule.OpenDialog("#board-edit-dialog",
                {
                    title: "Edit Board",
                    width: 255,
                    height: 200,
                    closeFunction: function () {
                        activeEditingBoard(undefined);
                    }
                },
                // The button array
                [{
                    text: "Save",
                    click: editBoardDialogSaveFunction
                },
                {
                    text: "Cancel",
                    click: boardDialogClear
                }]
            );
        };

        // event handler for the delete board  button
        var deleteBoard = function () {
            // set the active editing board to the active board view model
            var activeBoardViewModel = activeBoard();
            activeEditingBoard(activeBoardViewModel);
            
            // Open a dialog using the dialog module and the specified options
            nk.DialogModule.OpenDialog("#board-delete-dialog",
                {
                    title: "Delete Board",
                    width: 255,
                    height: 200,
                    closeFunction: function () {
                        activeEditingBoard(undefined);
                    }
                },
                // The button array
                [{
                    text: "Yes",
                    click: deleteBoardDialogSaveFunction
                },
                {
                    text: "Cancel",
                    click: boardDialogClear
                }]
            );
        };
        
        // ==================================================
        // Utility Methods
        // ==================================================

        // Push a newly created board to the collection of user boards
        // and call data methods for server persistance
        var addNewBoard = function (shortBoardModel) {
            // Get the actual board object from the observable
            var board = activeEditingBoard();

            // Set the board name
            board.boardName(board.editingName());
            
            // push the board on the collection of user boards
            userBoards.push(board);

            persistingBoard(board);

            // persist the board to the server
            boardRepository.SaveBoard(board,saveBoardSuccess, saveBoardSuccess);
        };
        
        // remove the board from the board collection and 
        // persist the removal to the database
        function removeBoardAndViewModel(board) {
            // remove the board from the  user boards collection
            userBoards.remove(board);

            // set the active board to a default board
            var deafultShortBoardModel = new nk.ViewModels.ShortBoardViewModel("", "", "Select Board", false);
            activeBoard(deafultShortBoardModel);

            // reset the main board view model.
            boardViewModel().boardName("");
            boardViewModel().boardId("");

            // reload the notes, clearing the old notes
            boardViewModel().reloadNotes();
            
            // set the board to the persisted observable 
            // so we know there is changes to a certain board
            persistingBoard(board,removeBoardSucces,removeBoardFailiure);
            
            // Remove the board from the server side.
            boardRepository.RemoveBoard(board,removeBoardSucces,removeBoardFailiure);
        }

        // ==================================================
        // Server Communication Callbacks :
        // ==================================================

        var removeBoardSucces = function(data, status, object) {

        };

        var removeBoardFailiure = function(object, status, thrown) {

        };

        var getUserBoardsSuccess = function(data, status, object) {
            var boards = data.Data;
            var deafultShortBoardModel;

            if (data.Success) {
                // Add all the boards
                $.each(boards, function(idx, el) {
                    // Create the shortViewModel for each of the boards
                    var shortBoardViewModel = new nk.ViewModels.ShortBoardViewModel(el.Id, el.UserId, el.BoardName, el.IsDefaultBoard);

                    // Add the short board view model to the user boards
                    userBoards.push(shortBoardViewModel);

                    // If the current board is set as default
                    // set it as the active board
                    if (shortBoardViewModel.isDefaultBoard) {
                        activeBoard(shortBoardViewModel);
                    }
                });

                // if we still havent set an active board set a default one
                if (activeBoard() == undefined) {
                    deafultShortBoardModel = new nk.ViewModels.ShortBoardViewModel("", "", "Select Board", false);
                    activeBoard(deafultShortBoardModel);
                }
            }
            else {
                // Set the active board to a default board if there are not boards
                deafultShortBoardModel = new nk.ViewModels.ShortBoardViewModel("", "", "No Boards", false);
                activeBoard(deafultShortBoardModel);
            }

            // Call the init callback which in turn should apply the view model
            // this reduces sync issues with ajax
            initCallback();
        };
        
        var getUserBoardsFailiure = function(object, status, thrown) {
            alert("Got Boards Failed");
        };

        var saveBoardSuccess = function(data, status, object) {
            if (!data.Success) {
                alert("Failed to save the board");
            }
            else {
                // If we got a data object back
                // then set the id, so we make sure new boards have proper ids.
                if(data.Data != undefined){
                    var boardSaving = persistingBoard();

                    // Set the id of the board beeing persisted to the id that was recievedf
                    // from the server when the board was saved if the board was new.
                    boardSaving.id = data.Data.Id;
                    
                    // bassicaly if there is not board and i just created one set it as active.
                    if (boardViewModel().boardId() == undefined || boardViewModel().boardId() == "") {
                        setAsActiveBoard(boardSaving);
                    }
                }
            }
        };

        var saveBoardFailiure = function() {
            alert("Save board failiure");
        };
        
        return {
            init: init,
            userBoards:userBoards,
            activeBoard: activeBoard,
            activeEditingBoard:activeEditingBoard,
            boardViewModel: boardViewModel,
            setAsActiveBoard: setAsActiveBoard,
            editBoard: editBoard,
            deleteBoard:deleteBoard,
            addBoard:addBoard
        };
    })();
    
})(window.NK = window.NK || {}, jQuery);
