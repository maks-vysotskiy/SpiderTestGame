using UnityEngine;

internal sealed class PointManager
{
    private SpiderManager _spiderManager;

    private int _counts;

    public PointManager(SpiderManager spiderManager)
    {
        _spiderManager = spiderManager;
    }

    public void CalculatePoints(int point)
    {
        if (_spiderManager.IsLose)
        {
            return;
        }
        _counts += point;

        var record = PlayerPrefs.GetInt("record");
        if (record < _counts)
        {
            PlayerPrefs.SetInt("record", _counts);
        }

        _spiderManager.GetPoints(_counts);
    }
}

