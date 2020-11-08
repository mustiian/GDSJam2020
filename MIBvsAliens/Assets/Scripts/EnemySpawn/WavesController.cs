using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WavesController : MonoBehaviour
{
    public WavesCollection[] WavesCollections;

    public Action<WavesController> OnFinishWaves;
    public Action<WavesController> OnChangeWaves;

    protected bool isFinished = false;

    protected Wave currentWaveTop;

    private int finishedLines;

    void Start()
    {
        //StartNextWave();
    }

    public void StartNextWave()
    {
        finishedLines = 0;
        int finishedCollections = 0;
        Wave currentWave;
        foreach (var wave in WavesCollections)
        {
            if (wave.NextWaveExist())
            {
                currentWave = wave.GetNextWave();
                currentWave.OnFinishWave += FinishLine;
                currentWave.ActivateWave(wave.SpawnPoint.position, wave.EndPoint.position);
            }
            else
                finishedCollections++;
        }

        if (finishedCollections == WavesCollections.Length)
            OnFinishWaves?.Invoke(this);
    }

    public void FinishLine(Wave wave)
    {
        finishedLines++;

        if (finishedLines == WavesCollections.Length)
        {
            OnChangeWaves?.Invoke(this);
            StartNextWave();
        }
    }
}
