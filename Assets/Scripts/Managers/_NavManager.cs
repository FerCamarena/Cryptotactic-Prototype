using UnityEngine;

namespace App.Managers {
    /// <summary>
    /// App static navigation singleton. Manages app navigation, screen transitions, and routing between different sections of the app. 
    /// </summary>
    public class _NavManager {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enable debug logs for this script. (Default: false)")]
        [SerializeField] private bool DEBUG = false;
        public static _NavManager Instance {
            get {
                if (_instance == null) {
                    _instance = new _NavManager();
                }
                return _instance;
            }
        }

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        private static _NavManager _instance;

        // * ATTRIBUTES

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}