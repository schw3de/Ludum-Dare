namespace schw3de.ld47
{
    public class GameState : Singleton<GameState>
    {
        static GameState()
        {
            _dontDestroyOnLoad = true;
        }

        public LevelData CurrentLevel { get; set; }

        public int Score { get; set; }
    }
}
