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
    public void ShopNLobby()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            SceneManager.LoadScene("ShopScene");
        }
        if (SceneManager.GetActiveScene().name == "ShopScene")
        {
            SceneManager.LoadScene("Lobby");
        }
    }
    public void Logout()
    {
        SceneManager.LoadScene("Menu");
    }
}
