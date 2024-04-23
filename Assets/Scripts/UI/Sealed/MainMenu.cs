using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEngine.Android;

namespace UI.Sealed
{
    public class MainMenu : ScreenBase
    {
        private VisualElement _leftHeaderContainerButtonBack;
        private VisualElement _leftHeaderContainerButtonExit;
        private VisualElement _openCameraButton;
        private VisualElement _takePhotoButton;

        private VisualElement _cameraContainer;

        private WebCamTexture _webcamTexture;

        public override void OnInitialized()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
            _screenElement = _rootElement.Q<VisualElement>(Constants.MAIN_MENU);
            base.OnInitialized();
            AssignButtons();
            InitHeaderButtons();
            StartApplication();
        }

        private void AssignButtons()
        {
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
            OpenCamera();
        }

        private void OnTakePhotoButtonClicked()
        {
            TakePhoto();
        }

        private void StartApplication()
        {
            _screenController.ShowScreen(ScreenName.Main);
        }
        
        
        void OpenCamera()
        {
            var existingCameraImage = _cameraContainer.Q<Image>();
            
            if (existingCameraImage != null)
            {
                existingCameraImage.RemoveFromHierarchy();
            }
            
            _webcamTexture = new WebCamTexture();
            _webcamTexture.Play();
            
            var cameraImage = new Image();
            cameraImage.image = _webcamTexture;
            
            cameraImage.transform.rotation = Quaternion.Euler(0, 0, 90f);
            
            _cameraContainer.Add(cameraImage);
            
            _cameraContainer.style.display = DisplayStyle.Flex;
        }
        
        public void TakePhoto()
        {
            if (_webcamTexture == null)
            {
                Debug.LogError("WebCamTexture is not initialized!");
                return;
            }
            
            Texture2D photoTexture = new Texture2D(_webcamTexture.width, _webcamTexture.height);
            photoTexture.SetPixels(_webcamTexture.GetPixels());
            photoTexture.Apply();
            
            photoTexture = RotateTexture(photoTexture, 90);
            
            byte[] photoBytes = photoTexture.EncodeToPNG();
            
            PhotoData photoData = new PhotoData();
            photoData.base64Image = System.Convert.ToBase64String(photoBytes);
            photoData.width = _webcamTexture.width;
            photoData.height = _webcamTexture.height;
            photoData.timestamp = System.DateTime.Now.ToString();
            
            string jsonData = JsonUtility.ToJson(photoData);
            
            string jsonFilePath = Path.Combine(Application.persistentDataPath, "photo.json");
            File.WriteAllText(jsonFilePath, jsonData);
            
            string imageFileName = "photo.png";
            string imageFilePath = Path.Combine(Application.persistentDataPath, imageFileName);
            File.WriteAllBytes(imageFilePath, photoBytes);

            Debug.Log("Photo saved to: " + jsonFilePath);
            Debug.Log("Image saved to: " + imageFilePath);
        }
        
        private Texture2D RotateTexture(Texture2D originalTexture, float angle)
        {
            int width = originalTexture.width;
            int height = originalTexture.height;

            Texture2D rotatedTexture = new Texture2D(height, width);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    rotatedTexture.SetPixel(y, width - x - 1, originalTexture.GetPixel(x, y));
                }
            }

            rotatedTexture.Apply();

            return rotatedTexture;
        }

        [System.Serializable]
        public class PhotoData
        {
            public string base64Image;
            public int width;
            public int height;
            public string timestamp;
        }
        
        void OnDestroy()
        {
            if (_webcamTexture != null)
            {
                _webcamTexture.Stop();
            }
        }
    }
}