using UnityEngine;

namespace MegaSkill.Utils
{
    public class Rotator : MonoBehaviour
    {
        public Vector3 rotation;

        void FixedUpdate()
        {
            transform.Rotate(rotation*Time.fixedDeltaTime);
        }
    }
}
