using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private WavesController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller.OnFinishWaves += WavesFinished;
    }

    public void WavesFinished(WavesController controller)
    {
        // TODO
    }

    public void StartNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
