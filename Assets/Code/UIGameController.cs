using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


internal sealed class UIGameController : MonoBehaviour
{
    [SerializeField] private Text _textCountBoosterKillSpiders;
    [SerializeField] private Text _textCountPoints;
    [SerializeField] private GameObject _panelButtonExit;

    private SpiderPool _spiderPool;

    private int _countBoosterKillSpiders = 0;
    private int _points = 0;


    private void Start()
    {
       _textCountPoints.text = _points.ToString();
        _panelButtonExit.SetActive(false);
    }

    public void GetSpiderPool(SpiderPool spiderPool)
    {
        _spiderPool = spiderPool;
    }

    public void GetCountBoosterKillSpider(int count)
    {
        _countBoosterKillSpiders = count;
        _textCountBoosterKillSpiders.text = _countBoosterKillSpiders.ToString();
    }

    public void GameOver()
    {
        _panelButtonExit.SetActive(true);
    }

    public void GetPoints(int points)
    {
        _points = points;
        _textCountPoints.text = _points.ToString();
    }

    public void UseBusterKillSpiders()
    {
        if (_countBoosterKillSpiders > 0)
        {
            _spiderPool.DeactivatePoolEntity();
            _countBoosterKillSpiders--;
            _textCountBoosterKillSpiders.text = _countBoosterKillSpiders.ToString();
        }

    }
    public void BtnRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void BtnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BtnBack()
    {
        SceneManager.LoadScene(0);
    }
}

