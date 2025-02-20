
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        public List<int> BaseHeroes = new List<int>();
        public List<int> ElementBookOpen = new List<int>();
        public int[] TotalRes = new int[3];
        public int Strong;
        public int Money = 40;
        public int MaxStrong;
        public int TotalBattle;
        public int BattleWin = 2;
        public int IndexOpenHero;
        public int LevelStrong;
        public int TotalSpawnUnits;
        public Sprite CurrentSpawn;
        public Sprite AdsSpawn;
        public bool isFirstOpen = true;
        public string EnemyName;
        public int EnemyStrong;
        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
