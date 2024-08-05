using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManagement : MonoBehaviour
{
    public GameObject settingcanvas;
    
    public void SettingTrigger()
    {
        settingcanvas.SetActive(true);
    }
    public void SettingClose()
    {
        settingcanvas.SetActive(false);
    }
    public void RegScene()
    {
        SceneManager.LoadScene("RegisterScene");
    }
    public void LogScene()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public void BackScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}
