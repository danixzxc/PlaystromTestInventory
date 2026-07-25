using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public class CoinIndicatorView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;

        public void Initialize(int initialAmount)
        {
            _amountText.text = initialAmount.ToString();
        }
        public void UpdateAmount(int amount)
        {
            _amountText.text = amount.ToString();
        }
    }
}