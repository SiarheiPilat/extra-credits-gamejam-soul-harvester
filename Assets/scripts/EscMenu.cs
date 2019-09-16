using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{

    public GameObject EscapeMenu;
    public GameObject[] Deactivateable;
    bool paused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            EscapeMenu.SetActive(true);
            Time.timeScale = 0.0f;
            foreach (GameObject i in Deactivateable)
            {
                i.SetActive(false);
            }
            paused = true;
        }

        if (paused && Input.GetKey(KeyCode.Y))
        {
            QuitGame();
        }

        if (paused && Input.GetKey(KeyCode.N))
        {
            ReturnToGame();
            paused = false;
        }
    }


    public void ReturnToGame()
    {
        EscapeMenu.SetActive(false);
        Time.timeScale = 1.0f;
        foreach (GameObject i in Deactivateable)
        {
            i.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
