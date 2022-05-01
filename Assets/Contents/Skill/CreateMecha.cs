using UnityEngine;

namespace Contents.Skill
{
    public class CreateMecha : ASkill
    {
        public GameObject instantiateTarget;

        protected override void Action()
        {
            Instantiate(instantiateTarget, transform.position, transform.rotation);
        }
    }
}
