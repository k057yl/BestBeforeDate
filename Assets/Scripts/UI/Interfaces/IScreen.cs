using System;

namespace UI.Interfaces
{
    public interface IScreen : IDisposable
    {
        void Show();
        void Hide();
    }   
}