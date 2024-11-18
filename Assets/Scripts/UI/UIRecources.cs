using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Game
{
    public class UIRecources : MonoBehaviour
    {
        [field: SerializeField] public int[] CountRes { get; private set; } = new int[3]; // 0 - первый, 1- второй, 2- третий

        [SerializeField] private TextMeshProUGUI[] _textRes;
        [SerializeField] private Image _button;
        [SerializeField] private SOResources _sOResources;
        [SerializeField] private BaseResource[] _prefabRes;
        [SerializeField] private Transform _spawnPos;

        private ClassPool _pool;
        private MainUI _mainUI;
        private int _currentLevel = 0;
        private bool IsUpgrade;
        private Sequence sequence;

        [Inject]
        public void Construct(MainUI ui, ClassPool pool)
        {
            _mainUI = ui;
            _pool = pool;
        }

        private void Start()
        {
            Load();
            UpgradeStrong();
        }
        public void UpgradeStrong()
        {
            Save();
            for (int i = 0; i < CountRes.Length; i++)
            {
                if (_sOResources.ModelResources[i].sOResource._modelRecource[_currentLevel].Price > CountRes[i])
                {
                    _textRes[i].color = Color.red;
                }
                else
                {
                    _textRes[i].color = Color.green;
                }  
            }
            for (int i = 0; i < CountRes.Length; i++)
            {
                if (_sOResources.ModelResources[i].sOResource._modelRecource[_currentLevel].Price > CountRes[i])
                {
                    IsUpgrade = false;
                    _button.color = Color.red;
                    return;
                }
                else
                {
                    _textRes[i].color = Color.green;
                    _button.color = Color.white;
                    IsUpgrade = true;
                }
            }
        }

        public void AddRes(int total, Transform pos = null)
        {
            int randomRes = Random.Range(0, 3);
            CountRes[randomRes] += total;
            _textRes[randomRes].SetText($"{CountRes[randomRes]}");
            if (pos != null)
            {
                for (int i = 0; i < total; i++)
                {
                    MonoPool newMono = _pool.Get(_prefabRes[randomRes], _spawnPos);
                    BaseResource res = newMono.Get() as BaseResource;
                    res.transform.position = pos.position;

                    sequence = DOTween.Sequence()
                        .Append(res.transform.DOMove(new Vector3(res.transform.position.x + Random.Range(-3, 3), res.transform.position.y + Random.Range(-3, 3), 0), 1f))
                        .Append(res.SpriteRenderer?.DOFade(0, 1f))
                        .SetLink(res.gameObject)
                        .AppendCallback(() => { newMono.PutAway(res); });
                    UpgradeStrong();
                }
            }
            else
            {
                UpgradeStrong();
            }
        }

        public void UpStrong()
        {
            if (IsUpgrade)
            {
                for (int i = 0; i < CountRes.Length; i++)
                {
                    CountRes[i] -= _sOResources.ModelResources[i].sOResource._modelRecource[_currentLevel].Price;
                    _textRes[i].SetText($"{CountRes[i]}");
                }
                _mainUI.AddStrong(_sOResources.ModelResources[0].sOResource._modelRecource[_currentLevel].BonusStrong);
                _currentLevel++;
                UpgradeStrong();
            }
        }
        private void AdsAddRes(int total)
        {
            for (int i = 0; i < CountRes.Length; i++)
            {
                CountRes[i] += total;
                _textRes[i].SetText($"{CountRes[i]}");
                UpgradeStrong();
            }
        }
        public void AddRandomRes()
        {
            AdsAddRes(Random.Range(_sOResources.ModelResources[0].sOResource._modelRecource[_currentLevel].RandomResAds[0], _sOResources.ModelResources[0].sOResource._modelRecource[_currentLevel].RandomResAds.Length - 1 + 1));
        }

        private void OnDestroy()
        {
            sequence?.Kill();
        }
        public void Save()
        {
            YandexGame.savesData.TotalRes = CountRes;
            YandexGame.savesData.LevelStrong = _currentLevel;
            YandexGame.SaveProgress();
        }
        public void Load()
        {
            YandexGame.LoadProgress();
            CountRes = YandexGame.savesData.TotalRes;
            _currentLevel = YandexGame.savesData.LevelStrong;
            for (int i = 0; i < CountRes.Length; i++)
            {

                _textRes[i].SetText($"{CountRes[i]}");
            }
        }
    }
}
