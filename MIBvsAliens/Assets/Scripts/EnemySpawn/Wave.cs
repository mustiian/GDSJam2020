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

    private List<GameObject> enemies = new List<GameObject>();

    public void ActivateWave(Vector3 spawnPoint, Vector3 endPoint)
    {
        StartCoroutine(InitEnemies(spawnPoint, endPoint));
    }

    private IEnumerator InitEnemies(Vector3 startPosition, Vector3 endPosition)
    {
        foreach (var enemy in Enemies)
        {
            Vector2 start = new Vector2(startPosition.x, startPosition.y + _yOffset);
            var gameEnemy = GameObject.Instantiate(enemy, start, Quaternion.identity);
            enemies.Add(gameEnemy);

            if (gameEnemy.TryGetComponent(out Fight fight)){
                fight.Died += GameManager.instance.pointsManager.AddPoints;
                fight.AfterAnimationDied += EnemyDie;
            }

            if (gameEnemy.TryGetComponent(out Movement movement))
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
            enemies.Remove(creature.gameObject);

            if (creature.TryGetComponent(out Fight fight))
            {
                fight.Died -= EnemyDie;
            }

            Destroy(creature.gameObject);

            if (enemies.Count == 0)
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
