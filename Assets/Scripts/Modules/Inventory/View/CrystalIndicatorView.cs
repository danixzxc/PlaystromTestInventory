using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public class CrystalIndicatorView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;

        public void Initialize(int amount)
        {
            _amountText.text = amount.ToString();
        }
        public void UpdateAmount(int amount)
        {
            _amountText.text = amount.ToString();
        }
    }
}