using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGuage : MonoBehaviour
{
    private Slider slider;
    public ASkill Target;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = Target.Casttime;
        slider.minValue = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Target.CurrentCasttime;

        if(slider.value == 0)
        {
            Utility.HideObject(gameObject);
        }
        else
        {
            Utility.ShowObject(gameObject);
        }
    }
}
