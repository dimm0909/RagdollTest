using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private const float USAGE_DISTANCE = 5f;

        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject uiTip;
        [SerializeField] private GameObject npcPrefab;

        internal bool mayUseItem = false;

        private Interactive.BaseInteractive itemToInteract = null;

        private void Awake()
        {
            mayUseItem = false;
            itemToInteract = null;
            uiTip.SetActive(false);
        }


        private void Update()
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, USAGE_DISTANCE))
            {
                Interactive.BaseInteractive isInteractive = hit.transform.gameObject.GetComponent<Interactive.BaseInteractive>();
                mayUseItem = isInteractive != null;

                if (Input.GetKey(KeyCode.E) && isInteractive && uiTip.activeSelf)
                    uiTip.SetActive(false);
                else if (isInteractive && !uiTip.activeSelf)
                    uiTip.SetActive(true);
                else if (!isInteractive)
                    ResetInteractiveItem();

                itemToInteract = isInteractive;
            }
            else
                ResetInteractiveItem();

            if (mayUseItem && itemToInteract != null)
            {
                if (!uiTip.activeSelf)
                    uiTip.SetActive(true);

                if (Input.GetKey(KeyCode.E))
                {
                    Vector3 p = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                    Ray r = _camera.ScreenPointToRay(p);

                    Vector3 directPoint = ray.GetPoint(USAGE_DISTANCE -3);
                    itemToInteract.Grab(directPoint);
                }
            }
            else if ((!mayUseItem || itemToInteract == null) && uiTip.activeSelf)
            {
                uiTip.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject b = Instantiate(npcPrefab);
                b.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y + 2, this.gameObject.transform.position.z) ;
            }
        }
        private void ResetInteractiveItem()
        {
            if (itemToInteract)
                itemToInteract.rb.isKinematic = false;
            mayUseItem = false;
            itemToInteract = null;
            uiTip.SetActive(false);
        }
    }
}