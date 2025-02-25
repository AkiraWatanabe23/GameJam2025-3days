using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] Text text;
    GameManager gameManager;
    float timer = 0.0f;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        timer = gameManager.GetTime();
        text.text = $"{timer}•b";
    }
}
