using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Rendering;

public class Wave : MonoBehaviour
{
    public float InitiationDelay;

    public GameObject[] Enemies;

    public Action<Wave> OnFinishWave;

    private float _yOffset = -0.2f;

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

            if (gameEnemy.TryGetComponent(out FightingSystem fight))
            {
                fight.Died += GameManager.instance.pointsManager.AddPoints;
                fight.AfterAnimationDied += UnitDied;
            }

            if (gameEnemy.TryGetComponent(out UnitControlSystem unitControlSystem))
            {
                unitControlSystem.Initialize(start, endPosition);
                unitControlSystem.WillBeDestroyed += UnitControlSystemOnWillBeDestroyed;
            }

            //gameEnemy.GetComponent<SortingGroup>().sortingOrder += sortingLayer;

            GenerateNewOffset();

            yield return new WaitForSeconds(InitiationDelay);
        }
    }

    private void UnitControlSystemOnWillBeDestroyed(object sender, EventArgs e)
    {
        if (sender is UnitControlSystem controlSystem)
        {
            RemoveUnit(controlSystem);
        }
    }

    private void RemoveUnit(UnitControlSystem controlSystem, FightingSystem fightingSystem = null)
    {
        controlSystem.WillBeDestroyed -= UnitControlSystemOnWillBeDestroyed;
        if (fightingSystem == null)
            fightingSystem = controlSystem.GetComponent<FightingSystem>();
        RemoveFightingSystem(fightingSystem);
    }

    private void RemoveFightingSystem(FightingSystem fightingSystem)
    {
        enemies.Remove(fightingSystem.gameObject);

        Debug.Log("Enemy die");

        fightingSystem.AfterAnimationDied -= UnitDied;
        fightingSystem.Died -= GameManager.instance.pointsManager.AddPoints;

        if (enemies.Count == 0)
            OnFinishWave?.Invoke(this);
    }


    private void UnitDied(object sender, EventArgs e)
    {
        if (sender is FightingSystem fightingSystem)
        {
            var controlSystem = fightingSystem.GetComponent<UnitControlSystem>();
            RemoveUnit(controlSystem, fightingSystem);
        }
    }

    private void GenerateNewOffset()
    {
        if (_offsetCount < 3)
        {
            _yOffset -= 0.2f;
            _offsetCount++;
        }
        else
        {
            _yOffset = 0.2f;
            _offsetCount = 0;
        }
    }
}
