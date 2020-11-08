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

    private float _yOffset = -0.6f;

    private int _offsetCount = 0;

    public void ActivateWave(Vector3 spawnPoint, Vector3 endPoint)
    {
        StartCoroutine(InitEnemies(spawnPoint, endPoint));
    }

    private IEnumerator InitEnemies(Vector3 startPosition, Vector3 endPosition)
    {
        foreach (var enemy in Enemies)
        {
            Vector2 start = new Vector2(startPosition.x, startPosition.y + _yOffset);
            GameObject.Instantiate(enemy, start, Quaternion.identity);
            if (enemy.TryGetComponent(out Fight fight)){
                fight.Died += EnemyDie;
                fight.AfterAnimationDied += GameManager.instance.pointsManager.AddPoints;
            }

            if (enemy.TryGetComponent(out Movement movement))
            {
                movement.SetStartPosition(start);
                movement.SetDestination(endPosition);
            }

            GenerateNewOffset();

            yield return new WaitForSeconds(InitiationDelay);
        }
    }

    // Finish wave when all enemies are dead
    public void EnemyDie(object sender, EventArgs args)
    {
        if (sender is BaseCreature creature)
        {
            Enemies = Enemies.Where(enemy => enemy != creature.gameObject).ToArray();

            if (creature.TryGetComponent(out Fight fight))
            {
                fight.Died -= EnemyDie;
            }

            if (Enemies.Length == 0)
                OnFinishWave?.Invoke(this);
        }
    }

    private void GenerateNewOffset()
    {
        if (_offsetCount < 4)
        {
            _yOffset += 0.3f;
            _offsetCount++;
        }
        else
        {
            _yOffset = -0.6f;
            _offsetCount = 0;
        }
    }
}
