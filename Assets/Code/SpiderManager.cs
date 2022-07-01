using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SpiderManager: MonoBehaviour
{
    [SerializeField] private int _capasity = 30;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _timeSpawnSpider;

    private SpiderPool _spiderPool;
    private SpiderFactory _spiderFactory;

    private void Start()
    {
        _spiderPool = new SpiderPool(_capasity, _spawnPoints);
        _spiderFactory = new SpiderFactory(_spiderPool, _timeSpawnSpider);
    }

    private void Update()
    {
       _spiderFactory.Execute();
    }
}

