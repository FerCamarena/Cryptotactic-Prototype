using App.Game.Mail;
using UnityEngine;
using System;

namespace App.Game.Managers {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MailManager : MonoBehaviour, IMailIconHandler {
    // ? DEBUG================================================================================================================================
        [Header("Debug")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private bool DEBUG = false;

    // ? PARAMETERS===========================================================================================================================
        // * REFERENCES
        [Header("References")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private GameObject mailViewPrefab;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private GameObject mailIconPrefab;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Transform mailViewParent;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private Transform mailIconParent;
        
        // * ATTRIBUTES
        [Header("Attributes")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private MailGenerator generator = new MailGenerator();
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private float spawnInterval = 5.0f;

        // * INTERNALS
        [Header("Internals")]
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private MailDisplayer displayer;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private MailSpawner spawner;
        /// <summary>
        /// 
        /// </summary>
        private IMailIconProvider provider => this.spawner;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private float timer;

        // TODO: Refactor
        // TODO: Generate mailbox at start instead of giving direct reference of it
        // TODO: alongside of updating MailBox for dependency injection of its reference to IMailIconProvider *this
        public MailBox mailBox;

    // ? BASE METHODS==========================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        private void Awake() {
            this.displayer = new MailDisplayer(
                this.mailViewPrefab,
                this.mailViewParent
            );

            this.spawner = new MailSpawner(
                prefab: this.mailIconPrefab,
                parent: this.mailIconParent,
                handler: this
            );

            this.mailBox.iconProvider = this.provider;

            this.timer = 0.0f;

            if (DEBUG)
                Debug.Log("[MM] Values and dependecies initialized");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update() {
            this.timer += Time.deltaTime;

            if (this.timer >= this.spawnInterval) {
                this.timer = 0.0f;

                this.InstantiateMailIcon();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InstantiateMailIcon() {
            // TODO: Refactor
            // TODO: Update to store only MailData and notify MailBox changes on amount enqueued instead of it storing and counting them
            // TODO: Update to only instantiate from provider on request from Mailbox IActivatable.OnActivate instead of storing them in
            MailData newData = this.generator.OnGenerateMailData();
            GameObject newIcon = this.provider.OnSpawnMailIcon(newData);
            this.mailBox.AddIncomingMail(newIcon);

            if (DEBUG)
                Debug.Log("[MM] Incoming mail requested");
        }

    // ? CUSTOM METHODS=============================================================================================================================
        void IMailIconHandler.OnMailIconReleased(MailIcon icon) {
            this.displayer.OnIconDestruction(icon.Id);

            this.spawner.OnEnqueueMailIcon(icon);
            
            if (DEBUG)
                Debug.Log("[MM] Mail icon released");
        }

        void IMailIconHandler.OnMailWindowInteract(MailIcon icon) {
            this.displayer.OpenOrFocusWindow(icon);
            
            if (DEBUG)
                Debug.Log("[MM] Mail icon window opened and focused");
        }

        void IMailIconHandler.OnMailWindowClose(MailIcon icon) {
            this.displayer.CloseWindow(icon.Id);
            
            if (DEBUG)
                Debug.Log("[MM] Mail icon window closed");
        }
    }
}