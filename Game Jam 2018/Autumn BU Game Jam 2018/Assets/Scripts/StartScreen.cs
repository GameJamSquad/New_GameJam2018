using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

	void Update ()
    {
        if (Input.GetButtonDown("AButton_P1") || Input.GetButtonDown("AButton_P2") || Input.GetButtonDown("AButton_P3") || Input.GetButtonDown("AButton_P4"))
        {
            LoadRandomLevel();
        }
    }

    void LoadRandomLevel()
    {
        int level = Random.Range(1, 3);
        SceneManager.LoadScene(level);
    }
}
