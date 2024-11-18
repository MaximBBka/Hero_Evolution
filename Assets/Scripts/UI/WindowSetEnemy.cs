using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

namespace Game
{
    public class WindowSetEnemy : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textNameEnemy;
        [SerializeField] private TextMeshProUGUI _textStrongEnemy;
        [SerializeField] private TextMeshProUGUI _textStrongPlayer;

        private void Start()
        {
            SetInfo();
        }
        private void SetInfo()
        {
            _textNameEnemy.SetText($"{YandexGame.savesData.EnemyName}");
            _textStrongEnemy.SetText($"{YandexGame.savesData.EnemyStrong}");
            _textStrongPlayer.SetText($"{YandexGame.savesData.Strong}");
        }
    }
}
