// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class UIManager : MonoBehaviour
    {
       public static UIManager Instance
        {
            get
            {
                return _instance;
            }
        }
        private static UIManager _instance;
        public Canvas canvas;
        public Camera cam;

        private void Awake()
        {
            _instance = this;
        }

        
    }
   
}
