using System.Collections;
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

    private Transform _rootPool;

    public SpiderPool(int capasity, List<Transform> spawnPoints)
    {
        _spiderPool = new HashSet<Spider>();
        _capasityPool = capasity;
        _spawnPoints = spawnPoints;

        if (!_rootPool)
        {
            _rootPool = new GameObject(SpiderPoolName).transform;
        }
        CreatePool(_capasityPool);
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
        for(int i=0; i<capasity; i++)
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

    public Spider TakeSpider()
    {
        var spider = _spiderPool.FirstOrDefault(s => !s.gameObject.activeSelf);
        spider.GetPool(this);
        return ActiveSpider(spider);
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

        return spider;
    }

}

