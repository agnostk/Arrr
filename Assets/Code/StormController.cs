using UnityEngine;
using DG.Tweening;

public class StormController : MonoBehaviour
{
    public GameObject stormParticles;
    public Light directionalLight;
    public Color stormColor = new Color(114, 116, 123);
    private Color defaultColor;

    void Awake()
    {
        GameManager.Instance.onStartStorm += StartStorm;
        GameManager.Instance.onStopStorm += StopStorm;
    }

    void Start()
    {
        defaultColor = directionalLight.color;
    }

    public void StartStorm()
    {
        directionalLight.DOColor(stormColor, 2.5f);
        stormParticles.SetActive(true);
    }
    public void StopStorm()
    {
        directionalLight.DOColor(defaultColor, 2.5f);
        stormParticles.SetActive(false);
    }
}
