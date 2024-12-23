using TMPro;
using UnityEngine;
namespace EndlessTycoon.Core
{
    public class TextFading : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;

        public void SetText(string str)
        {
            txt.text = str;

            LeanTween.moveY(gameObject, transform.position.y + 1, .6f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}