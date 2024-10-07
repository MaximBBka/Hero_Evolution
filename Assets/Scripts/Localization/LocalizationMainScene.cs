using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

namespace Game
{
    public class LocalizationMainScene : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textButtonStrong;
        [SerializeField] private TextMeshProUGUI _textAdsRes;
        [SerializeField] private TextMeshProUGUI _textAdsMoney;
        [SerializeField] private TextMeshProUGUI _textTitleBook;
        [SerializeField] private TextMeshProUGUI _textTitleLeaderBoard;
        [SerializeField] private TextMeshProUGUI _textTitleBattle;
        [SerializeField] private TextMeshProUGUI _textTitleBattle2;
        [SerializeField] private TextMeshProUGUI _textButtonLeave;
        [SerializeField] private TextMeshProUGUI _textButtonFight;
        [SerializeField] private TextMeshProUGUI _textButtonUpHero;


        private void Start()
        {
            if (YandexGame.EnvironmentData.language == "ru")
            {
                _textButtonStrong.SetText("УВЕЛИЧИТЬ СИЛУ");
                _textAdsRes.SetText("Случайное количество ресурсов");
                _textAdsMoney.SetText("Получить");
                _textTitleBook.SetText("СПИСОК");
                _textTitleLeaderBoard.SetText("Таблица лидеров");
                _textTitleBattle.SetText("НАПАДЕНИЕ");
                _textTitleBattle2.SetText("На вас напал:");
                _textButtonLeave.SetText("Убежать");
                _textButtonFight.SetText("В бой");
                _textButtonUpHero.SetText("Продолжить");
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                _textButtonStrong.SetText("INCREASE THE POWER");
                _textAdsRes.SetText("Random number of resources");
                _textAdsMoney.SetText("Receive");
                _textTitleBook.SetText("LIST");
                _textTitleLeaderBoard.SetText("Leaderboard");
                _textTitleBattle.SetText("ATTACK");
                _textTitleBattle2.SetText("You were attacked:");
                _textButtonLeave.SetText("Escape");
                _textButtonFight.SetText("Into battle");
                _textButtonUpHero.SetText("Continue");
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                _textButtonStrong.SetText("GÜCÜ ARTIRMAK");
                _textAdsRes.SetText("Rastgele kaynak sayısı");
                _textAdsMoney.SetText("Almak");
                _textTitleBook.SetText("LİSTE");
                _textTitleLeaderBoard.SetText("ТLiderlik Tablosu");
                _textTitleBattle.SetText("SALDIRI");
                _textTitleBattle2.SetText("Sana saldırdım:");
                _textButtonLeave.SetText("Kaçmak");
                _textButtonFight.SetText("Savaşa");
                _textButtonUpHero.SetText("Devam et");
            }
        }
    }
}
