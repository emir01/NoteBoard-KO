namespace NK.Web.ViewModels.Data
{
    /// <summary>
    /// View Model used to create easily seriazible strongy typed JSON results to the client.
    /// </summary>
    public class JsonResultViewModel
    {
        #region Properties
        
        /// <summary>
        /// Boolean value indicating the success of the operation.s
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// The message asociated with the result of the operation 
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Any data we need to return to the client together with the success indicator and message
        /// </summary>
        public object Data { get; set; }
        
        #endregion

        #region Constructor

        /// <summary>
        ///  Create a json result view modeol object and set the Success indicator to false.
        /// </summary>
        public JsonResultViewModel()
        {
            Success = false;
        }   
        
        #endregion
    }
}