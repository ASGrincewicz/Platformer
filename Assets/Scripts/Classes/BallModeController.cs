﻿// Aaron Grincewicz Veganimus@icloud.com 6/5/2021
using UnityEngine;
namespace Veganimus.Platformer
{
    public class BallModeController : MonoBehaviour, IBombable
    {
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private float _dropRate = 0.5f;
        [SerializeField] private float _bombRechargeTime = 3.0f;
        [SerializeField] private int _bombCount = 3;
        private float _canDropBomb = -1.0f;
        private InputManager _inputManager;

        private void Start()
        {
            _inputManager = GetComponentInParent<InputManager>();
        }
        private void Update()
        {
            if (Time.time > _canDropBomb)
            {
                var bombTriggered = _inputManager.controls.Standard.Shoot.triggered;
                if (bombTriggered && _bombCount > 0)
                {
                    DropBomb();
                }
                else
                    return;
            }
        }
        private void DropBomb()
        {
            var bombTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (bombTriggered)
            {
                _bombCount--;
                _canDropBomb = Time.time + _dropRate;
                Instantiate(_bombPrefab,new Vector3(transform.position.x,transform.position.y, transform.position.z - 0.5f), Quaternion.identity);
                Invoke("RechargeBomb", _bombRechargeTime);
            }
        }
        private void RechargeBomb()
        {
            _bombCount++;
        }
    }
}