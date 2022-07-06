using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


internal sealed class UIStartSceneController : MonoBehaviour
{
    [SerializeField] private Text _recordText;
    [SerializeField] private GameObject _panelButtons;
    [SerializeField] private GameObject _panelRecord;
    [SerializeField] private GameObject _panelAuthors;

    private int _record;

    private void Start()
    {
        _panelButtons.SetActive(true);
        _panelRecord.SetActive(false);
        _panelAuthors.SetActive(false);

        _record = PlayerPrefs.GetInt("record");
        _recordText.text = _record.ToString();
    }

    public void BtnAuthors()
    {
        _panelButtons.SetActive(false);
        _panelRecord.SetActive(false);
        _panelAuthors.SetActive(true);
    }
    public void BtnBackFromRecordPanel()
    {
        _panelButtons.SetActive(true);
        _panelRecord.SetActive(false);
        _panelAuthors.SetActive(false);
    }

    public void BtnBackFromAuthorPanel()
    {
        _panelButtons.SetActive(true);
        _panelRecord.SetActive(false);
        _panelAuthors.SetActive(false);
    }
    public void BtnRecord()
    {
        _panelButtons.SetActive(false);
        _panelRecord.SetActive(true);
        _panelAuthors.SetActive(false);
    }

    public void BtnNewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BtnExit()
    {
        Application.Quit();
    }
}

