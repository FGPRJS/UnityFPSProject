using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillIcon : MonoBehaviour
{
    public ASkill target;
    public Slider slider;
    public TextMeshProUGUI cooltimeText;

    private void Awake()
    {
        slider.minValue = 0;
        slider.maxValue = target.Cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        cooltimeText.text = target.CurrentCooltime.ToString("0.00");

        slider.value = target.CurrentCooltime;
        if (slider.value == 0)
        {
            Utility.HideObject(cooltimeText.gameObject);
        }
        else
        {
            Utility.ShowObject(cooltimeText.gameObject);
        }
    }
}
