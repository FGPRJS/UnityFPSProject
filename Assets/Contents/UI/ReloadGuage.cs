using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGuage : MonoBehaviour
{
    private Slider slider;
    public Player player;
    private AMecha targetMecha;
    private ASkill targetSkill;

    private void Awake()
    {
        targetMecha = player.target;
        targetSkill = targetMecha.skillReload;
        
        slider = GetComponent<Slider>();
        slider.maxValue = targetSkill.Casttime;
        slider.minValue = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
