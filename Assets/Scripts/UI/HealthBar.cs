using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public MechaBase Target;

    public TextMeshProUGUI HPText;

    public Slider Fill, FillRed;

    float lerp = 0f;
    public float duration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Fill.maxValue = Target.MaxHP;
        FillRed.maxValue = Target.MaxHP;

        Fill.value = Target.HP;
        FillRed.value = Target.HP;

        HPText.text = BuildHPText(Target.HP, Target.MaxHP);

        //Rotating
        transform.RotateAround(transform.position, Vector3.up, 330);
    }

    string BuildHPText(long HP, long MaxHP)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(HP);
        builder.Append(" / ");
        builder.Append(MaxHP);

        return builder.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //HP Image
        Fill.value = Target.HP;

        if(Fill.value != FillRed.value)
        {
            lerp += Time.deltaTime / duration;
            FillRed.value = (int)Mathf.Lerp(FillRed.value, Fill.value, lerp);
        }
         //HP Value Text
        HPText.text = BuildHPText((int)FillRed.value, Target.MaxHP);
    }
}
