using System;
using UnityEngine;

internal sealed class ComplicationSpeedSpider
{
    private readonly SpiderFactory _spiderFactory;

    private float _timeComplication;
    private float _speedSpider;
    private float _speedStepUp;
    private float _speedAnimationSpider;
    private float _maxSpeedSpider;
    private float _currentTime = 0;

    private bool _isMaxSpeed = false;

    public ComplicationSpeedSpider(SpiderFactory spiderFactory, float timeComplication, float startSpeedSpider,
        float startAnimationSpider, float maxSpeedSpider, float speedStepUp)
    {
        _spiderFactory = spiderFactory;
        _timeComplication = timeComplication;
        _speedSpider = startSpeedSpider;
        _speedAnimationSpider = startAnimationSpider;
        _maxSpeedSpider = maxSpeedSpider;
        _speedStepUp = speedStepUp;
    }

    public void Execute()
    {
        if (_isMaxSpeed)
        {
            return;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= _timeComplication)
        {
            _currentTime = 0;

            if (_speedSpider < _maxSpeedSpider)
            {
                _speedSpider += _speedStepUp;
                _speedAnimationSpider += _speedStepUp / 20;
                _spiderFactory.GetNewSpeedSpiderParamerts(_speedSpider, _speedAnimationSpider);
            }
            else
            {
                _isMaxSpeed = true;
            }
        }
    }
}

