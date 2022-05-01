using System;
using System.Text;
using Contents.Controller.Player;
using Contents.Mechanic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Contents.UI
{
    public class HealthBar : MonoBehaviour
    {
        public Player player;
        private AMecha playerMecha;

        public TextMeshProUGUI hpText;

        public Slider fill, fillRed;

        float lerp = 0f;
        public float duration = 10f;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (!player.target) return;

            playerMecha = player.target;
            
            fill.maxValue = playerMecha.MaxHP;
            fillRed.maxValue = playerMecha.MaxHP;

            fill.value = playerMecha.HP;
            fillRed.value = playerMecha.HP;

            hpText.text = BuildHPText(playerMecha.HP, playerMecha.MaxHP);
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
            if (!playerMecha) return;
            //HP Image
            fill.value = playerMecha.HP;

            if(Math.Abs(fill.value - fillRed.value) > 0.0f)
            {
                lerp += Time.deltaTime / duration;
                fillRed.value = (int)Mathf.Lerp(fillRed.value, fill.value, lerp);
            }
            //HP Value Text
            hpText.text = BuildHPText((int)fillRed.value, playerMecha.MaxHP);
        }
    }
}
