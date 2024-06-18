using System.Collections;
using UnityEngine;

namespace Manager
{
    public class CameraManager : MonoBehaviour
    {
        private const float ZoomOutFieldOfView = 120f;
        public float zoomDuration = 3f;
        private float _speedZoom = 2f;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void ZoomOutCamera()
        {
            if (_camera is not null)
                StartCoroutine(ZoomOutCoroutine());
        }

        private IEnumerator ZoomOutCoroutine()
        {
            float startFieldOfView = _camera.fieldOfView;
            float elapsed = 0;

            while (elapsed < zoomDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                _camera.fieldOfView = Mathf.Lerp(startFieldOfView, ZoomOutFieldOfView, elapsed / zoomDuration);
                yield return null;
            }

            _camera.fieldOfView = ZoomOutFieldOfView;
        }
    }
}