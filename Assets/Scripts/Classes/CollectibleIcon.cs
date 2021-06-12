// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class CollectibleIcon : MonoBehaviour
    {
        public Camera _camera;    // Camera containing the canvas
        public Transform _target; // object in the 3D World
        [SerializeField] private RectTransform _icon;   // icon to place in the canvas
        public Canvas _canvas; // canvas with "Render mode: Screen Space - Camera"
        [SerializeField] private Vector2 _uiPosition;
        [SerializeField] private float _speed;

        private void Start()
        {
            Vector3 screenPos = _camera.WorldToScreenPoint(_target.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - (w / 2);
            float y = screenPos.y - (h / 2);
            float s = _canvas.scaleFactor;
            _icon.anchoredPosition = new Vector2(x, y) / s;
        }

        private void Update()
        {
            _icon.anchoredPosition = Vector3.Lerp(_icon.anchoredPosition, _uiPosition, _speed * Time.deltaTime);
        }
    }
}
