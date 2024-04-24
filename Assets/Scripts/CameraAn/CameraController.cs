using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace CameraAn
{
    public class CameraController
    {
        private VisualElement _cameraContainer;
        private WebCamTexture _webcamTexture;

        private List<Texture2D> _photoList = new List<Texture2D>();//***************************
        
        public CameraController(VisualElement cameraContainer)
        {
            _cameraContainer = cameraContainer;
        }

        public void OpenCamera()
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
            
            string fileName = "photo_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            string imageFilePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(imageFilePath, photoBytes);

            Debug.Log("Image saved to: " + imageFilePath);
            
            string jsonFilePath = Path.Combine(Application.persistentDataPath, "photos.json");
            if (File.Exists(jsonFilePath))
            {
                List<PhotoData> photoList = JsonUtility.FromJson<List<PhotoData>>(File.ReadAllText(jsonFilePath));
                photoList.Add(photoData);
                jsonData = JsonUtility.ToJson(photoList);
            }
            else
            {
                List<PhotoData> photoList = new List<PhotoData>();
                photoList.Add(photoData);
                jsonData = JsonUtility.ToJson(photoList);
            }

            File.WriteAllText(jsonFilePath, jsonData);

            Debug.Log("Photo data saved to: " + jsonFilePath);
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

        public void DiscardPhoto()
        {
            if (_webcamTexture != null)
            {
                _webcamTexture.Stop();
                _webcamTexture = null;
                var existingCameraImage = _cameraContainer.Q<Image>();
                if (existingCameraImage != null)
                {
                    existingCameraImage.RemoveFromHierarchy();
                }
            }
        }
        
        public void Dispose()
        {
            if (_webcamTexture != null)
            {
                _webcamTexture.Stop();
            }
        }
        
        public Texture2D GetLastPhoto()//***********************
        {
            if (_photoList.Count > 0)
            {
                return _photoList[_photoList.Count - 1];
            }
            else
            {
                Debug.LogError("No photos available.");
                return null;
            }
        }
    }

    [System.Serializable]
    public class PhotoData
    {
        public string base64Image;
        public int width;
        public int height;
        public string timestamp;
        public string Date;
    }
}