using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public ASkill Target;
    public Slider slider;

    private void Awake()
    {
        slider.minValue = 0;
        slider.maxValue = Target.Cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Target.CurrentCooltime;
    }
}
