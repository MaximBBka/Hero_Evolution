using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SetupAds : MonoBehaviour
    {
        [field: SerializeField] public WindowAds WindowAds { get; private set; }
        [field: SerializeField] public UIRecources UIRecources { get; private set; }
        [field: SerializeField] public SpawnHero SpawnHero { get; private set; }
        [field: SerializeField] public WindowIntroAds WindowIntro { get; private set; }


        [Inject]
        public void Constructor(WindowAds windowAds, UIRecources recources, SpawnHero spawn)
        {
            WindowAds = windowAds;
            UIRecources = recources;
            SpawnHero = spawn;
        }

        public static SetupAds Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
        }
    }
}
