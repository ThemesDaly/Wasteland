public static class Services
{
    public static IBootstrap Boot => Bootstrap.Instance;

    public static IGameService Game => GameService.Instance;
    
    public static IConstructorService Constructor => ConstructorService.Instance;

    public static ISceneService Scene => SceneService.Instance;

    public static IUIController UI => UIService.Instance;
}