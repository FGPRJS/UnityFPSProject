using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGuage : MonoBehaviour
{
    public MechaBase Target;
    private Slider slider;

    private void Awake()
    {
        slider = transform.GetComponent<Slider>();

        slider.minValue = 0;
        slider.maxValue = Target.ReloadCoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target.ammoStatus == MechaBase.AmmoStatus.Normal)
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
        else if(Target.ammoStatus == MechaBase.AmmoStatus.Reloading)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        slider.value = Target.CurrentReloadCoolTime;
    }
}
