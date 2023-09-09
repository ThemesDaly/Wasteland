public static class ExtentionsView
{
    public static void Init(this IView[] views)
    {
        foreach (var view in views)
            view.Init();
    }
    
    public static void TurnOn(this IView[] views)
    {
        foreach (var view in views)
            view.TurnOn();
    }
    
    public static void TurnOff(this IView[] views)
    {
        foreach (var view in views)
            view.TurnOff();
    }
}