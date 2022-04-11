/*
 * @author Valentin Simonov / http://va.lent.in/
 * @updated by Kevin Stafferton (Inition)
 */

using TouchScript.Gestures.Base;
using TouchScript.Behaviors;
using TouchScript.Layers;
using TouchScript.Utils.Geom;
using UnityEngine;
using System.Collections.Generic;

namespace TouchScript.Gestures
{
    /// <summary>
    /// Recognizes a transform gesture in screen space, i.e. translation, rotation, scaling or a combination of these.
    /// Added momentum for single touch transformations
    /// </summary>
    [AddComponentMenu("TouchScript/Gestures/Screen Drag Transform Gesture")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ScreenDragTransformGesture : ScreenTransformGesture
    {
        private TargetJoint2D targetJoint2D;
        private Rigidbody2D rigidbody2D;

        protected override void Awake()
        {
            base.Awake();

            targetJoint2D = GetComponent<TargetJoint2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            if (targetJoint2D == null)
            {
                targetJoint2D = gameObject.AddComponent<TargetJoint2D>();
            }
            if (rigidbody2D == null)
            {
                rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            }
            targetJoint2D.enabled = false; 
        }

        /// <inheritdoc />
        protected override Vector3 doOnePointTranslation(Vector2 oldScreenPos, Vector2 newScreenPos,
                                                         ProjectionParams projectionParams)
        {
            if (rigidbody2D.isKinematic)
                rigidbody2D.isKinematic = false;

            if (isTransforming)
            {
                if(targetJoint2D.target != newScreenPos)
                {
                    targetJoint2D.target = newScreenPos;
                }
                return Vector3.zero;
            }

            screenPixelTranslationBuffer += newScreenPos - oldScreenPos;
            if (screenPixelTranslationBuffer.sqrMagnitude > screenTransformPixelThresholdSquared)
            {
                isTransforming = true;  
                rigidbody2D.isKinematic = true;
                Debug.Log("Touch Position=" + newScreenPos + "; RigidBody Position=" + rigidbody2D.gameObject.transform.position);
                if (Vector2.Distance(rigidbody2D.position, newScreenPos) > 100.0f)
                {
                    targetJoint2D.anchor = transform.InverseTransformPoint(newScreenPos);
                }
                else
                {
                    targetJoint2D.anchor = transform.InverseTransformPoint(rigidbody2D.gameObject.transform.position);
                }
                targetJoint2D.enabled = true;
                return screenPixelTranslationBuffer;
            }
            return Vector3.zero;
        }

        protected override Vector3 doTwoPointTranslation(Vector2 oldScreenPos1, Vector2 oldScreenPos2, Vector2 newScreenPos1, Vector2 newScreenPos2, float dR, float dS, ProjectionParams projectionParams)
        {
            targetJoint2D.enabled = false;
            return base.doTwoPointTranslation(oldScreenPos1, oldScreenPos2, newScreenPos1, newScreenPos2, dR, dS, projectionParams);
        }

        protected override void reset()
        {
            base.reset();
            targetJoint2D.enabled = false; 
        }

    }
}