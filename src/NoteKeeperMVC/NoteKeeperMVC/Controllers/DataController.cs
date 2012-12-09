using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NK.Services.Interface;
using NK.Web.Infrastructure.Mappers;
using NK.Web.ViewModels.Data;

namespace NK.Web.Controllers
{

    /// <summary>
    /// Data Controller containing action that return json data for notes and boards.
    /// </summary>
    [Authorize]
    public class DataController : Controller
    {
        #region Properties

        #region Services

        /// <summary>
        /// The basic note service used to retrieve/creade/edit notes and boards.
        /// </summary>
        private readonly INoteService _noteService;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Creates the DataController using Structure Map to inject the dependencies
        /// </summary>
        /// <param name="noteService">The structure map injected note service.</param>
        public DataController(INoteService noteService)
        {
            _noteService = noteService;
        }

        #endregion

        #region Actions
        
        #region Notes
        
        /// <summary>
        /// Retrieve all the notes for the given note-board specified with the given id.
        /// </summary>
        /// <param name="id">The id of the board for which we are retrieving notes.</param>
        /// <returns><see cref="JsonResultViewModel"/> object in JSON format.</returns>
        [HttpPost]
        public ActionResult NotesForBoard(string id)
        {
            // Create the Json result view model.
            var result = new JsonResultViewModel();

            // Parse the Guid that was passed in as string
            Guid boardGuidId;
            Guid.TryParse(id, out boardGuidId);

            // Get the notes for the board from the note service.
            var notes = _noteService.GetNotesForBoard(boardGuidId);

            // If we got notes back for the given board.
            if (notes != null)
            {
                // Set the properties on the json result view model.
                result.Success = true;
                result.Message = "Retrieved the notes fo the board";

                // Map the domain note objects to the NoteViewModel objects
                var viewModelNotes = notes.Select(NoteMappings.ToNoteViewModel);

                result.Data = viewModelNotes.ToList();
                
                // Return json data  containing the notes in json format using Json()
                return Json(result);
            }
            else
            {
                // Something went wrong, so set the apropriate properties on the 
                // json result view model and return it in json format
                result.Success = false;
                result.Message = "Failed to retrieve the notes for the board";
                result.Data = null;

                return Json(result);
            }
        }

        /// <summary>
        ///  Save the passed in note object for the current user and the specified note-board.
        /// </summary>
        /// <param name="noteViewModel">The note view model passed in from the client side.</param>
        /// <returns>Json Result view model containing the saved/updated note.</returns>\
        /// <remarks>This same action can be used to update old and save new notes, based on teh value of the Id of the note view model</remarks>
        [HttpPost]
        public ActionResult SaveNote(NoteViewModel noteViewModel)
        {
            // Create a json result view model object that will be returned to the client calling code.
            var result = new JsonResultViewModel();

            // Transform the view model to the Domain note object
            var note = NoteMappings.ToNoteEntity(noteViewModel);

            // Try and to save or update the note using the note service
            var noteUpdated = _noteService.SaveOrUpdateNote(note);

            // If we succeeded in saving or updating the note the updated note will have a concrete value
            if (noteUpdated != null)
            {
                // Set the apropriate json result object values.
                result.Success = true;
                result.Message = "Successfully saved the note";
                
                // Map the Note from the domain object to the NoteViewModel
                result.Data = NoteMappings.ToNoteViewModel(noteUpdated);

                // return the result view model as json
                return Json(result);
            }
            else
            {
                // If something went wrong with the service call set the 
                // apropriate values on the json result object.
                result.Success = false;
                result.Message = "Failed to save or update the note";
                result.Data = null;

                // Return the Json Result object in json format to the client code
                return Json(result);
            }
        }

        /// <summary>
        /// Delete a note with the given note id.
        /// </summary>
        /// <param name="noteId">The id of the note we want to remove passed in from the client side.</param>
        /// <returns><see cref="JsonResultViewModel"/> object containing the result of the operation.</returns>
        [HttpPost]
        public ActionResult DeleteNote(string noteId)
        {
            // The json result object that will be returned to the calling client code.
            var result = new JsonResultViewModel();

            // Parse the note id passed in form the client
            var noteGuid = Guid.Parse(noteId);

            // Try and delete the note using the note service
            var noteDeleted = _noteService.DeleteNote(noteGuid);

            if (noteDeleted)
            {
                // If the deletion was successfull
                // return a success json result view model
                result.Success = true;
                result.Message = "Successfully deleted the note";
                
                return Json(result);
            }
            else
            {
                // If the deletion failed return the apropriate message
                // to the client calling code in Json format.
                result.Success = false;
                result.Message = "Failed to deleted the note";
                
                return Json(result);
            }
        }

        #endregion

        #region Boards
        
        /// <summary>
        /// Retrieves all the note boards for the currently logged in user.
        /// </summary>
        /// <returns><see cref="JsonResultViewModel"/> object i JSON format.</returns>
        [HttpPost]
        public ActionResult BoardsForUser()
        {
            // Create the Json Result View model.
            var jsonResult = new JsonResultViewModel();

            // Get the user id of the currently logged in user
            var userId = GetUserId();

            // Get the boards for the user form the note service
            var boardsForUser = _noteService.GetBoardsForUser(userId);

            if (boardsForUser != null)
            {
                // Transform the user boards domain objects to apropriate view model objects
                var boardsViewModel = boardsForUser.Select(BoardMapper.BoardToBoardViewModel);

                // Set the json result properties
                jsonResult.Success = true;
                jsonResult.Message = "Sucessfuly retrieved user boards";
                jsonResult.Data = boardsViewModel;

                // Using Json() transform the result object to a JSON string and return it to the calling client code.
                return Json(jsonResult);
            }
            else
            {
                // If something went wrong with the call to the Note service set the apropriate fields
                // in the json resut object and again return it to the calling client code
                jsonResult.Success = false;
                jsonResult.Message = "Failed to retrieve user boards";
                jsonResult.Data = null;

                return Json(jsonResult);
            }
        }

        /// <summary>
        /// Save the pased in note-board for the currently logged in user.
        /// </summary>
        /// <param name="boardViewModel">The note-board view model object sent from the client side.</param>
        /// <returns><see cref="JsonResultViewModel"/> object in JSON format containing result information</returns>
        /// <remarks>Can be used to both save new boards or edit already exsisting boards based on the value of the board id</remarks>
        [HttpPost]
        public ActionResult SaveBoard(BoardViewModel boardViewModel)
        {
            var result = new JsonResultViewModel();

            // Get the id of the currently logged in user
            var userId = GetUserId();

            var board = BoardMapper.BoardViewModelToBoard(boardViewModel);

            // Try and save the board using the note service.
            var savedBoard = _noteService.SaveOrUpdateBoard(board, userId);

            // If the board was saved successfuly 
            if (savedBoard != null)
            {
                // Set the propper values for the Json result object
                result.Success = true;
                result.Message = " Board saved successfully";
                
                // Set the data to the saved/updated board, transformed to a BoardViewModel object
                result.Data = BoardMapper.BoardToBoardViewModel(savedBoard);

                // Return the json result object in JSON format to the client side code.
                // It contains the saved/updated board in the Data attribute
                return Json(result);
            }
            else
            {
                // Something went wrong while saving/upadting the board.
                // Set the propper values in the json result object
                result.Success = false;
                result.Message = "Failed to save the board";
                result.Data = null;

                return Json(result);
            }
        }

        /// <summary>
        /// Deletes the board with the given id.
        /// </summary>
        /// <param name="boardId">The id of the board we want to delete</param>
        /// <returns><see cref="JsonResultViewModel"/> in JSON format with the result information for the delete operation</returns>
        [HttpPost]
        public ActionResult RemoveBoard(Guid boardId)
        {
            var result = new JsonResultViewModel();

            // Try and delete the board with the given id.
            var success = _noteService.DeleteBoard(boardId);
            
            // If the deletion was successful
            if (success)
            {
                // Set the correct values to the Json Result View Model
                result.Success = true;
                result.Message = "Successfully deleted the board";
                result.Data = null;

                // Return the result object in JSON format to the client calling code.
                return Json(result);
            }
            else
            {
                // Something went wrong so we set the apropritate values to the result object
                // and return it in JSON format to the calling client code.
                result.Success = false;
                result.Message = "Failed to delete the board";
                result.Data = null;

                return Json(result);
            }
        }

        #endregion

        #region Users

        #endregion

        #endregion

        #region Private Methods

        //TODO : Refactor this, to probably use a wrapper, or get rid of the authentication wrapper.
        /// <summary>
        /// Gets the user id from the saved information in the authentication cookie
        /// </summary>
        /// <returns>The GUID id of the currenly logged in use.</returns>
        private Guid GetUserId()
        {
            // Get the Authentication cookie
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            // If there is no cookie there is no User GUID id
            if (cookie == null)
            {
                return Guid.Empty;
            }
            else
            {
                // Decrypt the cookie
                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                // And check the UserData on the cookie which should be set to a GUID.
                if (string.IsNullOrEmpty(decrypted.UserData))
                {
                    return Guid.Empty;
                }
                else
                {
                    // Try and parse the Guid from the user data and return it
                    var id =  Guid.Parse(decrypted.UserData);
                    return id;
                }
            }
        }

        #endregion
    }
}
