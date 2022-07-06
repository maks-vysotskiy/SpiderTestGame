using System;
using UnityEngine;

internal sealed class ComplicationSpawnLevel
{
    private readonly SpiderFactory _spiderFactory;

    private float _timeSpawnSpider;
    private float _timeComplication;
    private float _minSpawnTime;
    private float _spawnTimeStepUp;
    private float _currentTime = 0;

    private bool _isMaxDifficult = false;

    public ComplicationSpawnLevel(SpiderFactory spiderFactory, float timeSpawn, float timeComplication, float minSpawnTime, float spawnTimeStepUp)
    {
        _spiderFactory = spiderFactory;
        _timeSpawnSpider = timeSpawn;
        _timeComplication = timeComplication;
        _minSpawnTime = minSpawnTime;
        _spawnTimeStepUp = spawnTimeStepUp;
    }

    public void Execute()
    {
        if (_isMaxDifficult)
        {
            return;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= _timeComplication)
        {
            _currentTime = 0;

            if (_timeSpawnSpider > _minSpawnTime)
            {
                _timeSpawnSpider -= _spawnTimeStepUp;
                _spiderFactory.GetNewTimeSpawn(_timeSpawnSpider);
            }
            else
            {
                _isMaxDifficult = true;
            }
        }
    }
}

