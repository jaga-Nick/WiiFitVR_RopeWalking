using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    int score;
    public Text timeText; // ������\������Text�R���|�[�l���g
    public Text DeathText; // ������\������Text�R���|�[�l���g
    public Text scoreText; // ������\������Text�R���|�[�l���g
    void Start()
    {
        score = 10000 - PlayerPrefs.GetInt("time") * 10 - PlayerPrefs.GetInt("Death") * 100;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time : " + PlayerPrefs.GetInt("time").ToString("00") + "�b";
        DeathText.text = "���� : " + PlayerPrefs.GetInt("Death").ToString("00") + "��";
        timeText.text = "Score : " + score.ToString("00000");
    }

    public void OnClick()
    {
        SceneManager.instance.Title();
    }
}
