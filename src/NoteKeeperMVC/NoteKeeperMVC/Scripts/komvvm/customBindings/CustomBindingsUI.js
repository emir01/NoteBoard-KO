
// ===================================================================================================
// ======================== Custom UI bindings for Knockout ==========================================
// ===================================================================================================
// ---------------------------------------------------------------------------------------------------

// ===================================================================================
// ======= Draggable binding ===========
// Can be applied to note/dom elements to make them draggable.
// When the dragging stops the note view model is updated with the new coordinates.
// Not quite usable for other elements.

ko.bindingHandlers.draggable = {
	init:function(element,valueAccessor,allBindingAccessor,viewModel){
	    var $element = $(element);
	    var allValues = allBindingAccessor();

	    var dragCallback = allValues.dragCallback;

		var makeDraggable = ko.utils.unwrapObservable(valueAccessor());

		if(makeDraggable){
			$element.draggable
			({
                handle:".header",
                containment:"parent",
                stop: function (event, ui) {
                    viewModel.top(ui.position.top + "px");
                    viewModel.left(ui.position.left + "px");

                    if (dragCallback != undefined) {
                        dragCallback(viewModel);
                    }
                }
            });
        }
	}
};

// ===================================================================================
// ======= Stackable Elements binding ===========
// Can be applied to dom elements to make them "stackable" using their z-index property
// Used to make the notes stackable on the board. When one note is clicked/selected it gets stacked 
// on the top of all the other notes. Again not quite usable really by other elements  besides the notes.

ko.bindingHandlers.stackableElements = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var stackTriggerSelector = valueAccessor();
        var allBindings = allBindingAccessor();

        var stackTriggerParentSelector = allBindings.stackTriggerParentSelector || ".note";

        $(element).on('mousedown', stackTriggerSelector, function () {
            var stackElement = $(this).parent(stackTriggerParentSelector);
            var maxZ = 0;

            $(stackTriggerParentSelector).each(function (index, el) {
                var stackElementZ = parseInt($(el).css("z-index"));
                if (stackElementZ > maxZ) {
                    maxZ = stackElementZ;
                }
            });
            stackElement.css("z-index", maxZ + 1);
        });
    }
};