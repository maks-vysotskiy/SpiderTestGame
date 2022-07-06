using System.Collections.Generic;
using UnityEngine;

internal sealed class SpiderManager : MonoBehaviour
{
    [Header("General game Settings")]
    [SerializeField] private int _boosterKillSpidersCount;
    [Space]

    [Header("Spider Pool Settings")]
    [SerializeField] private int _countSpidersOnLevelForLoseGame = 10;
    [Space]

    [Header("Spider Pool Settings")]
    [SerializeField] private int _capasity = 30;
    [SerializeField] private List<Transform> _spawnPoints;
    [Space]

    [Header("Spider Spawn Settings")]
    [SerializeField] private float _timeSpawnSpider;
    [SerializeField] private float _timeComplicationLevel;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _spawnTimeStepUp;
    [Space]

    [Header("Spider Speed Settings")]
    [SerializeField] private float _timeCompilationSpeedSpider;
    [SerializeField] private float _startSpeedSpeedSpider;
    [SerializeField] private float _startAnimationSpeedSpider;
    [SerializeField] private float _maxSpeedSpider;
    [SerializeField] private float _speedStepUpSpider;
    [Space]

    [Header("Spider Speed Settings")]
    [SerializeField] private UIGameController _UIgameController;
    [Space]

    private SpiderPool _spiderPool;
    private SpiderFactory _spiderFactory;
    private ComplicationSpawnLevel _complicationLevel;
    private ComplicationSpeedSpider _complicationSpeedSpider;
    private PointManager _pointManager;

    private int _points = 0;

    public bool IsLose;


    private void Start()
    {
        _pointManager = new PointManager(this);
        _spiderPool = new SpiderPool(_capasity, _spawnPoints, _pointManager);
        _spiderFactory = new SpiderFactory(_spiderPool, _timeSpawnSpider, _startSpeedSpeedSpider, _startAnimationSpeedSpider);
        _complicationLevel = new ComplicationSpawnLevel(_spiderFactory, _timeSpawnSpider, _timeComplicationLevel, _minSpawnTime, _spawnTimeStepUp);
        _complicationSpeedSpider = new ComplicationSpeedSpider(_spiderFactory, _timeCompilationSpeedSpider, _startSpeedSpeedSpider, _startAnimationSpeedSpider, _maxSpeedSpider, _speedStepUpSpider);
         
        IsLose = false;

        _UIgameController.GetSpiderPool(_spiderPool);
        _UIgameController.GetCountBoosterKillSpider(_boosterKillSpidersCount);
    }

    private void Update()
    {
        if(IsLose)
        {
            return;
        }
        GameOver();
        _spiderFactory.Execute();
        _complicationLevel.Execute();
        _complicationSpeedSpider.Execute();

    }

    private void GameOver()
    {
        var countSpider =_spiderPool.CountSpiderOnScene;
        if (countSpider> _countSpidersOnLevelForLoseGame)
        {
            IsLose = true;
            _spiderPool.DeactivatePoolEntity();
            _UIgameController.GameOver();
        }
    }

    public void GetPoints(int points)
    {
        _points = points;
        _UIgameController.GetPoints(_points);
    }
}

