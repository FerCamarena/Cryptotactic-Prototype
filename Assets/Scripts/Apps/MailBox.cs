using System.Collections.Generic;
using App.Game.Interaction;
using System.Collections;
using App.Game.Managers;
using UnityEngine;
using System;
using TMPro;

namespace App.Game.Mail {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MailBox : MonoBehaviour, IHoverable, IActivatable, ISelectable, IDraggable {
    // ? DEBUG======================================================================================================================================
        // * DEBUG
        [Header("Debug")]
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [SerializeField] private bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private readonly Queue<GameObject> pendingMails = new Queue<GameObject>();
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Coroutine dragRoutine;
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
        bool IActivatable.IsEnabled => false;
        // public bool Dragging => this.dragging;

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        private void Start() {
        }
        
    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newMail"></param>

        void IDraggable.StopActiveDragRoutine() {
            this.StopCoroutine(this.dragRoutine);
            this.dragVelocity = Vector2.zero;
            this.dragRoutine = null;
        }

        void IDraggable.SetDragPosition(Vector2 position) {
            if (!this.dragging) return;

            this.transform.position = position;

            if (DEBUG)
                Debug.Log($"[MB] Drag position set at: {position}");
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
                Debug.Log($"[MB] Start Drag: {this.name}");
        }

        void IDraggable.OnCompleteDrag(Vector2 targetPosition, Vector2 currentVelocity, float smoothTime) {
            if (this.dragRoutine != null) ((IDraggable)this).StopActiveDragRoutine();

            this.dragVelocity = currentVelocity;
            this.dragRoutine = this.StartCoroutine(((IDraggable)this).MoveTo(targetPosition, smoothTime));
            this.dragging = false;

            if (DEBUG)
                Debug.Log($"[MB] Complete Drag: {this.name}");
        }

        void IDraggable.OnDragEnd() {
            if (DEBUG)
                Debug.Log($"[MB] Drag End: {this.name}"); //Queueing end drag corroutine
        }

        void IDraggable.OnDragCancel() {
            if (DEBUG)
                Debug.Log($"[MB] Drag Cancelled: {this.name}");
        }

        void ISelectable.OnSelect() { }

        void ISelectable.OnDeselect() { }

        void IActivatable.OnActivate() {
                
            GameObject mailIcon = this.pendingMails.Dequeue();


            mailIcon.gameObject.SetActive(true);

            if (DEBUG)
                Debug.Log($"[MB] Dropped {mailIcon} to gameground");
        }

        void IActivatable.OnDeactivate() { }

        void IHoverable.OnHoverEnter() { }
        
        void IHoverable.OnHoverExit() { }
    }
}