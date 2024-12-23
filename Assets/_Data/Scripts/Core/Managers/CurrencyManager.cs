using System;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [field: SerializeField] public int Currency { get; private set; }

        public static Action onUpdated;

        private void Start()
        {
            UpdateTexts();
        }

        [NaughtyAttributes.Button]
        private void Add10()
        {
            AddCurrency(10);
        }

        private void UpdateTexts()
        {
            CurrencyText[] currencyTexts = FindObjectsByType<CurrencyText>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (CurrencyText text in currencyTexts)
            {
                text.UpdateText(Currency.ToString());
            }
        }

        public void AddCurrency(int amount)
        {
            Currency += amount;
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            UpdateTexts();
            onUpdated?.Invoke();
        }

        public bool HasEnoughCurrency(int price) => Currency >= price;
        public void UseCurrency(int price) => AddCurrency(-price);
    }


}


