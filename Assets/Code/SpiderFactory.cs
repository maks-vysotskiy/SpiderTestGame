using UnityEngine;

internal sealed class SpiderFactory
{
    private readonly SpiderPool _spiderPool;

    private float _timeSpawnSpider;
    private float _time = 0f;

    private float _startSpeedSpider;
    private float _startSpeedAnimationSpider;

    public SpiderFactory(SpiderPool spiderPool, float timeSpawnSpider, float startSpeedSpider, float startSpeedAnimationSpider)
    {
        _spiderPool = spiderPool;
        _timeSpawnSpider = timeSpawnSpider;
        _startSpeedSpider = startSpeedSpider;
        _startSpeedAnimationSpider = startSpeedAnimationSpider;
    }

    public Spider GetSpider(float startSpeedSpider, float startSpeedAnimationSpider)
    {
        var spider = _spiderPool.TakeSpider(startSpeedSpider, startSpeedAnimationSpider);
        return spider;
    }

    public void GetNewTimeSpawn(float timeSpawn)
    {
        _timeSpawnSpider = timeSpawn;
    }

    public void GetNewSpeedSpiderParamerts(float speedSpider, float speedAnimationSpider)
    {
        _startSpeedSpider = speedSpider;
        _startSpeedAnimationSpider = speedAnimationSpider;
    }

    public void Execute()
    {
        _time += Time.deltaTime;

        if (_time >= _timeSpawnSpider)
        {
            GetSpider(_startSpeedSpider, _startSpeedAnimationSpider);
            _time = 0;
        }
    }


}

