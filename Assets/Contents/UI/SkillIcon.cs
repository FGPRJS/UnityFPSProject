using System.Collections;
using System.Collections.Generic;
using Contents.Controller.Player;
using Contents.Mechanic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillIcon : MonoBehaviour
{
    public Player player;
    private AMecha targetMecha;
    private ASkill targetSkill;
    public Slider slider;
    public TextMeshProUGUI cooltimeText;

    private void Awake()
    {
        targetMecha = player.target;
        targetSkill = targetMecha.skillAdvanced;
        
        slider.minValue = 0;
        slider.maxValue = targetSkill.Cooltime;
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
