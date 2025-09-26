using UnityEngine;

namespace App.Game.Managers {
    /// <summary>
    /// Game level scene local manager. Handles level-specific logic, events, and interactions from gameplay loop.
    /// </summary>
    public class GameLevelManager : MonoBehaviour {
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