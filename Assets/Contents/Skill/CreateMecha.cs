using UnityEngine;

namespace Contents.Skill
{
    public class CreateMecha : ASkill
    {
        public GameObject instantiateTarget;

        protected override void Action()
        {
            var thisTransform = transform;
            Instantiate(instantiateTarget, thisTransform.position, thisTransform.rotation);
        }
    }
}
