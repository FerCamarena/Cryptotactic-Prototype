using UnityEngine;

namespace App.Managers {
    /// <summary>
    /// App lifecycle audio static manager singleton. Manages app audio settings load, save, handling, events and other audio-related functions. 
    /// </summary>
    public class _AudioManager {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enable debug logs for this script. (Default: false)")]
        [SerializeField] private readonly bool DEBUG = false;
        public static _AudioManager Instance {
            get {
                if (_instance == null) {
                    _instance = new _AudioManager();
                }
                return _instance;
            }
        }

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        private static _AudioManager _instance;

        // * ATTRIBUTES

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}