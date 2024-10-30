using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick1()
    {
        SceneManager.instance.GamePlay();
    }

    public void OnClick2()
    {
        SceneManager.instance.GamePlay1();
    }

    public void OnClick3()
    {
        SceneManager.instance.GamePlay2();
    }
}
