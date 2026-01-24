using UnityEngine;

public class TabsManager : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public string id;
        public GameObject content;
    }

    [Header("Tabs")]
    public Tab[] tabs;

    [Header("Default")]
    public string defaultTabId;

    void Start()
    {
        OpenTab(defaultTabId);
    }

    public void OpenTab(string tabId)
    {
        foreach (var tab in tabs)
        {
            bool isActive = tab.id == tabId;
            tab.content.SetActive(isActive);
        }
    }
}

