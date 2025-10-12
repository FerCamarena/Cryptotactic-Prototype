using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace App.Game.UI {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class WindowHandle : MonoBehaviour, IPointerDownHandler, IDragHandler {
    // ? DEBUG======================================================================================================================================
        // * DEBUG
        [Header("Debug")]
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [SerializeField] private bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        // [Header("References")]
         
        // * ATTRIBUTES
        // [Header("Attributes")]
        
        // * INTERNALS
        [Header("Internals")]
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private MonoBehaviour windowGO;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private RectTransform windowTransform;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private IWindow window;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 offset;

    // ? PROPERTIES=================================================================================================================================

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        private void Awake() {
            this.ValidateReferences();
        }

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        private void ValidateReferences() {
            if (!this.transform.parent.TryGetComponent<IWindow>(out IWindow window)) {
                Debug.LogError("[W] Parent Window not found!");
            } else if (this.window == null && window != null) {
                this.window = window;
            } else if (this.window != window) {
                this.window = window;
            }
            this.windowTransform = this.transform.parent as RectTransform;  
        }

    // ? EVENT METHODS==============================================================================================================================
        public void OnPointerDown(PointerEventData eventData) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.windowTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );

            offset = (Vector2)this.windowTransform.anchoredPosition - localPoint;

            this.window.OnRequestFocus();
        }

        public void OnDrag(PointerEventData eventData) {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.windowTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            )) {
                this.windowTransform.anchoredPosition = localPoint + offset;
            }
        }
    }
}