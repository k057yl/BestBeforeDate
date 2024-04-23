using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Sealed
{
    public class PhotoMenu : ScreenBase
    {
        private VisualElement _leftHeaderContainerButtonBack;
        private VisualElement _leftHeaderContainerButtonExit;
        
        public override void OnInitialized()
        {
            _screenElement = _rootElement.Q(Constants.PHOTO_MENU);
            base.OnInitialized();
            AssignButtons();
            InitHeaderButtons();
        }
        
        private void AssignButtons()
        {
            _leftHeaderContainerButtonBack = _screenElement.Q(Constants.BACK);
            _leftHeaderContainerButtonExit = _screenElement.Q(Constants.EXIT);
        }
        
        private void InitHeaderButtons()
        {
            _leftHeaderContainerButtonBack.RegisterCallback<ClickEvent>(e =>
            {
                _screenController.HideCurrentScreen();//**********
                _screenController.ShowScreen(ScreenName.Main);
                Debug.Log("Main");
            });
            
            _leftHeaderContainerButtonExit.RegisterCallback<ClickEvent>(e =>
            {
                //_screenController.ShowScreen(ScreenName.Main);
                Debug.Log("Photo");
            });
        }
    }
}