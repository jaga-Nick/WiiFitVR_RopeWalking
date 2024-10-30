using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    float time = 0f;
    int sec = 0;
    public Text timeText; // 時刻を表示するTextコンポーネント
    public Text DeathText; // 時刻を表示するTextコンポーネント


    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        sec = Mathf.FloorToInt(time);
        Debug.Log("Time : " + sec);
        timeText.text = "Time : " + sec.ToString("00") + "秒";
        DeathText.text = "落下 : " + PlayerController.DeathCount.ToString("00") + "回";
        if (PlayerController.isGoal)
        {
            PlayerPrefs.SetInt("time", sec);
        }
    }
}
