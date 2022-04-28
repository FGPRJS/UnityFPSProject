using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    private AMecha targetMecha;

    public TextMeshProUGUI hpText;

    public Slider fill, fillRed;

    float lerp = 0f;
    public float duration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (!targetMecha) return;
        
        targetMecha = player.target;
        
        fill.maxValue = targetMecha.MaxHP;
        fillRed.maxValue = targetMecha.MaxHP;

        fill.value = targetMecha.HP;
        fillRed.value = targetMecha.HP;

        hpText.text = BuildHPText(targetMecha.HP, targetMecha.MaxHP);
    }

    string BuildHPText(long HP, long MaxHP)
    {
        var builder = new StringBuilder();

        builder.Append(HP);
        builder.Append(" / ");
        builder.Append(MaxHP);

        return builder.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetMecha) return;
        //HP Image
        fill.value = targetMecha.HP;

        if(Math.Abs(fill.value - fillRed.value) > 0.0f)
        {
            lerp += Time.deltaTime / duration;
            fillRed.value = (int)Mathf.Lerp(fillRed.value, fill.value, lerp);
        }
         //HP Value Text
        hpText.text = BuildHPText((int)fillRed.value, targetMecha.MaxHP);
    }
}
