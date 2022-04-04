using System;

namespace Common.Basic.UMVC.Elements
{
    public interface IButtonView : IView
    {
        Action OnUp { set; }

        bool Interactable { set; }
    }
}
