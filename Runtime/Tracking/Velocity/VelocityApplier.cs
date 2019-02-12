﻿namespace Zinnia.Tracking.Velocity
{
    using Malimbe.XmlDocumentationAttribute;
    using UnityEngine;

    /// <summary>
    /// Applies the velocity data from the given <see cref="VelocityTracker"/> to the given <see cref="Rigidbody"/>.
    /// </summary>
    public class VelocityApplier : MonoBehaviour
    {
        /// <summary>
        /// The source <see cref="VelocityTracker "/> to receive the velocity data from.
        /// </summary>
        [DocumentedByXml]
        public VelocityTracker source;
        /// <summary>
        /// The target <see cref="Rigidbody"/> to apply the source velocity data to.
        /// </summary>
        [DocumentedByXml]
        public Rigidbody target;

        /// <summary>
        /// Applies the velocity data to the <see cref="target"/>.
        /// </summary>
        public virtual void Apply()
        {
            if (source == null || target == null)
            {
                return;
            }

            target.velocity = source.GetVelocity();
            target.angularVelocity = source.GetAngularVelocity();
        }
    }
}