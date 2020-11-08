using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveSpawnMessage : MonoBehaviour
{
    public Image panel;
    public float DelayBetweenWaves;
    private TextMeshProUGUI text;
    private int waveNumber = 0;
    private WavesController controller;
    private FaderController fader;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        controller = FindObjectOfType<WavesController>();
        fader = GetComponent<FaderController>();
        controller.OnChangeWaves += StartNextWave;
        fader.SetPanel(panel);

        StartNextWave(controller);
    }

    public void StartNextWave(WavesController wave)
    {
        text.text = "Wave " + waveNumber.ToString();
        StartCoroutine(ActivateNextWave(DelayBetweenWaves));
    }

    private IEnumerator ActivateNextWave(float delay)
    {
        fader.FadeInOut(delay / 2, panel.color, 1, panel.color, delay/2);

        yield return new WaitForSeconds(delay);

        controller.StartNextWave();
    }
}
