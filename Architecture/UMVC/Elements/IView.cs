namespace Common.Basic.UMVC.Elements
{
    public interface IView
    {
        void Show();
        void Hide();

        bool IsVisible { get; }

        IView AsParent();
        T GetParent<T>() where T : IView;
    }
}
