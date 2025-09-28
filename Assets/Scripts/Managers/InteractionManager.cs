using UnityEngine.InputSystem;
using App.Game.Interaction;
using UnityEngine;
using System;
using System.ComponentModel;

namespace App.Game.Managers {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InteractionManager : MonoBehaviour {
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
        [SerializeField] private InputActionReference select;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private InputActionReference deselect;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private InputActionReference peek;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private InputActionReference interact;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private InputActionReference snap;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Camera gameplayCamera;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Transform pointer;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Transform hoverer;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Transform selector;

        // * ATTRIBUTES
        [Header("Attributes")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private LayerMask interactableLayer;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Vector2 dragOffset;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField, Min(0.1f)] private float dragThreshold = 1.0f;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField, Range(0.01f, 1.0f)] private float dragSmooth = 0.08f;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private bool centerItemOnDrag = false;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField, Range(0.01f, 2.0f)] private float gridSize = 1.0f;

        // * INTERNALS
        [Header("Internals")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Transform hoveredObject;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Transform selectedObject;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Transform draggedObject;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeReference, NonSerialized] private IHoverable hovered;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeReference, NonSerialized] private ISelectable selected;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeReference, NonSerialized] private IDraggable dragged;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeReference, NonSerialized] private IActivatable currentPeekIcon;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 pressScreenPosition;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 dragStartPosition;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 cursorPosition;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 currentOffset;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private Vector2 velocity;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private bool isDragging;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private bool selectPressed;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private bool snapActive;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private bool isPeeking;

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        private void OnEnable() {
            this.select.action.started += this.OnSelectDown;
            this.select.action.canceled += this.OnSelectUp;

            this.peek.action.performed += this.OnPeekDown;
            this.peek.action.canceled += this.OnPeekUp;

            this.snap.action.performed += this.OnSnapDown;
            this.snap.action.canceled += this.OnSnapUp;

            this.interact.action.performed += this.OnInteract;
            this.deselect.action.performed += this.OnDeselect;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnDisable() {
            this.select.action.started -= this.OnSelectDown;
            this.select.action.canceled -= this.OnSelectUp;

            this.peek.action.performed -= this.OnPeekDown;
            this.peek.action.canceled -= this.OnPeekUp;

            this.snap.action.performed -= this.OnSnapDown;
            this.snap.action.canceled -= this.OnSnapUp;

            this.interact.action.performed -= this.OnInteract;
            this.deselect.action.performed -= this.OnDeselect;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Awake() {
            if (!this.gameplayCamera)
                this.gameplayCamera = Camera.main;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update() {
            this.UpdateHover();
            this.UpdateDrag();

            if (this.hoveredObject && !this.isDragging) {
                this.hoverer.position = this.hoveredObject.position;
                this.hoverer.gameObject.SetActive(true);
            } else this.hoverer.gameObject.SetActive(false);
            
            if (this.selectedObject) {
                this.selector.position = this.selectedObject.position;
                this.selector.gameObject.SetActive(true);
            } else this.selector.gameObject.SetActive(false);
        }

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Vector2 GetCursorWorldPosition() {
            // TODO: Update to use pointer object position instead of mouse to support multiple input devices
            Vector2 mouse = Mouse.current.position.ReadValue();
            Vector3 screen = new Vector3(mouse.x, mouse.y, Mathf.Abs(this.gameplayCamera.transform.position.z));
            return this.gameplayCamera.ScreenToWorldPoint(screen);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateHover() {
            // TODO: Implement point input listening to place pointer 
            Vector2 world = this.GetCursorWorldPosition();
            this.pointer.position = world;

            if (this.isDragging) return;
            Collider2D hit = Physics2D.OverlapPoint(world, this.interactableLayer);

            Transform pointed = hit ? hit.transform : null;
            if (pointed == this.hoveredObject) return;

            this.hovered?.OnHoverExit();

            this.hoveredObject = pointed;
            this.hovered = null;

            if (this.hoveredObject)
                this.hoveredObject.TryGetComponent(out this.hovered);

            this.hovered?.OnHoverEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateDrag() {
            if (!this.selectPressed) return;

            Vector2 mouse = Mouse.current.position.ReadValue();

            if (!this.isDragging) {
                if ((mouse - this.pressScreenPosition).sqrMagnitude < this.dragThreshold * this.dragThreshold)
                    return;

                if (this.selected == null || !this.selectedObject.TryGetComponent(out this.dragged))
                    return;

                this.draggedObject = this.selectedObject;
                this.isDragging = true;

                Vector2 world = this.GetCursorWorldPosition();
                this.currentOffset = this.centerItemOnDrag
                    ? this.dragOffset
                    : (Vector2)this.draggedObject.position - world;

                this.dragStartPosition = this.draggedObject.position;
                this.velocity = Vector2.zero;

                this.dragged.OnDragStart(this.currentOffset, this.dragSmooth);
                return;
            }

            Vector2 cursor = this.GetCursorWorldPosition();
            this.cursorPosition = cursor + this.currentOffset;

            if (this.snapActive) {
                this.cursorPosition.x = Mathf.Round(this.cursorPosition.x / this.gridSize) * this.gridSize;
                this.cursorPosition.y = Mathf.Round(this.cursorPosition.y / this.gridSize) * this.gridSize;
            }

            Vector2 smoothed = Vector2.SmoothDamp(
                this.draggedObject.position,
                this.cursorPosition,
                ref this.velocity,
                this.dragSmooth
            );

            this.dragged.SetDragPosition(smoothed);
        }

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnSelectDown(InputAction.CallbackContext ctx) {
            this.selectPressed = true;
            this.pressScreenPosition = Mouse.current.position.ReadValue();

            if (this.hoveredObject && this.hoveredObject.TryGetComponent(out this.selected)) {
                this.selectedObject = this.hoveredObject;
                this.selected.OnSelect();
            } else {
                this.selectedObject = null;
                this.selected = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnSelectUp(InputAction.CallbackContext ctx) {
            this.selectPressed = false;

            if (this.isDragging && this.draggedObject && this.dragged != null) {
                this.isDragging = false;

                this.dragged.OnCompleteDrag(this.cursorPosition, this.velocity, this.dragSmooth);

                this.draggedObject = null;
                this.dragged = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnDeselect(InputAction.CallbackContext ctx) {
            if (this.draggedObject && this.dragged != null) {
                this.isDragging = false;

                Vector2 returnTarget = this.dragStartPosition;
                if (this.snapActive) {
                    returnTarget.x = Mathf.Round(returnTarget.x / this.gridSize) * this.gridSize;
                    returnTarget.y = Mathf.Round(returnTarget.y / this.gridSize) * this.gridSize;
                }

                this.dragged.OnCompleteDrag(returnTarget, this.velocity, this.dragSmooth);

                this.draggedObject = null;
                this.dragged = null;
            }

            this.selectPressed = false;
            this.selectedObject = null;
            this.selected = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnPeekDown(InputAction.CallbackContext ctx) {
            if (this.isDragging) return;

            // if (this.isPeeking) this.OnPeekUp(ctx);

            if (this.hoveredObject && this.hoveredObject.TryGetComponent<IActivatable>(out IActivatable a)) {
                // ! Turn on later when close UI button is added
                // if (a.IsEnabled) return;
                a.OnActivate();

                this.currentPeekIcon = a;
                this.isPeeking = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnPeekUp(InputAction.CallbackContext ctx) {
            if (!this.isPeeking || this.currentPeekIcon == null) return;

            this.currentPeekIcon.OnDeactivate();
            this.currentPeekIcon = null;

            this.isPeeking = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnSnapDown(InputAction.CallbackContext ctx) {
            this.snapActive = true;
            if (DEBUG) Debug.Log("Snap active");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnSnapUp(InputAction.CallbackContext ctx) {
            this.snapActive = false;
            if (DEBUG) Debug.Log("Snap inactive");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        private void OnInteract(InputAction.CallbackContext ctx) {
            if (this.isPeeking) return;

            if (this.selectedObject && this.selectedObject.TryGetComponent<IActivatable>(out IActivatable a))
                a.OnActivate();
        }
    }
}