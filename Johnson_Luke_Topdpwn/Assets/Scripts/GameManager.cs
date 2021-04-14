using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerKillCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerKillCount = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if ((SceneManager.GetActiveScene().name == "SampleScene") && playerKillCount >= 15)
            SceneManager.LoadScene("Level2");

        else if ((SceneManager.GetActiveScene().name == "Level2") && playerKillCount >= 15)
            SceneManager.LoadScene("MainMenu");

    }
}
