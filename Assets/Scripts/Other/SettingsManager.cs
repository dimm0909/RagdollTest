using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] internal float mouseSense;
        [SerializeField] internal float cameraMinimumVert;
        [SerializeField] internal float cameraMaximumVert;
        internal static SettingsManager instance { get; private set; }

        private void Awake()
        {
            if (instance != this)
                DestroyImmediate(instance);
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
}


