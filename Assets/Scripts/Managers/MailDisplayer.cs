using System.Collections.Generic;
using App.Game.Mail;
using App.Game.UI;
using UnityEngine;

namespace App.Game.Managers {
    /// <summary>
    /// 
    /// </summary>
        [DisallowMultipleComponent]
        public sealed class MailDisplayer {
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
        private readonly Dictionary<string, IWindow> cachedWindows;
        /// <summary>
        /// 
        /// </summary>
        private readonly Queue<IWindow> windowsPool;
        /// <summary>
        /// 
        /// </summary>
        private readonly Transform windowsUI;
        /// <summary>
        /// 
        /// </summary>
        private readonly GameObject windowPrefab;

    // ? PROPERTIES=================================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public MailDisplayer(GameObject prefab, Transform parent) {
            this.windowPrefab = prefab;
            this.windowsUI = parent;

            this.cachedWindows = new Dictionary<string, IWindow>();
            this.windowsPool = new Queue<IWindow>();
        }
    // ? BASE METHODS===============================================================================================================================
    
    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public void OpenOrFocusWindow(Mail.MailIcon icon) {
            MailData data = icon.MailData;

            if (this.cachedWindows.TryGetValue(data.id, out IWindow window)) {
                if (!window.IsOpen) window.Show();

                this.FocusWindow(window);

                if (DEBUG)
                    Debug.Log($"Focused cached window for {data.id}");
            } else this.OpenWindow(icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        public void FocusWindow(IWindow window) {
            Transform t = ((MonoBehaviour)window).transform;
            t.SetAsLastSibling();
            
            if (DEBUG)
                Debug.Log($"Focused window for {window}");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenWindow(Mail.MailIcon icon) {
            MailData data = icon.MailData;
            IWindow newWindow = this.GetWindow(data);
            newWindow.Bind(data);

            Vector2 worldPosition = icon.transform.position;
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            newWindow.Show(screenPosition);

            this.cachedWindows.Add(data.id, newWindow);
            
            if (DEBUG)
                Debug.Log($"Binded and cached window for {data.id}");
            
            this.FocusWindow(newWindow);
            
            if (DEBUG)
                Debug.Log($"Focused new window for {data.id}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IWindow GetWindow(MailData data) {
            IWindow window;

            if (this.windowsPool.Count > 0) {
                window = this.windowsPool.Dequeue();
                if (DEBUG)
                    Debug.Log($"Reused window for {data.id}");
            } else {
                window = this.CreateWindow().GetComponent<IWindow>();
                    
                if (window == null) {
                    Debug.LogError("[MD] Window prefab missing IWindow implementation");
                    return null;
                }
                window.Initialize(this);

                if (DEBUG)
                    Debug.Log($"Created window for {data.id}");
            }

            return window;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GameObject CreateWindow() => Object.Instantiate(this.windowPrefab, this.windowsUI);

        /// <summary>
        /// 
        /// </summary>
        public void CloseWindow(string id) {
            if (!this.cachedWindows.TryGetValue(id, out IWindow window))
                return;

            window.Hide();

            if (DEBUG)
                Debug.Log($"Hidden cached window for {id}");
        }

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public void OnIconDestruction(string id) {
            if (!this.cachedWindows.TryGetValue(id, out IWindow window))
                return;
            
            this.cachedWindows.Remove(id);

            this.CloseWindow(id);

            this.windowsPool.Enqueue(window);

            if (DEBUG)
                Debug.Log($"Icon destruction detected. {id} window set for reuse");
        }
    }
}