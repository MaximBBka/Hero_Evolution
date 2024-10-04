using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class UIRecources : MonoBehaviour
    {
        [field: SerializeField] public int[] CountRes { get; private set; } = new int[3]; // 0 - ������, 1- ������, 2- ������

        [SerializeField] private TextMeshProUGUI[] _textRes;
        [SerializeField] private Image _button;
        [SerializeField] private SOResources _sOResources;
        [SerializeField] private BaseResource[] _prefabRes;
        [SerializeField] private Transform _spawnPos;

        private ClassPool _pool;
        private MainUI _mainUI;
        private int _currentLevel = 0;
        private bool IsUpgrade;

        [Inject]
        public void Construct(MainUI ui, ClassPool pool)
        {
            _mainUI = ui;
            _pool = pool;
        }

        private void Start()
        {           
            UpgradeStrong();
        }
        public void UpgradeStrong()
        {
            for (int i = 0; i < CountRes.Length; i++)
            {
                if (_sOResources.ModelResources[i].sOResource._modelRecource[_currentLevel].Price >= CountRes[i])
                {
                    IsUpgrade = false;
                    _button.color = Color.red;
                    return;
                }
            }
            CanBuy();
        }
        public void CanBuy()
        {
            _button.color = Color.white;
            IsUpgrade = true;
        }
        public void AddRes(int total, Transform pos)
        {
            int randomRes = Random.Range(0, 3);
            CountRes[randomRes] += total;
            _textRes[randomRes].SetText($"{CountRes[randomRes]}");
            for (int i = 0;i < total; i++)
            {
                MonoPool newMono = _pool.Get(_prefabRes[randomRes], _spawnPos);
                BaseResource res = newMono.Get() as BaseResource;
                res.transform.position = pos.position;
                DOTween.Sequence()
                    .Append(res.transform.DOMove(new Vector3(res.transform.position.x + Random.Range(-3, 3), res.transform.position.y + Random.Range(-3, 3), 0), 1f))
                    .Append(res.SpriteRenderer.DOFade(0, 1f))
                    .AppendCallback(() => { newMono.PutAway(res); });
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
                _mainUI.UpdateStrong(_sOResources.ModelResources[0].sOResource._modelRecource[_currentLevel].BonusStrong);
                _currentLevel++;
                UpgradeStrong();
            }
        }
    }
}
