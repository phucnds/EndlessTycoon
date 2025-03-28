using EndlessTycoon.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace EndlessTycoon.TaskSystems
{
    public class MyButton : Button
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (!interactable) return;
            LeanTween.scale(this.gameObject, Vector3.one * .9f, .1f).setEase(LeanTweenType.easeOutQuad);

            SoundManager.Instance.PlaySFXPressUI();
        }


        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (!interactable) return;
            LeanTween.scale(this.gameObject, Vector3.one, .1f).setEase(LeanTweenType.easeInQuad);
        }
    }

}
