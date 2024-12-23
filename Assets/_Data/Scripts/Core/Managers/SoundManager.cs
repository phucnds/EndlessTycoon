using UnityEngine;

namespace EndlessTycoon.Core
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip pressUI;
        [SerializeField] private AudioClip collectCoin;

        [NaughtyAttributes.Button]
        public void PlaySFXPressUI()
        {
            audioSource.PlayOneShot(pressUI);
        }

        [NaughtyAttributes.Button]
        public void PlaySFXCollectCoin()
        {
            audioSource.PlayOneShot(collectCoin);
        }
    }

}

