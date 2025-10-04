using UnityEngine;

namespace App.Game.Managers {
    /// <summary>
    /// App loader scene local manager. Manages app initialization, crash handling, exit, resume and other app-wide events. 
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class AppLoadManager : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [Header("DEBUGGING"), Tooltip("Enable debug logs for all instances of this script. (Default: false)")]
        [SerializeField] private static bool DEBUG_ALL = false;
        [Tooltip("Enable debug logs for this script instance. (Default: false)")]
        [SerializeField] private bool DEBUG_INSTANCE = false;
        //[Space(5)]
        
    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        //private void Awake() {}

        //private void Start() {}

        //private void Update() {}
    
        //private void FixedUpdate() {}

        //private void LateUpdate() {}
        
        //private void OnValidate() {}

        //private void OnEnable() {}

        //private void OnDisable() {}

        //private void OnDestroy() {}


    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}