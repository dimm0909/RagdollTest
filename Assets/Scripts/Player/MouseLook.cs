using UnityEngine;
using System.Collections;

namespace Movement
{
    [AddComponentMenu("Control Script/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        public enum RotationAxes
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }
        public RotationAxes axes = RotationAxes.MouseXAndY;

        private float sensitivity = 9.0f;

        public float minimumVert = -45.0f;
        public float maximumVert = 45.0f;

        private float _rotationX = 0;


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
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
            }
            else
            {
                float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

                _rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }
}