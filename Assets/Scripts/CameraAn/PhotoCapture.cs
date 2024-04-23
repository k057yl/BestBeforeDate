using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace CameraAn
{
    public class PhotoCapture
    {
        private WebCamTexture _webcamTexture;

        public void OpenCamera(VisualElement containerElement)
        {
            containerElement.Clear();

            _webcamTexture = new WebCamTexture();
            _webcamTexture.Play();

            Texture2D texture = new Texture2D(_webcamTexture.width, _webcamTexture.height, TextureFormat.RGBA32, false);
            texture.SetPixels(_webcamTexture.GetPixels());
            texture.Apply();

            // Создаем изображение камеры и устанавливаем его текстуру
            var cameraImage = new UnityEngine.UIElements.Image();
            cameraImage.image = texture;

            // Поворачиваем изображение на 90 градусов (если необходимо)
            //cameraImage.style.transform = Quaternion.Euler(0, 0, 90f).ToAngleAxis();
    
            // Добавляем изображение в контейнер
            containerElement.Add(cameraImage);

            // Подгоняем размер изображения под размер контейнера
            cameraImage.style.width = containerElement.resolvedStyle.width;
            cameraImage.style.height = containerElement.resolvedStyle.height;
        }

        public void TakePhoto(Transform containerTransform)
        {
            if (_webcamTexture == null)
            {
                Debug.LogError("WebCamTexture is not initialized!");
                return;
            }

            Texture2D photoTexture = new Texture2D(_webcamTexture.width, _webcamTexture.height);
            photoTexture.SetPixels(_webcamTexture.GetPixels());
            photoTexture.Apply();

            string filePath = Path.Combine(Application.persistentDataPath, "photo.png");
            File.WriteAllBytes(filePath, photoTexture.EncodeToPNG());
        }

        public void StopCamera()
        {
            if (_webcamTexture != null)
            {
                _webcamTexture.Stop();
            }
        }
    }
}

