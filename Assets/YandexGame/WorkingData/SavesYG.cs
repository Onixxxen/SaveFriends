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

        public int SavedTotalHeart = 3;
        public int SavedTotalMoney = 500; // 500
        public int SavedTotalScore = 0;
        public int SavedScoreBonus = 1;
        public int SavedMoneyBonus = 1;
        public int SavedGoal = 15000;
        public int SavedCurrentBackground = 0;
        public int SavedNextBackground = 1;
        public bool IsLanguageLoaded = false;
        public string SavedLanguage = "ru";

        public int[] SavedCostImprove = new int[3];
        public int[] SavedBuyedImprove = new int[3];
        public int[] SavedCostBackgrounds = new int[11];
    }
}
