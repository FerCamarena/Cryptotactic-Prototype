
using System.Collections.Generic;
using App.Game.Mail;
using UnityEngine;
using System;

namespace App.Game.Managers {
    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MailSpawner: IMailIconProvider {
    // ? DEBUG======================================================================================================================================
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
        [NonSerialized] private readonly Queue<MailIcon> mailIconPool;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private readonly List<MailIcon> activeMailIcons;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private readonly GameObject mailIconPrefab;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private readonly Transform mailIconParent;
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [NonSerialized] private readonly IMailIconHandler mailIconHandler;

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public MailSpawner(GameObject prefab, Transform parent, IMailIconHandler handler) {
            this.mailIconPrefab = prefab;
            this.mailIconParent = parent;
            this.mailIconHandler = handler;
            
            this.mailIconPool = new Queue<MailIcon>();
            this.activeMailIcons = new List<MailIcon>();
        }

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public void OnEnqueueMailIcon(MailIcon icon) {
            if (!this.activeMailIcons.Remove(icon))
                return;

            icon.Reset();

            this.mailIconPool.Enqueue(icon);
        }

        GameObject IMailIconProvider.OnSpawnMailIcon(MailData data) {
            GameObject mailIconGO = ((IMailIconProvider)this).OnGetMailIcon();

            MailIcon mailIcon = mailIconGO.GetComponent<MailIcon>();
            
            mailIcon.Initialize(data, this.mailIconHandler);
    
            this.activeMailIcons.Add(mailIcon);

            return mailIconGO;
        }

        GameObject IMailIconProvider.OnGetMailIcon() {
            GameObject mailIconGO = this.mailIconPool.Count > 0
                ? this.mailIconPool.Dequeue().gameObject
                : UnityEngine.Object.Instantiate(this.mailIconPrefab, this.mailIconParent);

            return mailIconGO;
        }
    }
}