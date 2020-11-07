using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesCollection : MonoBehaviour
{
    public Vector3 SpawnPoint;

    public Wave[] Waves;

    private int nextWaveIndex = 0;

    public bool NextWaveExist() => nextWaveIndex != Waves.Length;

   public Wave GetNextWave()
    {
        if (nextWaveIndex < Waves.Length)
            return Waves[nextWaveIndex++];
        else
            return null;
    }
}
