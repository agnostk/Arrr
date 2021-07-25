using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    public float currentTime = 0f;

    void Awake()
    {
        text = GetComponent<Text>();
        GameManager.Instance.onStartGame += StartTimer;
    }

    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            text.text = TimeSpan.FromSeconds(currentTime++).ToString(@"m\:ss");
        }
    }

    public void StartTimer()
    {
        StartCoroutine(Tick());

    }
}
