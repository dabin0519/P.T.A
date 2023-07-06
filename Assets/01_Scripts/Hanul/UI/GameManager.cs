using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private void Awake()
    {
        CreateUIManager();
    }
    void Start()
    {
        
    }

    private void CreateUIManager()
    {
        GameObject obj = new GameObject("UIManager");
        UIManager.Instance = obj.AddComponent<UIManager>();

        obj.transform.parent = transform;

        Transform canvasTrm = FindAnyObjectByType<Canvas>().transform;
        UIManager.Instance.Init(canvasTrm);
    }
}
