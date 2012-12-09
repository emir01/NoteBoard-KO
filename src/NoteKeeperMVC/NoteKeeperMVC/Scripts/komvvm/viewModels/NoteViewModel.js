(function (nk, $) {
    // =====================================================
    // =========== **** Main Note View Model **** =========
    // This is the simple note view model used to display notes
    // on a board.
    // =====================================================
    
    // Checking code that creates the proppert namespace under the global namespace
    var viewModels = nk.ViewModels || {};
    nk.ViewModels = viewModels;

    nk.ViewModels.NoteViewModel = function (id, boardId, title, text, top, left) {
        var self = this;

        // The identifier observables for each note.
        self.id = id;
        self.boardId = boardId;

        // Textual observables for each note.
        self.title = ko.observable(title);
        self.text = ko.observable(text);
        
        // The editable textual observables
        self.editingText = ko.observable(text);
        self.editingTitle = ko.observable(title);
        
        // Positional observables for each note
        self.top = ko.observable(top);
        self.left = ko.observable(left);
    };
})(window.NK = window.NK || {}, jQuery);