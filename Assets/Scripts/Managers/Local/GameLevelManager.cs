using UnityEngine;

namespace App.Game.Managers {
    /// <summary>
    /// Game level scene local manager. Handles level-specific logic, events, and interactions from gameplay loop.
    /// </summary>
    [DisallowMultipleComponent]
    public class GameLevelManager : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [Header("DEBUGGING")]
        [Tooltip("")]
        [SerializeField] private static bool DEBUG_ALL = false;
        [Tooltip("")]
        [SerializeField] private bool DEBUG_INSTANCE = false;
        
    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNALS

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}