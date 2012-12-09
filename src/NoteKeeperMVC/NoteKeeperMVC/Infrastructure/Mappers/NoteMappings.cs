using System;
using NK.Model.Entities;
using NK.Web.ViewModels.Data;

namespace NK.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Simple object mappers for the Note entitiy and the various Note View Models
    /// </summary>
    public static class NoteMappings
    {
        /// <summary>
        /// Map the domain model for the Note object to the NoteViewModel object.
        /// </summary>
        /// <param name="note">The domain <see cref="Note"/> object</param>
        /// <returns>Returns a <see cref="NoteViewModel"/> object</returns>
        public static NoteViewModel ToNoteViewModel(Note note)
        {
            var noteViewModel = new NoteViewModel()
                                    {
                                        Id = note.Id,
                                        Text = note.Text,
                                        BoardId = note.Board == null ? Guid.Empty : note.Board.Id,
                                        Title = note.Title,
                                        Top = note.Top,
                                        Left = note.Left
                                    };
            
            return noteViewModel;
        }

        /// <summary>
        /// Maps a given note view model to the Note model entitiy class.
        /// </summary>
        /// <param name="noteViewModel">The note view model we want to transform to a note entity class object.</param>
        /// <returns><see cref="Note"/> object transformed from the note view model.</returns>
        public static Note ToNoteEntity(NoteViewModel noteViewModel)
        {
            var note = new Note()
            {
                Id = noteViewModel.Id,
                Board = new Board() { Id = noteViewModel.BoardId },
                Text = noteViewModel.Text,
                Title = noteViewModel.Title,
                Top = noteViewModel.Top,
                Left = noteViewModel.Left,
            };

            return note;
        }
    }
}