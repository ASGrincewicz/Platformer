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
        [SerializeField, Tooltip("UI Object for Pause Menu.")]
        private GameObject _pauseMenu;
        [SerializeField, Tooltip("Highlight to show secondary weapon is active.")]
        private Image _secondaryWeaponActive;
        [SerializeField] private List<Image> _bombImages = new List<Image>();
        [SerializeField] private List<Image> _livesImages = new List<Image>();
        [SerializeField] private TMP_Text _collectibleText, _healthText, _missilesText;
        [SerializeField] private TMP_Text _levelCompleteText, _upgradeText;
        private int _collectiblesCollected;
        private int _currentBombs;
        private int _currentHealth;
        private int _currentLives;
        private int  _secondaryAmmoCount;
        private WaitForSeconds _upgradeTextRoutineDelay;
        public TMP_Text MissileText { get => _missilesText;  set { } } 

        private void Awake()
        {
            _instance = this;
            _upgradeTextRoutineDelay = new WaitForSeconds(0.5f);
        }

        private void Start()
        {
            HealthTextUpdate(_currentHealth);
            LivesUpdate(_currentLives);
            SecondaryFireActive(false);
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
        /// <summary>
        /// Sets the state of the pause menu.
        /// </summary>
        /// <param name="isActive"></param>
        public void ActivatePauseMenu(bool isActive) => _pauseMenu.SetActive(isActive);

        /// <summary>
        /// Updates the UI to show current available bombs.
        /// </summary>
        /// <param name="amount"></param>
        public void BombUpdate(int amount)
        {
            _currentBombs = amount;
            ClearAmount(_bombImages);
            ShowAmount(1, amount);
        }

        /// <summary>
        /// Updates the UI to show current amount of collectibles collected.
        /// </summary>
        /// <param name="amount"></param>
        public void CollectibleTextUpdate(int amount)
        {
            _collectiblesCollected += amount;
            _collectibleText.text = $"{_collectiblesCollected}";
        }

        /// <summary>
        /// Shows text on screen to indicate the current level's win conditions
        /// have been met.
        /// </summary>
        public void DisplayLevelComplete()
        {
            _levelCompleteText.text = "Level Complete!";
            _levelCompleteText.gameObject.SetActive(true);
        }

        /// <summary>
        /// Updates the player's health UI to show current health.
        /// </summary>
        /// <param name="amount"></param>
        public void HealthTextUpdate(int amount)
        {
            _currentHealth = amount;
            _healthText.text = $"Health: {_currentHealth}";
        }

        /// <summary>
        /// Updates the player's UI to show current lives available.
        /// </summary>
        /// <param name="amount"></param>
        public void LivesUpdate(int amount)
        {
            _currentLives = amount - 1;
            ClearAmount(_livesImages);
            ShowAmount(0, _currentLives);
        }

        /// <summary>
        /// Updates the player's UI to show current missile count.
        /// </summary>
        /// <param name="amount"></param>
        public void MissilesTextUpdate(int amount)
        {
            _secondaryAmmoCount = amount;
            _missilesText.text = $"Missiles: {_secondaryAmmoCount}";
        }

        /// <summary>
        /// Toggles a highlight on the secondary fire mode UI to show it's
        /// current state.
        /// </summary>
        /// <param name="isActive"></param>
        public void SecondaryFireActive(bool isActive)
        {
            if (!isActive)
                _secondaryWeaponActive.canvasRenderer.SetAlpha(0);
            else
                _secondaryWeaponActive.canvasRenderer.SetAlpha(0.75f);
        }


        /// <summary>
        /// Displays UI to show the upgrade acquired.
        /// </summary>
        /// <param name="upgradeName"></param>
        /// <returns></returns>
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
