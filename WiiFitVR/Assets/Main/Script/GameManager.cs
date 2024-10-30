using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    float time = 0f;
    int sec = 0;
    public Text timeText; // ������\������Text�R���|�[�l���g
    public Text DeathText; // ������\������Text�R���|�[�l���g


    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        sec = Mathf.FloorToInt(time);
        Debug.Log("Time : " + sec);
        timeText.text = "Time : " + sec.ToString("00") + "�b";
        DeathText.text = "���� : " + PlayerController.DeathCount.ToString("00") + "��";
        if (PlayerController.isGoal)
        {
            PlayerPrefs.SetInt("time", sec);
        }
    }
}
