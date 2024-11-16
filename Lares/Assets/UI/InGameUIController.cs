using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace Lares.UI
{
    public enum MainUITabs
    {
        CHARACTER, 
        INVENTORY, 
        QUEST, 
        MAP, 
        SETTINGS
    }

    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] private Tab[] _tabs;
        private Tab _currentActiveTab;
        public MainUITabs currentActiveTab => _currentActiveTab.tabName;
        [SerializeField] PlayerInput _input;

        private void Start()
        {
            _currentActiveTab = _tabs[0];
            foreach (Tab tab in _tabs)
            {
                tab.screenButton.onClick.AddListener(() => OnActiveTabChanged(tab));
            }
            _input.actions.FindActionMap("BaseControls").FindAction("ToggleMenu").performed += MenuOn;
            _input.actions.FindActionMap("UIControls").FindAction("ToggleMenu").performed += MenuOff;
            gameObject.SetActive(false);
        }


        private void MenuOn(InputAction.CallbackContext context) 
        {
            gameObject.SetActive(true);
            _input.currentActionMap = _input.actions.FindActionMap("UIControls");
        }

        private void MenuOff(InputAction.CallbackContext context) 
        {
            gameObject.SetActive(false);
            _input.currentActionMap = _input.actions.FindActionMap("BaseControls");
        }

        private void OnActiveTabChanged(Tab newTab)
        {
            _currentActiveTab.tab.SetActive(false);
            _currentActiveTab = newTab;
            _currentActiveTab.tab.SetActive(true);
        }
    }

    [Serializable]
    public struct Tab
    {
        public MainUITabs tabName;
        public Button screenButton;
        public GameObject tab;

        public Tab(MainUITabs _tabName, Button _screenButton, GameObject _tab)
        {
            this.tabName = _tabName;
            this.screenButton = _screenButton;
            this.tab = _tab;
        }
    }

}
