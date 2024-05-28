using System;
using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;
using Random = System.Random;

public class GameMode : MonoBehaviour
{
    [SerializeField, Header("Enemies Spawn rate")] private GameObject enemiesObject;
    [SerializeField] private float baseEnemiesSpawnRate = 1;
    [SerializeField] private float minDistanceToSpawn = 10;
    [SerializeField] private float maxDistanceToSpawn = 15;
    
    private float _lTimerForSpawn;
    private Transform _playerRef; 
    
    private void Start()
    {
        _playerRef = FindFirstObjectByType<PlayerEngine>().transform;
    }

    private void Update()
    {
        if (_lTimerForSpawn <= baseEnemiesSpawnRate)
        {
            _lTimerForSpawn += Time.deltaTime;
        }
        else
        {
            // float lRandomPoint = 
            //
            // Instantiate(enemiesObject, _playerRef);
            _lTimerForSpawn = 0;
        }
    }
}
