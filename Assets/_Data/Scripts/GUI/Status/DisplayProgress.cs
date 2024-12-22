using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayProgress : MonoBehaviour
{
    [SerializeField] private GameObject progression;
    [SerializeField] private Image pgImagel;

    private float value;
    private float maxValue = 1;
    private bool isActive;
    private Action onComplete;

    private void Update()
    {
        if (!isActive) return;

        value += Time.deltaTime;
        pgImagel.fillAmount = value / Mathf.Max(maxValue, 0.01f);
        if (value >= maxValue)
        {
            onComplete?.Invoke();
            isActive = false;
            progression.SetActive(false);
        }
    }

    public void StartProgression(float mValue, Action onComplete)
    {
        maxValue = mValue;
        this.onComplete = onComplete;
        progression.SetActive(true);
        value = 0;

        isActive = true;
    }
}
