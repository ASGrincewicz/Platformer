﻿// Aaron Grincewicz Veganimus@icloud.com 6/5/2021

using System;
using UnityEngine;
namespace Veganimus.Platformer
{
    public class BallModeController : MonoBehaviour, IBombable
    {
        [SerializeField] private InputManagerSO _inputManager;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField, Tooltip("The rate the player can drop bombs.")]
        private float _dropRate = 0.5f;
        [SerializeField, Tooltip("Rate ate which bomb count increases.")]
        private float _bombRechargeTime = 3.0f;
        [SerializeField, Tooltip("Set the player's max bomb count.")]
        private int _bombCount = 3;
        private float _canDropBomb = -1.0f;
        private Character _player;
        private Vector3 _position;

        private void Awake()
        {
            _position = transform.position;
            _player = Character.Instance;
        }
        private void Update()
        {
            if (Time.time > _canDropBomb && _player.Upgrades.ballBombs)
            {
                var bombTriggered = _inputManager.controls.Standard.Shoot.triggered;
                if (bombTriggered && _bombCount > 0) 
                    DropBomb();
            }
        }
        private void DropBomb()
        {
            _position = transform.position;
            var bombTriggered = _inputManager.controls.Standard.Shoot.triggered;
            if (bombTriggered)
            {
                _bombCount--;
                UIManager.Instance.BombUpdate(_bombCount);
                _canDropBomb = Time.time + _dropRate;
                Instantiate(_bombPrefab,new Vector3(_position.x,_position.y, _position.z - 0.5f), Quaternion.identity);
                Invoke(nameof(RechargeBomb), _bombRechargeTime);
            }
        }
        private void RechargeBomb()
        {
            _bombCount++;
            UIManager.Instance.BombUpdate(_bombCount);
        }
    }
}