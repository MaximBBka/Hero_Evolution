using TMPro;
using UnityEngine;
using YG;

namespace Game
{
    public class LocalizationBattleScene : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textButtonPlay;
        [SerializeField] private TextMeshProUGUI _textButtonBack;
        [SerializeField] private TextMeshProUGUI _textReward;
        [SerializeField] private TextMeshProUGUI _textStartBattle;
        [SerializeField] private TextMeshProUGUI _textNamePlayer;


        private void Start()
        {
            if (YandexGame.EnvironmentData.language == "ru")
            {
                _textButtonPlay.SetText("Играть");
                _textButtonBack.SetText("Получить");
                _textReward.SetText("награда");
                _textStartBattle.SetText("Нажмите что бы начать бой");
                _textNamePlayer.SetText("ВЫ");
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                _textButtonPlay.SetText("Play");
                _textButtonBack.SetText("Pick up");
                _textReward.SetText("REWARD");
                _textStartBattle.SetText("Click to start the fight");
                _textNamePlayer.SetText("YOU");
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                _textButtonPlay.SetText("Oynamak");
                _textButtonBack.SetText("Almak");
                _textReward.SetText("ÖDÜL");
                _textStartBattle.SetText("Dövüşü başlatmak için tıklayın");
                _textNamePlayer.SetText("SİZ");
            }
        }
    }
}
