using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public class CrystalIndicatorView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void UpdateAmount(int amount)
        {
            _amountText.text = amount.ToString();
        }
    }
}