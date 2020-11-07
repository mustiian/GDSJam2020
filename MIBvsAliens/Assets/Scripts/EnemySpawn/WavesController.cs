using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WavesController : MonoBehaviour
{
    public WavesCollection[] Waves;

    public Action<WavesController> OnFinishWaves;
    public Action<WavesController> OnChangeWaves;

    protected bool isFinished = false;

    protected Wave currentWaveTop;

    private int finishedLines;

    void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        finishedLines = 0;
        int finishedCollections = 0;
        Wave currentWave;
        foreach (var wave in Waves)
        {
            if (wave.NextWaveExist())
            {
                currentWave = wave.GetNextWave();
                currentWave.OnFinishWave += FinishLine;
                currentWave.ActivateWave(wave.SpawnPoint);
            }
            else
                finishedCollections++;
        }

        if (finishedCollections == Waves.Length)
            OnFinishWaves?.Invoke(this);
    }

    public void FinishLine(Wave wave)
    {
        finishedLines++;

        if (finishedLines == Waves.Length)
        {
            OnChangeWaves?.Invoke(this);
            StartNextWave();
        }
    }
}
