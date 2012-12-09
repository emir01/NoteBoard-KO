// The module handles layout elements positioning and resizing.
(function(nk, $) {

	nk.LayoutModule = (function(){
		// The public initialization function
		var init = function(){
			calculateLayout();
			preloadImages();

			// Each time the window is resized we
			// recalcualte the layouts.
			window.onresize = function (e) {
                calculateLayout();
            };
        };

		// Preload important images.
        var preloadImages = function(){
            preload([
            	"/Content/loadmask/images/loading.gif",
            	"/Content/themes/ui-lightness/images/ui-bg_diagonals-thick_20_666666_40x40.png",
            	"/Content/themes/ui-lightness/images/ui-bg_diagonals-thick_18_b81900_40x40.png"
            	]);
        };

        // Preload important images.
        var preload = function (arrayOfImages) {
            $(arrayOfImages).each(function () {
                $('<img/>')[0].src = this;
                // Alternatively you could use:
                // (new Image()).src = this;
            });
        };

		// The main layout calculation that
		// combines multiple element layout
		// functions
		var calculateLayout = function(){
			// layout the main body element
			layoutBody();
			layoutBoard();
			layoutNotes();
		};

		// Private Methods : 
		// Makes the body the full height of the window
		var layoutBody = function(){
			// get the body element
			var $body = $("body");

			// get the html element
			var $html = $("html");

			// set the height to 100% of the html element but
			// taking into consideration
			// the bootstrap.css constraint of top-padding
			var bodyTopPadding = parseInt($body.css("padding-top"));

			// Get the html height
			var htmlHeight = $html.height();

			// calculate the new body height
			var newBodyHeight = htmlHeight - bodyTopPadding;

			// set the new height to the body
			$body.height(newBodyHeight);

		};

		// make the board the near full dimensions of the body
		// the notes might need to be recalculated on different board
		// sizes
		var layoutBoard = function(){
			// try and get the board object
			var $board = $("#active-board");
			
			// dont processd with layout if there is no active board element
			if($board.size() == 0){
				return;
			}

			// get the body element
			var $body = $("body");
			var bodyTopPadding = parseInt($body.css("padding-top"));
			var bodyHeight = $body.height();
			var bodyWidth = $body.width();

			// Test
			var compensationforHeight = 80;
			var boardHeightCalculation = bodyHeight - bodyTopPadding - compensationforHeight;
			$board.height(boardHeightCalculation);
		};

		// Change the position on the notes
		// to fit inside the new board sizes
		var layoutNotes = function(){
			// get all the notes
			var $notes = $(".note");

			// Get the board and its dimensions
			// for the calculations
			var $board = $("#active-board");
			var boardDimensions = {};
			boardDimensions.boardHeight = $board.height();
			boardDimensions.boardWidth = $board.width();

			// go through all the notes and check sizing 
			$notes.each(function (index, element){
				// get the individual note element and 
				// its dimension
				var $note = $(element);
				var noteDimensions = {};
				noteDimensions.noteTop = $note.offset().top;
				noteDimensions.noteLeft = $note.offset().left;
				noteDimensions.noteHeight = parseInt($note.css("height"));
				noteDimensions.noteWidth = parseInt($note.css("width"));
				noteDimensions.note = $note;

				// if the note is not within the board
				// set it inside the board
				if(!noteInsideBoard(noteDimensions, boardDimensions)){
					setNoteInBoard($note, noteDimensions, boardDimensions);
				}
				
			});
		};

		// Utility Functions : 
		var noteInsideBoard = function(noteDimension, boardDimension){
			// note dimensions : noteTop, noteLeft, noteHeight,noteWidth
			// board dimenison : boardHeight, boardWidth
			var boardOffset = $("#active-board").offset();

			// Check if the note is outside on the bottom or on the right.
			// It can never be outside
			if(noteDimension.noteTop > ((boardOffset.top + boardDimension.boardHeight) - noteDimension.noteHeight / 2)
				|| noteDimension.noteLeft > ((boardOffset.left + boardDimension.boardWidth) - noteDimension.noteWidth / 2)){
				return false;
			}

			// if the above checks fail  we return that the note is inside the board
			return true;
		};

		var setNoteInBoard = function ($note, noteDimensions,boardDimensions){
			$note.css("top","20px");
			$note.css("left","20px");
		};

		return {
			Initialize:init,
			CalculateLayout:calculateLayout
		};
	})();

	// hook into the document ready event
	// so we can start layout work
	// when the document is ready
	$(function(){
		// call the initialization on the layout module
		nk.LayoutModule.Initialize();


	});

})(NK = window.NK || {}, jQuery);