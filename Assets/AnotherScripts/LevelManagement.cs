using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManagement : MonoBehaviour
{
    public RectTransform SpellUI;
    public Vector2 UIpos;
    public Vector2 UIoripos;
    public float speed = 3f;
    public GameObject LevelInfoCanvas;
    public TextMeshProUGUI Leveltxt;
    public bool moved = false;
    public void Level1Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 1";
    }
    public void Level2Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 2";
    }
    public void Level3Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 3";
    }
    public void Level4Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 4";
    }
    public void Level5Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 5";
    }
    public void Level6Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 6";
    }
    public void Level7Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 7";
    }
    public void Level8Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 8";
    }
    public void Level9Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 9";
    }
    public void Level10Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 10";
    }
    public void Level11Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 11";
    }
    public void Level12Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 12";
    }
    public void Level13Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 13";
    }
    public void Level14Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 14";
    }
    public void Level15Trigger()
    {
        LevelInfoCanvas.SetActive(true);
        Leveltxt.text = "Level 15";
    }
    public void MovingCanvas()
    {
        if (moved == false)
        {
            SpellUI.anchoredPosition = UIpos;
            moved = true;
        }
        else
        {
            SpellUI.anchoredPosition = UIoripos;
            moved = false;
        }
    }
    public void LevelCancel()
    {
        LevelInfoCanvas.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
