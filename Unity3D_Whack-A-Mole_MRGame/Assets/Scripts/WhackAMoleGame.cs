using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WhackAMoleGame : MonoBehaviour
{
    [Header("Popup Settings")]
    public TMP_Text gameData;

    GameObject[] TargetObjects;
    float popupTimer = 1;
    static public int score = 0;
    public float GameTime = 30;

    static public bool LeftHandInUse = false;
    static public bool RightHandInUse = false;
 
    // Start is called before the first frame update
    void Start()
    {
        TargetObjects = GameObject.FindGameObjectsWithTag("TargetObject");
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime;
        if (GameTime < 0)
        {
            SceneManager.LoadScene("StartScene");
        }

        UpdateMoles();
        UpdateGUI();
    }

    void UpdateMoles()
    {
        popupTimer -= Time.deltaTime;

        if (popupTimer < 0)
        {
            int rnd = Random.Range(0, TargetObjects.Length);
            var script = TargetObjects[rnd].GetComponent<TargetObject_Jump>();
            script.Pop();

            popupTimer += Random.Range(1, 3);
        }
    }

    void UpdateGUI()
    {
        string buffer = "Time: " + GameTime.ToString("00.0") + "\n Score: " + score;
        gameData.text = buffer;
    }
}