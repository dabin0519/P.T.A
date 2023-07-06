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
    Image _backpanel;
    Button _xButton;
    Button _retryButton;
    Button _exitButton;
    bool _Input = false;

    public void Init(Transform canvasTrm)
    {
        _exitButton = canvasTrm.Find("Panel/ExitButton").GetComponent<Button>();
        _xButton = canvasTrm.Find("Panel/XButton").GetComponent<Button>();
        _retryButton = canvasTrm.Find("Panel/RetryButton").GetComponent<Button>();
        _panel = canvasTrm.Find("Panel").GetComponent<Image>();
        _backpanel = canvasTrm.Find("BackPanel").GetComponent<Image>();

        _panel.gameObject.SetActive(false);
        _backpanel.gameObject.SetActive(false);
    }

    void Start()
    {
        _exitButton.onClick.AddListener(ExitGameButton);
        _xButton.onClick.AddListener(ExitSettingButton);
        _retryButton.onClick.AddListener(RetryButton);
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
        _Input = false;
        Time.timeScale = 1;
    }

    void RetryButton()
    {
        SceneManager.LoadScene("Hanul");
        Time.timeScale = 1;
    }

    void ExitGameButton()
    {
        Application.Quit();
    }
}
