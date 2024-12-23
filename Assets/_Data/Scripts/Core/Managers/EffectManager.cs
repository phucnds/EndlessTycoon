using UnityEngine;

namespace EndlessTycoon.Core
{
    public class EffectManager : Singleton<EffectManager>
    {
        [SerializeField] private TextFading textFading;

        public void SetText(Transform tr, int money)
        {
            TextFading fading = Instantiate(textFading, transform);
            fading.transform.position = (Vector2)tr.position + Vector2.up;
            fading.SetText("+ " + money);
        }
    }
}

