using UI.Base;
using UnityEngine.UIElements;

namespace UI.Sealed
{
    public class MainMenu : ScreenBase
    {
        private VisualElement _leftHeaderContainerButtonBack;
        private VisualElement _leftHeaderContainerButtonExit;
        
        public override void OnInitialized()
        {
            _screenElement = _rootElement.Q(Constants.MAIN_MENU);
            base.OnInitialized();
            AssignButtons();
            InitHeaderButtons();
            StartApplication();
        }
        
        private void AssignButtons()
        {
            _leftHeaderContainerButtonBack = _screenElement.Q(Constants.EXIT);
            _leftHeaderContainerButtonExit = _screenElement.Q(Constants.BACK);
        }
        
        private void InitHeaderButtons()
        {
            _leftHeaderContainerButtonBack.RegisterCallback<ClickEvent>(e =>
            {
                //_mainMenuHandler.EnableMenu();
                _screenController.ShowScreen(ScreenName.Photo);
            });
            
            _leftHeaderContainerButtonExit.RegisterCallback<ClickEvent>(e =>
            {
                _screenController.ShowScreen(ScreenName.Main);
            });
        }

        private void StartApplication()
        {
            _screenController.ShowScreen(ScreenName.Main);
        }
    }
}

