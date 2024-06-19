using System.Collections;
using UnityEngine;

namespace Manager
{
    public class CameraManager : MonoBehaviour
    {
        private const float ZoomOutFieldOfView = 120f;
        public float zoomDuration = 3f;
        private float _speedZoom = 2f;

        private LevelUp _levelUp;
        private Camera _camera;
        private Player _player;

        private void Start()
        {
            _camera = Camera.main;
            _levelUp = GameObject.FindGameObjectWithTag(Constraints.GameManagerTag).GetComponentInChildren<LevelUp>();
            _player = Constraints.PlayerGameObject.GetComponent<Player>();
            _levelUp.OnGameUnpaused += ZoomOutCamera;
        }

        public void ZoomOutCamera()
        {
            if (_camera is not null)
            {
                if (_player.Data.Level >= 11)
                    StartCoroutine(ZoomOutCoroutine());
            }
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