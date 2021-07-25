using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ShipController : MonoBehaviour
{
    public float startingBalanceSpeed;
    private float balanceSpeed;

    public float startingHorizontalBalance;
    private float horizontalBalance;

    public float startingVerticalBalance;
    private float verticalBalance;

    public float stormSpeedMultiplier = 1.5f;
    public float stormBalanceMultiplier = 2.5f;

    private float t;

    void Awake()
    {
        GameManager.Instance.onStartStorm += () => StartStorm();
    }

    void Start()
    {
        balanceSpeed = startingBalanceSpeed;
        horizontalBalance = startingHorizontalBalance;
        verticalBalance = startingVerticalBalance;
    }

    public void Update()
    {
        t += Time.deltaTime * balanceSpeed;
        float sinT = Mathf.Sin(t);

        transform.rotation = Quaternion.Euler(sinT * horizontalBalance, 0f, sinT * verticalBalance);
    }
    public void StartStorm()
    {
        float targetSpeed = startingBalanceSpeed * stormSpeedMultiplier;
        float targetHorizontalBalance = startingHorizontalBalance * stormBalanceMultiplier;
        float targetVerticalBalance = startingVerticalBalance * stormBalanceMultiplier;
        float stormDuration = Random.Range(GameManager.Instance.minStormDuration, GameManager.Instance.maxStormDuration);

        Debug.Log($"Starting storm with duration of {stormDuration} seconds!");

        Sequence stormSequence = DOTween.Sequence();
        stormSequence
            .Append(DOTween.To(() => balanceSpeed, s => balanceSpeed = s, targetSpeed, stormDuration * 0.25f))
            .Insert(0, DOTween.To(() => horizontalBalance, h => horizontalBalance = h, targetHorizontalBalance, stormDuration * 0.25f))
            .Insert(0, DOTween.To(() => verticalBalance, v => verticalBalance = v, targetVerticalBalance, stormDuration * 0.25f))
            .AppendInterval(stormDuration * 0.25f)
            .SetLoops(2, LoopType.Yoyo)
            .Play()
            .OnComplete(() => GameManager.Instance.StopStorm());
    }
}
