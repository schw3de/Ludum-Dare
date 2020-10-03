namespace schw3de.ld47
{
    public class Controls : Singleton<Controls>
    {
        public ControlsAsset Asset { get; set; }

        private void Awake()
        {
            if (Asset == null)
            {
                Asset = new ControlsAsset();
            }

            Asset.Enable();
        }
    }
}
