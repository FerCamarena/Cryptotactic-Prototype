namespace App.Game.Interaction {
    /// <summary>
    /// 
    /// </summary>
    public interface IActivatable {
    // ? PROPERTIES=================================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        bool IsEnabled { get; }
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        void OnActivate();
        /// <summary>
        /// 
        /// </summary>
        void OnDeactivate();
    }
}