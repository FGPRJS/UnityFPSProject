using Contents.Controller.Player;
using Contents.Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Contents.UI
{
    public class SkillIcon : MonoBehaviour
    {
        public Player player;
        public ASkill targetSkill;

        public Image image;
        public Slider slider;
        public TextMeshProUGUI cooltimeText;

        private void Awake()
        {
            slider.minValue = 0;
            slider.maxValue = targetSkill.Cooltime;
        }

        private void Start()
        {
            image.sprite = targetSkill.skillSprite;
        }

        // Update is called once per frame
        void Update()
        {
            cooltimeText.text = targetSkill.CurrentCooltime.ToString("0.00");

            slider.value = targetSkill.CurrentCooltime;
            if (slider.value == 0)
            {
                UIUtility.HideObject(cooltimeText.gameObject);
            }
            else
            {
                UIUtility.ShowObject(cooltimeText.gameObject);
            }
        }
    }
}
