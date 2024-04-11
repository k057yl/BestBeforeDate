using UI.Interfaces;
using Zenject;

namespace UI
{
    public sealed class ScreenController
    {
        private readonly DiContainer _container;

        private IScreen _currentScreen;
        private IScreen _lastScreen;

        [Inject]
        public ScreenController(DiContainer container)
        {
            _container = container;
        }
        
        public void ShowScreen(ScreenName screenName)
        {
            if (_currentScreen == _container.ResolveId<IScreen>(screenName.ToString())) return;
            
            _currentScreen = _container.ResolveId<IScreen>(screenName.ToString());
            _currentScreen.Show();
        }
        
        public void HideCurrentScreen()
        {
            if (_currentScreen == null) return;
            _lastScreen = _currentScreen;
            _currentScreen.Hide();
            _currentScreen = null;
        }
        
        public void RestoreLastScreen()
        {
            if (_lastScreen == null) return;
            _currentScreen = _lastScreen;
            _currentScreen.Show();
        }
    }   
}