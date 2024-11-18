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
        [SerializeField] private TextMeshProUGUI _textTitleTutorial;
        [SerializeField] private TextMeshProUGUI _textTitleTutorial1;
        [SerializeField] private TextMeshProUGUI _textTitleTutorial2;
        [SerializeField] private TextMeshProUGUI _textTitleTutorial3;
        [SerializeField] private TextMeshProUGUI _textTutorialDescription1;
        [SerializeField] private TextMeshProUGUI _textTutorialDescription2;
        [SerializeField] private TextMeshProUGUI _textTutorialDescription3;


        private void Start()
        {
            if (YandexGame.EnvironmentData.language == "ru")
            {
                _textButtonStrong.SetText("УВЕЛИЧИТЬ СИЛУ");
                _textAdsRes.SetText("Случайное количество ресурсов");
                _textAdsMoney.SetText("Получить");
                _textTitleBook.SetText("СПИСОК");
                _textTitleLeaderBoard.SetText("Таблица лидеров");
                _textTitleBattle.SetText("нападение");
                _textTitleBattle2.SetText("На вас напал:");
                _textButtonLeave.SetText("Убежать");
                _textButtonFight.SetText("В бой");
                _textButtonUpHero.SetText("Продолжить");
                _textTitleTutorial.SetText("Обучение");
                _textTitleTutorial1.SetText(" 1. Объединяй героев что бы получить нового!");
                _textTitleTutorial2.SetText(" 2. Кликай на героя что бы заработать монет!");
                _textTitleTutorial3.SetText(" 3. Описание");
                _textTutorialDescription1.SetText("Книга со всеми героями");
                _textTutorialDescription2.SetText("Ваша сила");
                _textTutorialDescription3.SetText("Ресурсы для улучшения силы");
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
                _textTitleTutorial.SetText("Training");
                _textTitleTutorial1.SetText(" 1. Combine heroes to get a new one!");
                _textTitleTutorial2.SetText(" 2. Click on the hero to earn coins!");
                _textTitleTutorial3.SetText(" 3. Description");
                _textTutorialDescription1.SetText("A book with all the characters");
                _textTutorialDescription2.SetText("Your strength");
                _textTutorialDescription3.SetText("Resources for improving strength");
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
                _textTitleTutorial.SetText("Eğitim");
                _textTitleTutorial1.SetText(" 1. Yenisini almak için kahramanları birleştir!");
                _textTitleTutorial2.SetText(" 2. Para kazanmak için kahramana tıkla!");
                _textTitleTutorial3.SetText(" 3. Açıklama");
                _textTutorialDescription1.SetText("Tüm kahramanlarla kitap");
                _textTutorialDescription2.SetText("Gücünüz");
                _textTutorialDescription3.SetText("Gücü artıracak kaynaklar");
            }
        }
    }
}
