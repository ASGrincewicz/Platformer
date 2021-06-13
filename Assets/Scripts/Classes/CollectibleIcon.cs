// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using UnityEngine;
using UnityEngine.UI;
namespace Veganimus.Platformer
{
    public class CollectibleIcon : MonoBehaviour
    {
        public Camera uiCamera;    // Camera containing the canvas
        public Transform target; // object in the 3D World
        public Image icon;   // icon to place in the canvas
        public Canvas canvas; // canvas with "Render mode: Screen Space - Camera"
        public Vector2 uiPosition;
        public float speed;
        public RectTransform rectTransform;
        public GameObject parentCollectible;

       
        private void OnEnable()
        {
            var player = GameObject.Find("Character");
            target = player.transform;
        }


        private void Start()
        {

            Vector3 screenPos = uiCamera.WorldToScreenPoint(target.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - (w / 2);
            float y = screenPos.y - (h / 2);
            float s = canvas.scaleFactor;
            rectTransform.anchoredPosition = new Vector2(x, y) / s;
        }

        private void Update()
        {
           rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, uiPosition, speed * Time.deltaTime);
        }
    }
}
