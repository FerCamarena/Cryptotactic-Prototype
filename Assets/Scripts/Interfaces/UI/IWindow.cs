using UnityEngine;

namespace App.Game.UI {
    /// <summary>
    /// 
    /// </summary>
    public interface IWindow {
    // ? PROPERTIES===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        bool IsOpen { get; }

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        void Initialize(App.Game.Managers.MailDisplayer displayer);

        /// <summary>
        /// 
        /// </summary>
        void Bind(App.Game.Mail.MailData data);

        /// <summary>
        /// 
        /// </summary>
        void Show();

        /// <summary>
        /// 
        /// </summary>
        void Show(Vector2 screenPosition);

        /// <summary>
        /// 
        /// </summary>
        void Hide();

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        void OnRequestClose();

        /// <summary>
        /// 
        /// </summary>
        void OnRequestFocus();
    }
}