using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive
{
    public class BaseInteractive : MonoBehaviour
    {
        internal Rigidbody rb { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (!rb)
                rb = this.gameObject.AddComponent<Rigidbody>();
        }
    }
}


