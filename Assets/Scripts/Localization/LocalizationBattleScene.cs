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


        private void Start()
        {
            if (YandexGame.EnvironmentData.language == "ru")
            {
                _textButtonPlay.SetText("Играть");
                _textButtonBack.SetText("Забрать");
                _textReward.SetText("НАГРАДА");
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                _textButtonPlay.SetText("Play");
                _textButtonBack.SetText("Pick up");
                _textReward.SetText("REWARD");
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                _textButtonPlay.SetText("Oynamak");
                _textButtonBack.SetText("Almak");
                _textReward.SetText("ÖDÜL");
            }
        }
    }
}
