namespace Common.Basic.UMVC.Elements
{
    public interface IView
    {
        string ID { get; set; }
        void Show();
        void Hide();

        bool IsVisible { get; }

        IView AsParent();
        T GetParent<T>() where T : IView;
        IView GetParent();

        void FitToChildren();
    }
}
