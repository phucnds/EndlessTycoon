using EndlessTycoon.TaskSystems;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessTycoon.Core
{
    public class CounterPanel : MonoBehaviour
    {
        [SerializeField] private Counter counter;
        [SerializeField] private GameObject unlockPanel;
        [SerializeField] private Button btnUnlock;
        [SerializeField] private UpgradeCounterPanel upgradeCounterPanel;
        [SerializeField] private GameObject notice;
        [SerializeField] private Button btnUpgrade;
        [SerializeField] private Button btnHide;

        private bool isLock;

        private void Start()
        {
            SetupButton();
            CurrencyManager.onUpdated += UpdateVisual;
        }

        private void OnDestroy()
        {
            CurrencyManager.onUpdated -= UpdateVisual;
        }

        private void SetupButton()
        {
            btnUpgrade.onClick.AddListener(() =>
            {
                if (!isLock)
                {
                    unlockPanel.SetActive(true);
                    isLock = true;
                    return;
                }

                bool flag = !upgradeCounterPanel.gameObject.activeSelf;
                upgradeCounterPanel.gameObject.SetActive(flag);
            });

            btnUnlock.onClick.AddListener(() =>
            {
                CurrencyManager.Instance.UseCurrency(5);
                unlockPanel.SetActive(false);
                counter.AddSlot();
            });

            btnHide.onClick.AddListener(() =>
            {
                upgradeCounterPanel.gameObject.SetActive(false);
            });
        }

        private void UpdateVisual()
        {
            bool flag = CurrencyManager.Instance.HasEnoughCurrency(counter.GetCost());
            notice.SetActive(flag);
            upgradeCounterPanel.UpdateVisual(counter);
        }
    }
}


