using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesCollection : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform EndPoint;

    private Wave[] Waves;

    private int nextWaveIndex = 0;

    public bool NextWaveExist() => nextWaveIndex != Waves.Length;

    private void Start()
    {
        Waves = GetComponentsInChildren<Wave>();
    }

    public Wave GetNextWave()
    {
        if (nextWaveIndex < Waves.Length)
            return Waves[nextWaveIndex++];
        else
            return null;
    }
}
