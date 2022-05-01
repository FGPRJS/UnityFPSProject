using Contents.Controller.Player;
using Contents.Mechanic;
using Contents.Skill;
using UnityEngine;
using UnityEngine.UI;

namespace Contents.UI
{
    public class CastingGuage : MonoBehaviour
    {
        public Slider slider;
        public Player player;
        private ASkill targetSkill;
        
        private void Awake()
        {
            slider.minValue = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
            targetSkill = player.target.skillReload;
            slider.maxValue = targetSkill.Casttime;
        }

        // Update is called once per frame
        void Update()
        {
            slider.value = targetSkill.CurrentCasttime;

            if(slider.value == 0)
            {
                UIUtility.HideObject(gameObject);
            }
            else
            {
                UIUtility.ShowObject(gameObject);
            }
        }
    }
}
