using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    int score;
    public Text timeText; // 時刻を表示するTextコンポーネント
    public Text DeathText; // 時刻を表示するTextコンポーネント
    public Text scoreText; // 時刻を表示するTextコンポーネント
    void Start()
    {
        score = 10000 - PlayerPrefs.GetInt("time") * 10 - PlayerPrefs.GetInt("Death") * 100;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time : " + PlayerPrefs.GetInt("time").ToString("00") + "秒";
        DeathText.text = "落下 : " + PlayerPrefs.GetInt("Death").ToString("00") + "回";
        timeText.text = "Score : " + score.ToString("00000");
    }

    public void OnClick()
    {
        SceneManager.instance.Title();
    }
}
