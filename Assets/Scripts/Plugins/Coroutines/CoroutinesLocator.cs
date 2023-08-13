namespace Game.Plugins.Coroutines
{
    public static class CoroutinesLocator
    {
        private static ICoroutinesProvider NullService { get; } = new NullCoroutinesProvider();
        public static ICoroutinesProvider Service { get; private set; } = NullService;


        public static void Provide(ICoroutinesProvider value)
        {
            Service = value != null ? value : NullService;
        }
    }
}