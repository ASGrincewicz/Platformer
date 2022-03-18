// Aaron Grincewicz ASGrincewicz@icloud.com 6/11/2021
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private int _collectiblesCollected;
        private int _maxBombs;
        private int _maxHealth;
        private int _maxLives;
        private int _currentBombs;
        private int _currentHealth;
        private int _currentLives;
        private int _maxMissileCount;
        private int  _missileCount;
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
            for (int i = 0; i < toClear.Count; i++)
                toClear[i].canvasRenderer.SetAlpha(0);
        }
        private void ShowAmount(int toShow, int amount)
        {
            for (int i = 0; i < amount; i++)
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

        public void BombUpdate(int amount)
        {
            _currentBombs = amount;
            ClearAmount(_bombImages);
            ShowAmount(1, amount);
        }
        public void CollectibleTextUpdate(int amount)
        {
            _collectiblesCollected += amount;
            _collectibleText.text = $"{_collectiblesCollected}";
        }
        public void DisplayLevelComplete()
        {
            _levelCompleteText.text = "Level Complete!";
            _levelCompleteText.gameObject.SetActive(true);
        }

        public void HealthTextUpdate(int amount)
        {
            _currentHealth = amount;
            _healthText.text = $"Health: {_currentHealth}";
        }
        public void LivesUpdate(int amount)
        {
            _currentLives = amount - 1;
            ClearAmount(_livesImages);
            ShowAmount(0, _currentLives);
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
            GameManager.Instance.AcquireUpgradeEvent(true);
            yield return _upgradeTextRoutineDelay;
            _upgradeText.text = string.Empty;
            GameManager.Instance.AcquireUpgradeEvent(false);
            _upgradeText.gameObject.SetActive(false);
        }
    }
}
