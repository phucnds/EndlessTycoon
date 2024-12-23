using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessTycoon.Core
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private Button btnUpgrade;
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button btnExit;

        [SerializeField] private GameObject[] upgradeContain;
        [SerializeField] private Button[] buttons;
        [SerializeField] private int[] costs;
        [SerializeField] private GameObject notice;

        private int indexMinValue = 0;

        private void Start()
        {
            btnUpgrade.onClick.AddListener(ShowPopup);
            btnExit.onClick.AddListener(HidePopup);
            CurrencyManager.onUpdated += UpdateVisual;

            UpdateVisual();
            SetupButton();

        }


        private void OnDestroy()
        {
            CurrencyManager.onUpdated -= UpdateVisual;
        }

        private void ShowPopup()
        {
            upgradePanel.SetActive(true);
            upgradePanel.transform.localScale = Vector3.zero;
            canvasGroup.alpha = 1;
            LeanTween.scale(upgradePanel, Vector3.one, .3f).setEase(LeanTweenType.easeOutQuad);
        }

        private void HidePopup()
        {
            StartCoroutine(FadeRoutine(0));
        }


        private IEnumerator FadeRoutine(float targetAlpha)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime / .3f);
                yield return null;
            }

            upgradePanel.SetActive(false);
        }

        private void UpdateVisual()
        {
            for (int i = 0; i < costs.Length; i++)
            {
                buttons[i].interactable = CurrencyManager.Instance.HasEnoughCurrency(costs[i]);
            }

            if (indexMinValue < 4)
            {
                notice.SetActive(CurrencyManager.Instance.HasEnoughCurrency(costs[indexMinValue]));
            }
            else
            {
                notice.SetActive(false);
            }

        }

        private void SetupButton()
        {
            buttons[0].onClick.AddListener(() =>
            {
                indexMinValue = 1;
                CurrencyManager.Instance.UseCurrency(costs[0]);
                upgradeContain[0].gameObject.SetActive(false);
                LevelManager.Instance.IncreaseCustomer();
            });

            buttons[1].onClick.AddListener(() =>
            {
                indexMinValue = 2;
                CurrencyManager.Instance.UseCurrency(costs[1]);
                upgradeContain[1].gameObject.SetActive(false);
                LevelManager.Instance.Stall.AddStaff();
            });

            buttons[2].onClick.AddListener(() =>
            {
                indexMinValue = 3;
                CurrencyManager.Instance.UseCurrency(costs[2]);
                upgradeContain[2].gameObject.SetActive(false);
                LevelManager.Instance.IncreaseCustomer();
            });

            buttons[3].onClick.AddListener(() =>
            {
                indexMinValue = 4;
                CurrencyManager.Instance.UseCurrency(costs[3]);
                upgradeContain[3].gameObject.SetActive(false);
                LevelManager.Instance.Stall.GetCounter().UpgradeSpeed();
            });


        }



    }

}

