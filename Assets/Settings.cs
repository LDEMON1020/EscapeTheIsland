using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    public GameObject SettingPanelObject;
    private bool SettingPanel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SettingPanel = false;
        SettingPanelObject.SetActive(false);
    }

    void Update()
    {
        OpenSettings();
    }

    void OpenSettings()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SettingPanel = !SettingPanel;
            SettingPanelObject.SetActive(SettingPanel);
        }
    }
}
