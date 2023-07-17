using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] 
        private Color highlightColour;
        [SerializeField] 
        private Color outlineColour;
        private Color _defaultColour = new Color(0.93f, 0.6f, 0.20392156862f);
        
        private Outline _outline;
        private Image _buttonImage;
        
        void Awake()
        {
            _outline = GetComponent<Outline>();
            _buttonImage = GetComponent<Image>();

            _defaultColour = _buttonImage.color;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonImage.DOColor(highlightColour, 0.1f).SetUpdate(true);
            _outline.DOColor(outlineColour, 0.1f).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonImage.DOColor(_defaultColour, 0.1f).SetUpdate(true);
            _outline.DOColor(Color.clear, 0.1f).SetUpdate(true);
        }
    }
}
