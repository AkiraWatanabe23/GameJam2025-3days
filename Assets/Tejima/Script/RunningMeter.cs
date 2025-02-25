using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningMeter : MonoBehaviour
{
    [SerializeField] Text text;
    GameManager gameManager;
    float meter = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        meter = gameManager.GetDistance();
        text.text = $"{meter}m";
    }
}
