using System.Collections;
using UnityEngine;

namespace App.Game.Interaction {
    /// <summary>
    /// 
    /// </summary>
    public interface IDraggable {
    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        // ! Duplicated per-instance, refactor for base class override, abstract or composition and relies in stateful for a default implementation
        void StopActiveDragRoutine();
        /// <summary>
        /// 
        /// </summary>
        // ! Duplicated per-instance, refactor for base class override, abstract or composition and relies in stateful for a default implementation
        bool Reached(Vector2 origin, Vector2 target);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        // ! Duplicated per-instance, refactor for base class override, abstract or composition and relies in stateful for a default implementation
        void SetDragPosition(Vector2 position);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="smoothTime"></param>
        /// <returns></returns>
        // ! Duplicated per-instance, refactor for base class override, abstract or composition and relies in stateful for a default implementation
        IEnumerator MoveTo(Vector2 target, float smoothTime);
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="smoothTime"></param>
        void OnDragStart(Vector2 offset, float smoothTime);
        /// <summary>
        ///
        /// </summary>
        void OnDragEnd();
        /// <summary>
        ///
        /// </summary>
        void OnDragCancel();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="currentVelocity"></param>
        /// <param name="smoothTime"></param>
        void OnCompleteDrag(Vector2 targetPosition, Vector2 currentVelocity, float smoothTime);
    }
}