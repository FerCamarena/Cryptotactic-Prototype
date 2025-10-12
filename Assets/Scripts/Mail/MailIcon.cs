using App.Game.Interaction;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using App.Game.Managers;

namespace App.Game.Mail {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MailIcon : MonoBehaviour, IMail, IHoverable, IActivatable, IDraggable, ISelectable {
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
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Camera gameplayCamera;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [SerializeField] private GameObject unreadIcon;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [SerializeField] private SortingGroup sortingGroup; // TODO: RequireComponent and OnValidate

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
        [NonSerialized] private IMailIconHandler handler;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private MailData data;
        /// <summary>
        ///
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private MailState state;
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
        public MailData MailData => this.data;
        // TODO: Update to improve use-case with int, enum or struct for procedural typesafe id generation
        public string Id => this.MailData.id;
        bool IActivatable.IsEnabled => this.gameObject.activeSelf;

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        private void Awake() {
            if (!this.gameplayCamera) {
                this.gameplayCamera = Camera.main;

                if (DEBUG)
                    Debug.Log("[MI] Missing camera reference. Local main camera assigned");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void LateUpdate() {
            if (((Vector2)this.transform.position - this.lastPosition).sqrMagnitude < 0.001f)
                return;
            this.lastPosition = this.transform.position;

            this.sortingGroup.sortingOrder = Mathf.RoundToInt(-this.transform.position.y * 100.0f);
        }

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        ///
        /// </summary>
        internal void Initialize(MailData data, IMailIconHandler handler) {
            if (this.state == MailState.Active) {
                Debug.LogWarning("[MI] MailIcon already active. Missing reset call before re-initialization?");
                this.Reset();
            }

            this.data = data;
            this.handler = handler;
            this.state = MailState.Active;
            this.gameObject.SetActive(false);

            if (DEBUG)
                Debug.Log($"[MI] Mail Initialized: {this.MailData.subject}");
        }

        /// <summary>
        ///
        /// </summary>
        internal void Reset() {
            if (this.state == MailState.None) return;

            this.gameObject.SetActive(false);
            this.data = default;
            this.handler = null;
            this.state = MailState.None;

            if (DEBUG)
                Debug.Log("[MI] Reset completed.");
        }

        /// <summary>
        ///
        /// </summary>
        // ? Should it be a event styled call since is called from external objects?
        internal void Release() {
            if (this.state != MailState.Active) {
                Debug.LogWarning("[MI] Invalid Release call: MailIcon not active.");
                return;
            }

            this.state = MailState.Processed;
            this.handler?.OnMailIconReleased(this);

            if (DEBUG)
                Debug.Log($"[MI] Mail Released: {this.Id}");
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

            if (this.unreadIcon && this.unreadIcon.activeSelf) this.unreadIcon.SetActive(false);

            this.handler?.OnMailWindowInteract(this);

            if (DEBUG)
                Debug.Log($"[MI] Mail {this.Id} activated");
        }

        void IActivatable.OnDeactivate() {
            if (this.dragging) return;

            this.handler?.OnMailWindowClose(this);

            if (DEBUG)
                Debug.Log($"[MI] Mail {this.Id} deactivated");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum MailState {
        None,
        Active,
        Processed,
    }
}