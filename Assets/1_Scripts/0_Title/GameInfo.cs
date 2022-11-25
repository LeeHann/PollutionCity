public static class GameInfo
{
    public static Difficulty Difficulty {
        get {
            return difficulty;
        }
        set {
            difficulty = value;
            if (difficulty == Difficulty.Easy)
                SetBalance(40, 30, 30, 5000, 2500, 0.033f, 3, 0.3f);
            
            else if (difficulty == Difficulty.Normal)
                SetBalance(60, 45, 35, 7500, 4500, 0.019f, 4, 0.25f);

            else if (difficulty == Difficulty.Hard)
                SetBalance(80, 60, 40, 10000, 7000, 0.009f, 5, 0.2f);
        }
    }
    private static Difficulty difficulty = Difficulty.Easy;
    public static int cellCountX;
    public static int cellCountZ;
    public static int maxTurn = 30;
    public static int maxPA = 5000;
    public static int startPA = 2500;
    public static float PLPercent = 0.033f;
    public static int cityCount = 3;
    public static float winPL = 0.3f;
    public const float losePL = 100.0f;
    public const int startMoney = 1000;
    
    public static void SetBalance(int x, int z, int turn, int pa, int startpa, float plRisePercent, int numCity, float winningpl)
    {
        cellCountX = x;
        cellCountZ = z;
        maxTurn = turn;
        maxPA = pa;
        startPA = startpa;
        PLPercent = plRisePercent;
        cityCount = numCity;
        winPL = winningpl;
    }
}
