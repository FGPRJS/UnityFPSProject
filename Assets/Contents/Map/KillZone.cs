using Contents.Mechanic;
using UnityEngine;

namespace Contents.Map
{
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            var target = other.gameObject.GetComponent<AMecha>();

            if (target)
            {
                target.Damage(99999999);
            }
        }
    }
}
