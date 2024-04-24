using UI.Base;
using UnityEngine.UIElements;
using UnityEngine.Android;
using CameraAn;

namespace UI.Sealed
{
    public class MainMenu : ScreenBase
    {
        private VisualElement _leftHeaderContainerButtonBack;
        private VisualElement _leftHeaderContainerButtonExit;
        private VisualElement _openCameraButton;
        private VisualElement _takePhotoButton;
        private VisualElement _cameraContainer;
        
        
        private CameraController _cameraController;

        public override void OnInitialized()
        {
            base.OnInitialized();
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }

            AssignButtons();
            InitHeaderButtons();
            StartApplication();
        }

        private void AssignButtons()
        {
            _screenElement = _rootElement.Q<VisualElement>(Constants.MAIN_MENU);
            _leftHeaderContainerButtonBack = _screenElement.Q<VisualElement>(Constants.BACK);
            _leftHeaderContainerButtonExit = _screenElement.Q<VisualElement>(Constants.EXIT);
            _openCameraButton = _screenElement.Q<VisualElement>(Constants.OPEN_CAMERA);
            _takePhotoButton = _screenElement.Q<VisualElement>(Constants.TAKE_PHOTO);

            _cameraContainer = _screenElement.Q<VisualElement>(Constants.CAMERA_CONTAINER);
        }

        private void InitHeaderButtons()
        {
            _leftHeaderContainerButtonBack.RegisterCallback<ClickEvent>(e => OnBackButtonClicked());
            _leftHeaderContainerButtonExit.RegisterCallback<ClickEvent>(e => OnExitButtonClicked());
            _openCameraButton.RegisterCallback<ClickEvent>(e => OnOpenButtonClicked());
            _takePhotoButton.RegisterCallback<ClickEvent>(e => OnTakePhotoButtonClicked());
        }

        private void OnBackButtonClicked()
        {
            _screenController.HideCurrentScreen();
            _screenController.ShowScreen(ScreenName.Photo);
        }

        private void OnExitButtonClicked()
        {
            OnDestroy();
        }

        private void OnOpenButtonClicked()
        {
            _cameraController.OpenCamera();
        }

        private void OnTakePhotoButtonClicked()
        {
            _cameraController.TakePhoto();
            _screenController.ShowScreen(ScreenName.Save);
            OnDestroy();
            _cameraContainer.style.display = DisplayStyle.None;//************************
        }

        private void StartApplication()
        {
            _screenController.ShowScreen(ScreenName.Main);
            _cameraController = new CameraController(_cameraContainer);
            _cameraContainer.style.display = DisplayStyle.Flex;//************************
        }

        private void OnDestroy()
        {
            _cameraController.Dispose();
        }
    }
}