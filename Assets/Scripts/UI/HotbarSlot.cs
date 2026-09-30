using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class HotbarSlot : MonoBehaviour
    {
        [SerializeField] private ItemStack itemStack;
        [SerializeField] private int slotIndex;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject icon;
        private SpriteRenderer bgSr;
        private SpriteRenderer iconSr;

        public ItemStack GetItemStack() => itemStack;
        public int GetSlotIndex() => slotIndex;
        
        void Start()
        {
            bgSr = background.GetComponent<SpriteRenderer>();
            iconSr = icon.GetComponent<SpriteRenderer>();
            UpdateIcon();
        }
        
        void Update()
        {
        
        }

        public void UpdateIcon()
        {
            if (itemStack is null)
            {
                iconSr.sprite = null;
                icon.SetActive(false);
            }
            else
            {
                iconSr.sprite = itemStack.GetIcon();
                icon.SetActive(true);
            }
        }
    }
}
