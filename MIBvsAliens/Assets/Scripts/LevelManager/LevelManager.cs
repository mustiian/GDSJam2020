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

    private FaderController fader;

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
        controller.OnFinishWaves += LevelWin;
        fader = FindObjectOfType<FaderController>();
        fader.FadeOut(1f);
    }

    public void LevelWin(WavesController controller)
    {
        if (!panelLose)
            return;

        if (panelLose.gameObject.activeSelf == false)
        {
            panelWin.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LevelWin()
    {
        if (!panelLose)
            return;

        if (panelLose.gameObject.activeSelf == false)
        {
            panelWin.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LevelLose()
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
        StartCoroutine(LoadScene(1f, SceneManager.GetActiveScene().buildIndex));
    }

    public void StartNextLevel()
    {
        StartCoroutine(LoadScene(1f, SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(float delay, int index)
    {
        Time.timeScale = 1f;
        fader.FadeIn(delay);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}
