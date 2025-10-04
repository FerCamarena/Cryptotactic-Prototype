using App.Game.Interaction;
using System.Collections;
using UnityEngine;
using System;

namespace App.Game.Mail {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TestIcon : MonoBehaviour, IHoverable, IActivatable, IDraggable, ISelectable {
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
        [Header("References")]

        // * ATTRIBUTES
        // [Header("Attributes")]
        
        // * INTERNALS
        [Header("Internals")]
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Coroutine dragRoutine;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 lastPosition;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 dragVelocity;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private bool dragging;

    // ? PROPERTIES=================================================================================================================================
        bool IActivatable.IsEnabled => this.gameObject.activeSelf;

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

        /// <summary>
        ///
        /// </summary>
        internal void Reset() {
            this.gameObject.SetActive(false);

            if (DEBUG)
                Debug.Log("[MI] Reset completed.");
        }

        /// <summary>
        ///
        /// </summary>
        internal void Release() {
            if (DEBUG)
                Debug.Log($"[MI] Mail Released");
        }

        void IDraggable.StopActiveDragRoutine() {
            this.StopCoroutine(this.dragRoutine);
            this.dragVelocity = Vector2.zero;
            this.dragRoutine = null;
        }

        void IDraggable.SetDragPosition(Vector2 position) {
            if (!this.dragging) return;

            this.transform.position = position;

            if (DEBUG)
                Debug.Log($"[MI] Drag position set at: {position}");
        }

        bool IDraggable.Reached(Vector2 origin, Vector2 target) => Vector2.Distance(origin, target) <= 0.01f;

        IEnumerator IDraggable.MoveTo(Vector2 target, float smoothTime) {
            while (!((IDraggable)this).Reached(this.transform.position, target)) {
                this.transform.position = Vector2.SmoothDamp(this.transform.position, target, ref this.dragVelocity, smoothTime);
                yield return null;
            }

            this.transform.position = target;
            if (this.dragRoutine != null) ((IDraggable)this).StopActiveDragRoutine();

            if (DEBUG)
                Debug.Log($"[MI] MoveToTarget completed: {target}");
        }

    // ? EVENT METHODS==============================================================================================================================
        void IDraggable.OnDragStart(Vector2 offset, float smoothTime) {
            this.dragging = true;

            if (this.dragRoutine != null) ((IDraggable)this).StopActiveDragRoutine();

            if (DEBUG)
                Debug.Log($"[MI] Start Drag: {this.name}");
        }

        void IDraggable.OnCompleteDrag(Vector2 targetPosition, Vector2 currentVelocity, float smoothTime) {
            if (this.dragRoutine != null) ((IDraggable)this).StopActiveDragRoutine();

            this.dragVelocity = currentVelocity;
            this.dragRoutine = this.StartCoroutine(((IDraggable)this).MoveTo(targetPosition, smoothTime));
            this.dragging = false;

            if (DEBUG)
                Debug.Log($"[MI] Complete Drag: {this.name}");
        }

        void IDraggable.OnDragEnd() {
            if (DEBUG)
                Debug.Log($"[MI] Drag End: {this.name}");
        }

        void IDraggable.OnDragCancel() {
            if (DEBUG)
                Debug.Log($"[MI] Drag Cancelled: {this.name}");
        }

        void IHoverable.OnHoverEnter() { }

        void IHoverable.OnHoverExit() { }

        void ISelectable.OnSelect() { }

        void ISelectable.OnDeselect() { }

        void IActivatable.OnActivate() {
            if (this.dragging) return;

            if (DEBUG)
                Debug.Log($"[MI] Mail activated");
        }

        void IActivatable.OnDeactivate() {
            if (this.dragging) return;

            if (DEBUG)
                Debug.Log($"[MI] Mail deactivated");
        }
    }
}