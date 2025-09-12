using UnityEngine;

namespace App.Managers {
    /// <summary>
    /// App lifecycle saves static manager singleton. Manages app savestates and settings load, save and other save-related functions.
    /// </summary>
    public class _SavesManager {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enable debug logs for this script. (Default: false)")]
        [SerializeField] private readonly bool DEBUG = false;
        public static _SavesManager Instance {
            get {
                if (_instance == null) {
                    _instance = new _SavesManager();
                }
                return _instance;
            }
        }

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        private static _SavesManager _instance;

        // * ATTRIBUTES

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}