// Aaron Grincewicz ASGrincewicz@icloud.com 7/9/2021
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class PoolManager: MonoBehaviour
    {
        public static PoolManager Instance
        {
            get
            {
                return _instance;
            }
        }
        private static PoolManager _instance;
       
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
           
        }

    }
}
