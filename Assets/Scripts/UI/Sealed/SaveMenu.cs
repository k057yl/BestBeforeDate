using System.Collections.Generic;
using CameraAn;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Sealed
{
    public class SaveMenu : ScreenBase
    {
        private VisualElement _saveButton;
        private VisualElement _dontSaveButton;
        private List<Texture2D> _photoList;

        private CameraController _cameraController;
        private VisualElement _photoPreview;

        public override void OnInitialized()
        {
            base.OnInitialized();
            AssignButtons();
            InitHeaderButtons();

            _photoList = new List<Texture2D>();
        }

        private void AssignButtons()
        {
            _screenElement = _rootElement.Q<VisualElement>(Constants.SAVE_MENU);
            _saveButton = _screenElement.Q<VisualElement>(Constants.SAVE);
            _dontSaveButton = _screenElement.Q<VisualElement>(Constants.DONT_SAVE);
            _photoPreview = _screenElement.Q<Image>(Constants.PHOTO_PREVIEW);
        }
        
        private void InitHeaderButtons()
        {
            _saveButton.RegisterCallback<ClickEvent>(e => OnSaveButtonClicked());
            _dontSaveButton.RegisterCallback<ClickEvent>(e => OnDontSaveButtonClicked());
        }

        private void OnSaveButtonClicked()
        {
            _screenController.HideCurrentScreen();
            _screenController.ShowScreen(ScreenName.Main);
            //_cameraController.SavePhoto();
            
            _photoList.Add(_cameraController.GetLastPhoto());
            
            if (_photoList.Count > 0)
            {
                _photoPreview.style.backgroundImage = new StyleBackground(_photoList[_photoList.Count - 1]);
            }
        }
        
        private void OnDontSaveButtonClicked()
        {
            _screenController.HideCurrentScreen();
            _screenController.ShowScreen(ScreenName.Main);
            _cameraController.DiscardPhoto();
            
            if (_photoList.Count > 0)
            {
                _photoList.RemoveAt(_photoList.Count - 1);
            }
        }
    }
}
/*
using CameraAn;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Sealed
{
    public class SaveMenu : ScreenBase
    {
        private VisualElement _saveButton;
        private VisualElement _dontSaveButton;

        private CameraController _cameraController;

        public override void OnInitialized()
        {
            base.OnInitialized();
            AssignButtons();
            InitHeaderButtons();
        }

        private void AssignButtons()
        {
            _screenElement = _rootElement.Q<VisualElement>(Constants.SAVE_MENU);
            _saveButton = _screenElement.Q<VisualElement>(Constants.SAVE);
            _dontSaveButton = _screenElement.Q<VisualElement>(Constants.DONT_SAVE);
        }
        
        private void InitHeaderButtons()
        {
            _saveButton.RegisterCallback<ClickEvent>(e => OnSaveButtonClicked());
            _dontSaveButton.RegisterCallback<ClickEvent>(e => OnDontSaveButtonClicked());
        }

        private void OnSaveButtonClicked()
        {
            _screenController.HideCurrentScreen();
            _screenController.ShowScreen(ScreenName.Main);
        }
        
        private void OnDontSaveButtonClicked()
        {
            _screenController.HideCurrentScreen();
            _screenController.ShowScreen(ScreenName.Main);
            _cameraController.DiscardPhoto();
        }
    }
}
*/
