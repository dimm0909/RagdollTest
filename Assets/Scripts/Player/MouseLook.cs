using UnityEngine;
using System.Collections;

namespace Movement
{
    [AddComponentMenu("Control Script/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform playerBody;


        public enum RotationAxes
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }
        public RotationAxes axes = RotationAxes.MouseXAndY;

        private float sensitivity = 1;

        public float minimumVert = -45.0f;
        public float maximumVert = 45.0f;

        private float _rotationX = 0;

        private Vector2 startPos;
        private float startRot;
        private Quaternion originalBodyRot;


        void Start()
        {
            sensitivity = Settings.SettingsManager.instance.mouseSense;
            minimumVert = Settings.SettingsManager.instance.cameraMinimumVert;
            maximumVert = Settings.SettingsManager.instance.cameraMaximumVert;

            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null)
                body.freezeRotation = true;
        }


        void Update()
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        startRot = transform.localEulerAngles.x;
                        originalBodyRot = playerBody.rotation;
                        break;

                    case TouchPhase.Moved:
                        var delta = Vector2.Scale(touch.position - startPos, Vector2.one * sensitivity);

                        var newRot = Mathf.Clamp(startRot - delta.y, -90, 90);
                        transform.localEulerAngles = new Vector3(newRot, 0, 0);

                        playerBody.rotation = originalBodyRot * Quaternion.Euler(0, delta.x, 0);
                        break;
                }
            }
        }
    }
}