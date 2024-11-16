using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        

        private void Start()
        {
            _currentActiveTab = _tabs[0];
            foreach (Tab tab in _tabs)
            {
                tab.screenButton.onClick.AddListener(() => OnActiveTabChanged(tab));
            }
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
