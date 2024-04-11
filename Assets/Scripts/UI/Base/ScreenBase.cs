using UI.Interfaces;
using UnityEngine.UIElements;
using Zenject;

namespace UI.Base
{
    public class ScreenBase : IScreen
    {
        protected ScreenController _screenController;
        
        protected VisualElement _rootElement;
        protected VisualElement _screenElement;


        [Inject]
        public void Initialize(UIDocument uiDocument)
        {
            _rootElement = uiDocument.rootVisualElement;
            OnInitialized();
        }

        public virtual void OnInitialized()
        {
            Hide();
        }

        [Inject]
        public void Initialize(ScreenController screenController)
        {
            if (_screenController != null) return;
            _screenController = screenController;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Show()
        {
            PreShow();
        }
        
        private void PreShow()
        {
            if (_screenElement != null)
            {
                _screenElement.style.display = DisplayStyle.Flex;
            }
        }

        public virtual void Hide()
        {
            if (_screenElement != null)
            {
                _screenElement.style.display = DisplayStyle.None;
            }
        }
    }
}