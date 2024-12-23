using EndlessTycoon.TaskSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessTycoon.Core
{
    public class UpgradeCounterPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtLevel;
        [SerializeField] private TextMeshProUGUI txtIncome;
        [SerializeField] private TextMeshProUGUI txtSpeed;
        [SerializeField] private TextMeshProUGUI txtCost;
        [SerializeField] private Button btnUpgrade;
        
        public void UpdateVisual(Counter counter)
        {
            txtLevel.text = $"Level {counter.GetLevel() + 1}";
            txtIncome.text = counter.GetIncome().ToString();
            txtCost.text = counter.GetCost().ToString();
            txtSpeed.text = counter.GetSpeed().ToString();

            Debug.Log(txtLevel.text);

            btnUpgrade.interactable = CurrencyManager.Instance.HasEnoughCurrency(counter.GetCost());

            int cost = counter.GetCost();

            btnUpgrade.onClick.RemoveAllListeners();
            btnUpgrade.onClick.AddListener(() =>
            {
                counter.LevelUp();
                CurrencyManager.Instance.UseCurrency(cost);
                
            });

            if (counter.GetLevel() >= 24)
            {
                txtCost.text = "MAX";
                btnUpgrade.interactable = false;
            }
        }
    }

}

