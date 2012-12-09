using System;

namespace NK.Web.ViewModels.Data
{
    /// <summary>
    /// The View model used to display Note entity data on the client side (UI).
    /// </summary>
    public class NoteViewModel
    {
        #region Properties

        public Guid Id { get; set; }

        public Guid BoardId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Top { get; set; }

        public string Left { get; set; }

        #endregion
    }
}