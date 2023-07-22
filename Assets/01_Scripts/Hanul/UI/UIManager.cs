using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    TextMeshProUGUI _settings;
    Image _panel;
    Image _backPanel;
    Image _exitPanel;
    Button _xButton;
    Button _retryButton;
    Button _exitButton;
    Button _returnButton;
    Button _exitGameButton;
    bool _Input = false;

    public void Init(Transform canvasTrm)
    {
        _exitButton = canvasTrm.Find("Panel/ExitButton").GetComponent<Button>();
        _xButton = canvasTrm.Find("Panel/XButton").GetComponent<Button>();
        _retryButton = canvasTrm.Find("Panel/RetryButton").GetComponent<Button>();
        _panel = canvasTrm.Find("Panel").GetComponent<Image>();
        _backPanel = canvasTrm.Find("BackPanel").GetComponent<Image>();
        _exitPanel = canvasTrm.Find("ExitPanel").GetComponent<Image>();
        _returnButton = canvasTrm.Find("ExitPanel/ReturnButton").GetComponent<Button>();
        _exitGameButton = canvasTrm.Find("ExitPanel/ExitButton").GetComponent<Button>();

        _panel.gameObject.SetActive(false);
        _exitPanel.gameObject.SetActive(false);
        _backPanel.gameObject.SetActive(false);
    }

    void Start()
    {
        _exitButton.onClick.AddListener(ExitButton);
        _xButton.onClick.AddListener(ExitSettingButton);
        _retryButton.onClick.AddListener(RetryButton);
        _returnButton.onClick.AddListener(ReturnButton);
        _exitGameButton.onClick.AddListener(ExitGameButton);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_Input)
        {
            _panel.gameObject.SetActive(true);
            _Input = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _Input)
        {
            ExitSettingButton();
        }

    }

    void ExitSettingButton()
    {
        _panel.gameObject.SetActive(false);
        _exitPanel.gameObject.SetActive(false);
        _Input = false;
        Time.timeScale = 1;
    }

    void RetryButton()
    {
        SceneManager.LoadScene("Hanul");
        Time.timeScale = 1;
    }

    void ReturnButton()
    {
        _exitPanel.gameObject.SetActive(false);
        _panel.gameObject.SetActive(true);
    }

    void ExitButton()
    {
        _panel.gameObject.SetActive(false);
        _exitPanel.gameObject.SetActive(true);
    }

    void ExitGameButton()
    {
        Application.Quit();
    }
}
