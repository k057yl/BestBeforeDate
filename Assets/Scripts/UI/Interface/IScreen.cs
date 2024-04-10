using System;

public interface IScreen : IDisposable
{
    void Show();
    void ShowSilent();
    void Hide();
}
