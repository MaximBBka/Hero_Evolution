using UnityEngine;
using YG;
using Zenject;

namespace Game
{
    public class AdsProvider : MonoBehaviour
    {
        private int _rewardIndex = -1; // 0 - ����������� ���� � ���������. 1 - ��������� ���������� ��������. 2 - �������� ��� ������ + 2 �����
        private WindowAds _windowAds;
        private UIRecources _uIRecources;
        private SpawnHero _spawnHero;

        [Inject]
        public void Constructor(WindowAds windowAds, UIRecources recources, SpawnHero spawn)
        {
            _windowAds = windowAds;
            _uIRecources = recources;
            _spawnHero = spawn;
        }

        public void RewardAds(int index) // ������ ������ ��������
        {
            if (_rewardIndex == -1)
            {
                _rewardIndex = index;
                YandexGame.RewVideoShow(0);
            }
        }
        public void ShowAds(int index) // ������������� �� ������ � �������� � ��������� ������
        {
            if (_rewardIndex != -1)
            {
                _rewardIndex = -1;
            }
            RewardAds(index);
        }
        public void AddMoney()
        {
            if (_rewardIndex == 0)
            {
                _windowAds.AddMoneyAds();
                _rewardIndex = -1;
            }
        }

        public void AddRandomRes()
        {
            if (_rewardIndex == 1)
            {
                _uIRecources.AddRandomRes();
                _rewardIndex = -1;
            }
        }

        public void SpawnNextHero()
        {
            if (_rewardIndex == 2)
            {
                _spawnHero.AdsSpawn();
                _rewardIndex = -1;
            }
        }
    }
}
