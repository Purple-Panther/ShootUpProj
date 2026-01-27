using System.Collections;
using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ControlsOverlayController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _displayDuration = 5f;
        [SerializeField] private float _fadeDuration = 1f;

        private void Start()
        {
            if (_canvasGroup == null)
            {
                if (!TryGetComponent(out _canvasGroup))
                {
                    _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = false; 
            RectTransform rect = GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchorMin = new Vector2(0, 0.5f);
                rect.anchorMax = new Vector2(0, 0.5f);
                rect.pivot = new Vector2(0, 0.5f);
                rect.anchoredPosition = new Vector2(50f, 0f); 
                rect.localScale = Vector3.one;
            }

            StartCoroutine(DisplayAndFade());
        }

        private IEnumerator DisplayAndFade()
        {
            Debug.Log("ControlsOverlay: Starting display timer...");
            yield return new WaitForSecondsRealtime(_displayDuration);

            Debug.Log("ControlsOverlay: Fading out...");
            float elapsedTime = 0f;
            float startAlpha = _canvasGroup.alpha;

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / _fadeDuration);
                yield return null;
            }

            _canvasGroup.alpha = 0f;
            Debug.Log("ControlsOverlay: Destroying overlay.");
            Destroy(gameObject);
        }
    }
}
