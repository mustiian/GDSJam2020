using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Wave : MonoBehaviour
{
    public float InitiationDelay;

    public GameObject[] Enemies;

    public Action<Wave> OnFinishWave;

    public void ActivateWave(Vector3 spawnPoint)
    {
        StartCoroutine(InitEnemies(spawnPoint));
    }

    private IEnumerator InitEnemies(Vector3 position)
    {
        foreach (var enemy in Enemies)
        {
            GameObject.Instantiate(enemy, position, Quaternion.identity);
            yield return new WaitForSeconds(InitiationDelay);
        }
    }

    // Finish wave when all enemies are dead
    public void EnemyDie(GameObject obj)
    {
        Enemies = Enemies.Where(enemy => enemy != obj).ToArray();

        if (Enemies.Length == 0)
            OnFinishWave?.Invoke(this);
    }
}
