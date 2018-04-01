namespace Assets.Scripts
{
    public class StatisticData
    {
        private static StatisticData _instance;
        public static StatisticData instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StatisticData();
                }
                return _instance;
            }
        }

        public int FinishLevels;
        public bool EndButtonIsActive;
        public bool Rebooted;
        public bool NeedReload;
        public bool IsInBalka;
        public bool OpenEyes;
        public bool ThroughRoof;
        public bool ThroughFloor;
        public bool InHurry;
        public bool Headbutt;
    }
}
