using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace App.Game.UI {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Window : MonoBehaviour, IWindow, IPointerDownHandler {
    // ? DEBUG======================================================================================================================================
        [Header("Debug")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        private Managers.MailDisplayer displayer;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        private string currentId;

        // * ATTRIBUTES
        // [Header("Attributes")]
        
        // * INTERNALS
        // [Header("Internals")]

    // ? PROPERTIES=================================================================================================================================
        bool IWindow.IsOpen => this.gameObject.activeSelf;

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================
        void IWindow.Initialize(App.Game.Managers.MailDisplayer displayer) {
            this.displayer = displayer;
        }

        void IWindow.Bind(Mail.MailData data) {
            this.currentId = data.id;

            // Actualizar UI:
            // subject, body, attachments, etc.
        }

        void IWindow.Show() {
            this.gameObject.SetActive(true);
        }

        void IWindow.Show(Vector2 screenPosition) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.transform.parent as RectTransform,
                screenPosition,
                Camera.main,
                out Vector2 localPoint
            );

            ((RectTransform)this.transform).anchoredPosition  = localPoint;

            ((IWindow)this).Show();
        }

        void IWindow.Hide() {
            this.gameObject.SetActive(false);
        }

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public void OnRequestClose() {
            this.displayer.CloseWindow(this.currentId);
        }
    
        /// <summary>
        /// 
        /// </summary>
        public void OnRequestFocus() {
            this.displayer.FocusWindow(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData) {
            this.OnRequestFocus();
        }
    }
}