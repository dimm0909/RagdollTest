using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive
{
    public class Character : BaseInteractive
    {
        [SerializeField] private Animator anim;
        private bool waitingLanding = true;

        internal override void Grab(Vector3 targetPosition)
        {
            StopAllCoroutines();
            if (anim.enabled)
                anim.enabled = false;
            waitingLanding = true;
            base.Grab(targetPosition);
        }

        private void OnCollisionEnter(Collision collision)
        {
            StopAllCoroutines();

            if (waitingLanding)
                waitingLanding = false;
            StartCoroutine(TryStandUp(5));
        }

        private IEnumerator TryStandUp(float sec)
        {
            yield return new WaitForSeconds(sec);
            if (!waitingLanding)
            {
                anim.enabled = true;
                anim.transform.position = new Vector3(this.transform.position.x, 0, anim.transform.position.z);
            }
            waitingLanding = true;
        }
    }
}
