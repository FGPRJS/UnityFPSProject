using Contents.Controller.Player;
using Contents.Mechanic;
using TMPro;
using UnityEngine;

namespace Contents.UI
{
    public class Ammo : MonoBehaviour
    {
        public Player player;
        private AMecha targetMecha;
        
        public TextMeshProUGUI LeftAmmo;
        public TextMeshProUGUI MaxAmmo;
        public TextMeshProUGUI TotalAmmo;


        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            targetMecha = player.target;
        }

        // Update is called once per frame
        void Update()
        {
            if (!targetMecha) return;
        
            LeftAmmo.text = targetMecha.Ammo.ToString();
            MaxAmmo.text = targetMecha.MaxAmmo.ToString();
            TotalAmmo.text = targetMecha.TotalAmmo.ToString();
        }
    }
}
