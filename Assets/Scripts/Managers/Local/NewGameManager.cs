using UnityEngine;

namespace App.Managers {
    /// <summary>
    /// New Game scene local manager. Handles UI interactions, scene behaviour, events and other menu-specific logic.
    /// </summary>
    public class NewGameManager : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [Header("DEBUGGING"), Tooltip("Enable debug logs for all instances of this script. (Default: false)")]
        [SerializeField] private static readonly bool DEBUG_ALL = false;
        [Tooltip("Enable debug logs for this script instance. (Default: false)")]
        [SerializeField] private readonly bool DEBUG_INSTANCE = false;
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