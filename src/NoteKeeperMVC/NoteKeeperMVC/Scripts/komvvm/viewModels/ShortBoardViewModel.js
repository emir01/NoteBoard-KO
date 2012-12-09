(function (nk, $) {
    // =====================================================
    // =========== **** ShortBoardViewModel **** =========
    // This short board view model is used to draw/dispaly
    // the list of user boards. Because we need a collection
    // to display all the boards we don not want to wase
    // memory and the extra overhead with the large view model,
    // so a smaller simpler view model is used. 
    // --- Used in the main application view model.
    // =====================================================
    
    // Namespacing code.
    var viewModels = nk.ViewModels || {};
    nk.ViewModels = viewModels;
    
    nk.ViewModels.ShortBoardViewModel = function (id, userId, boardName, isDefaultBoard) {
        // Id values for the board and user.
        this.id = id;
        this.userId = userId;

        // The name observables when displaying and editing the board.
        this.boardName = ko.observable(boardName || "");
        this.editingName = ko.observable("");
        
        // Indicates if this board is selected as a default board.
        this.isDefaultBoard = isDefaultBoard;
    };
})(window.NK = window.NK || {}, jQuery);