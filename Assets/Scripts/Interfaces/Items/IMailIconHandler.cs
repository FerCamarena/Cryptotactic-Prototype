namespace App.Game.Mail {
    /// <summary>
    /// 
    /// </summary>
    public interface IMailIconHandler {
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        void OnMailIconReleased(MailIcon icon);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        void OnMailWindowInteract(MailIcon icon);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        void OnMailWindowClose(MailIcon icon);
    }
}