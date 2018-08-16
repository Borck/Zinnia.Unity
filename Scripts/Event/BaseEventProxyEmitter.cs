﻿namespace VRTK.Core.Event
{
    using UnityEngine;

    /// <summary>
    /// Forms the basis for all proxy emitters.
    /// </summary>
    public abstract class BaseEventProxyEmitter : MonoBehaviour
    {
        /// <summary>
        /// Determines if the emitter is valid.
        /// </summary>
        /// <returns><see langword="true"/> if the emitter is in a valid state.</returns>
        protected virtual bool IsValid()
        {
            return (isActiveAndEnabled);
        }
    }
}