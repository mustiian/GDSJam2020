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
    private int waveNumber = 1;
    private WavesController controller;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        controller = FindObjectOfType<WavesController>();
        animator = GetComponent<Animator>();
        controller.OnChangeWaves += StartNextWave;

        StartNextWave(controller);
    }

    public void StartNextWave(WavesController wave)
    {
        text.text = "Wave " + waveNumber.ToString();
        StartCoroutine(ActivateNextWave(DelayBetweenWaves));
    }

    private IEnumerator ActivateNextWave(float delay)
    {
        animator.Play("Appear");
        yield return new WaitForSeconds(delay);
        animator.Play("Disappear");

        controller.StartNextWave();
    }
}
