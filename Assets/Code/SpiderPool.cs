using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

internal sealed class SpiderPool
{
    private const string SpiderPoolName = "SPIDER_POOL";
    private const string BlackSpider = "spider_black";
    private const string GreenSpider = "spider_green";
    private const string BrownSpider = "spider_brown";

    private readonly HashSet<Spider> _spiderPool;
    private readonly int _capasityPool;
    private readonly List<Transform> _spawnPoints;

    private PointManager _pointManager;

    private Transform _rootPool;

    public int CountSpiderOnScene;

    public event Action OnDeactivate = delegate { };


    public SpiderPool(int capasity, List<Transform> spawnPoints, PointManager pointManager)
    {
        _pointManager = pointManager;
        _spiderPool = new HashSet<Spider>();
        _capasityPool = capasity;
        _spawnPoints = spawnPoints;

        if (!_rootPool)
        {
            _rootPool = new GameObject(SpiderPoolName).transform;
        }
        CountSpiderOnScene = 0;

        CreatePool(_capasityPool);
    }

    public void DeactivatePoolEntity()
    {
        OnDeactivate();
    }

    private string GetRandomSpider()
    {
        var random = Random.Range(1, 4);

        if (random == 1)
        {
            return BlackSpider;
        }
        else if (random == 2)
        {
            return GreenSpider;
        }
        else
        {
            return BrownSpider;
        }
    }

    private void CreatePool(int capasity)
    {
        for (int i = 0; i < capasity; i++)
        {
            var spider = Resources.Load<Spider>(GetRandomSpider());
            if (spider)
            {
                var instantiate = Object.Instantiate(spider);
                ReturnToPool(instantiate);
                _spiderPool.Add(instantiate);
            }
            else
            {
                Debug.LogError("I have not this type of spider");
            }
        }
    }

    public Spider TakeSpider(float speedSpider, float speedAnimationSpider)
    {
        var spider = _spiderPool.FirstOrDefault(s => !s.gameObject.activeSelf);
        spider.GetPool(this);
        spider.GetPointManager(_pointManager);
        spider.Speed = speedSpider;
        spider.AnimationSpeed = speedAnimationSpider;
        CountSpiderOnScene++;
        return ActiveSpider(spider);
    }

    public void CalculateSpiderOnSceneAfterDeathSpider()
    {
        CountSpiderOnScene--;
    }

    public void ReturnToPool(Spider spider)
    {
        var transform = spider.gameObject.transform;
        transform.localPosition = Vector3.zero;
        transform.gameObject.SetActive(false);
        transform.SetParent(_rootPool);
    }

    private Spider ActiveSpider(Spider spider)
    {
        var randSpawnPoint = Random.Range(0, _spawnPoints.Count);
        var transform = spider.gameObject.transform;
        transform.localPosition = _spawnPoints[randSpawnPoint].position;
        transform.localRotation = _spawnPoints[randSpawnPoint].rotation;
        transform.gameObject.SetActive(true);
        transform.SetParent(null);
        spider.ActivateSpiderSettings();
        return spider;
    }
}

