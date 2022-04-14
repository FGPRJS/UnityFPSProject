using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public MechaBase Target;

    public Slider Fill, FillRed;

    float lerp = 0f, duration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Fill.maxValue = Target.data.MaxHP;
        FillRed.maxValue = Target.data.MaxHP;

        Fill.value = Target.data.HP;
        FillRed.value = Target.data.HP;
    }

    // Update is called once per frame
    void Update()
    {
        Fill.value = Target.data.HP;

        if(Fill.value != FillRed.value)
        {
            lerp += Time.deltaTime / duration;
            FillRed.value = (int)Mathf.Lerp(FillRed.value, Fill.value, lerp);
        }
    }
}
