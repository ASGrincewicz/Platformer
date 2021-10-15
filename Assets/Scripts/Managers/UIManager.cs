// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

namespace Veganimus.Platformer
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        public static UIManager Instance { get { return _instance; } }
        private static UIManager _instance;
        #endregion
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Image _missileActiveBox;
        [SerializeField] private List<Image> _bombImages = new List<Image>();
        [SerializeField] private List<Image> _livesImages = new List<Image>();
        [SerializeField] private TMP_Text _collectibleText, _healthText, _missilesText;
        [SerializeField] private TMP_Text _levelCompleteText, _upgradeText;
        private byte _collectiblesCollected;
        private byte _maxBombs;
        private byte _maxHealth;
        private byte _maxLives;
        private sbyte _currentBombs;
        private sbyte _currentHealth;
        private sbyte _currentLives;
        private int _maxMissileCount;
        private int _missileCount;
        private WaitForSeconds _upgradeTextRoutineDelay;
        public TMP_Text MissileText { get { return _missilesText; } set { } } 

        private void Awake() => _instance = this;

        private void Start()
        {
            HealthTextUpdate(_currentHealth);
            LivesUpdate(_currentLives);
            SecondaryFireActive(false);
            _upgradeTextRoutineDelay = new WaitForSeconds(0.5f);
        }
       
        private void ClearAmount(List<Image> toClear)
        {
            for (byte i = 0; i < toClear.Count; i++)
                toClear[i].canvasRenderer.SetAlpha(0);
        }
        private void ShowAmount(byte toShow, sbyte amount)
        {
            for (byte i = 0; i < amount; i++)
            {
                switch(toShow)
                {
                    case 0://Lives
                        _livesImages[i].canvasRenderer.SetAlpha(1);
                        break;
                    case 1://Bombs
                        _bombImages[i].canvasRenderer.SetAlpha(1);
                        break;
                }
            }
        }

        public void ActivatePauseMenu(bool isActive) => _pauseMenu.SetActive(isActive);

        public void BombUpdate(sbyte amount)
        {
            _currentBombs = amount;
            ClearAmount(_bombImages);
            ShowAmount(1, amount);
        }
        public void CollectibleTextUpdate(byte amount)
        {
            _collectiblesCollected += amount;
            _collectibleText.text = $"{_collectiblesCollected}";
        }
        public void DisplayLevelComplete()
        {
            _levelCompleteText.text = "Level Complete!";
            _levelCompleteText.gameObject.SetActive(true);
        }

        public void HealthTextUpdate(sbyte amount)
        {
            _currentHealth = amount;
            _healthText.text = $"Health: {_currentHealth}";
        }
        public void LivesUpdate(sbyte amount)
        {
            _currentLives = (sbyte)(amount - 1);
            ClearAmount(_livesImages);
            ShowAmount((byte)0, _currentLives);
        }

        public void MissilesTextUpdate(int amount)
        {
            _missileCount = amount;
            _missilesText.text = $"Missiles: {_missileCount}";
        }

        public void SecondaryFireActive(bool isActive)
        {
            if (!isActive)
                _missileActiveBox.canvasRenderer.SetAlpha(0);
            else
                _missileActiveBox.canvasRenderer.SetAlpha(0.75f);
        }

        public IEnumerator AcquireUpgradeRoutine(string upgradeName)
        {
            _upgradeText.gameObject.SetActive(true);
            _upgradeText.text = $"{upgradeName} Acquired!";
            Time.timeScale = 0.25f;
            yield return _upgradeTextRoutineDelay;
            _upgradeText.text = string.Empty;
            Time.timeScale = 1.0f;
            _upgradeText.gameObject.SetActive(false);
        }
    }
}
