using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseInteractive : MonoBehaviour
    {
        internal Rigidbody rb { get; private set; }

        protected virtual void Awake() => rb = GetComponent<Rigidbody>();

        internal virtual void Grab(Vector3 targetPoint)
        {
            rb.isKinematic = true;
            rb.MovePosition(targetPoint);
        }
    }
}


