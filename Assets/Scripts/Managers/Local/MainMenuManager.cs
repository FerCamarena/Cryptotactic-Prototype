using UnityEngine;

namespace App.Game.Managers {
    /// <summary>
    /// Main menu scene local manager. Handles UI interactions, scene behaviour, events and other menu-specific logic.
    /// </summary>
    [DisallowMultipleComponent]
    public class MainMenuManager : MonoBehaviour {
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

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}