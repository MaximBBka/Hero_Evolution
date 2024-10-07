using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class WindowOpenHero : MonoBehaviour
    {
        [SerializeField] private Image _imageHero;       
        [SerializeField] private Transform _buttonNext;
        [SerializeField] private Transform _panelOpenHero;
        [SerializeField] private Transform _effectOpen;

        public int IndexOpen;
        private void Start()
        {
            Load();
        }
        public void Load()
        {
            YandexGame.LoadProgress();
            IndexOpen = YandexGame.savesData.IndexOpenHero;
        }
        public void Save()
        {
            YandexGame.savesData.IndexOpenHero = IndexOpen;
            YandexGame.SaveProgress();
        }
        public void StartOpen(Sprite image)
        {
            _imageHero.sprite = image;
            StartCoroutine(OpenHero());
        }

        public IEnumerator OpenHero()
        {
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.OpenHero);
            _panelOpenHero.gameObject.SetActive(true);
            _effectOpen.transform.DOScale(new Vector3(2, 2, 2), 3f);
            yield return new WaitForSeconds(3f);
            _buttonNext.gameObject.SetActive(true);
        }

        public void CloseWindow() 
        {
            _panelOpenHero.gameObject.SetActive(false);
            _effectOpen.transform.localScale = Vector3.zero;
            _buttonNext.gameObject.SetActive(false);
            Save();
        }
    }
}
