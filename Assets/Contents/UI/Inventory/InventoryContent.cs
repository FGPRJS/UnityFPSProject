using UnityEngine;

namespace Contents.UI.Inventory
{
    public class InventoryContent : MonoBehaviour
    {
        public string openItemDetailAnimationName, closeItemDetailAnimationName;
        public Animator itemDetailAnimator;
        public GameObject itemGrid;

        public void OpenItemDetail()
        {
            itemDetailAnimator.Play(openItemDetailAnimationName);
        }

        public void CloseItemDetail()
        {
            itemDetailAnimator.Play(closeItemDetailAnimationName);
        }
    }
}
