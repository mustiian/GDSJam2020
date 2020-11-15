using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private WavesController controller;

    public static LevelManager instance;

    public Image panelWin;
    public Image panelLose;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<WavesController>();
        controller.OnFinishWaves += WavesFinished;
    }

    public void WavesFinished(WavesController controller)
    {
        if (!panelLose)
            return;

        if (panelLose.gameObject.activeSelf == false)
        {
            panelWin.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CowsEnded(CowsManager controller)
    {
        if (!panelWin)
            return;

        if (panelWin.gameObject.activeSelf == false)
        {
            panelLose.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
