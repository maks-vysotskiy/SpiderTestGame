using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SpiderFactory
{
    private readonly SpiderPool _spiderPool;
    private readonly float _timeSpawnSpider;

    private float _time = 0f;

    public SpiderFactory(SpiderPool spiderPool, float timeSpawnSpider)
    {
        _spiderPool = spiderPool;
        _timeSpawnSpider = timeSpawnSpider;
    }

    public Spider GetSpider()
    {
        var spider = _spiderPool.TakeSpider();
        return spider;
    }

    public void Execute()
    {
        _time += Time.deltaTime;
        if(_time>=_timeSpawnSpider)
        {
            GetSpider();
            _time = 0;
        }
    }


}

