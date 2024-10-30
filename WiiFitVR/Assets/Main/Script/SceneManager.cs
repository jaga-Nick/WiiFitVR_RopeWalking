using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    void Awake()
    {
        CheckInstance();
    }

    void CheckInstance() //�V���O���g��
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Title() //�^�C�g��
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("title");
    }

    public void GamePlay() //�Q�[�����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    public void GamePlay1() //�Q�[�����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main 1");
    }

    public void GamePlay2() //�Q�[�����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main 2");
    }

    public void GameResult() //�Q�[���I�[�o�[���
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }




    public void EndGame() //�Q�[���I��
    {
#if UNITY_EDITOR //unity���ŃQ�[����
        UnityEditor.EditorApplication.isPlaying = false;
#else //�r���h���ꂽ�Q�[���̎�
            Application.Quit();
#endif
    }
}
