using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform gameReadyTransform;
    public Transform gameStartedTransform;

    public float cameraSpeed = 4.5f;

    void Awake()
    {
        GameManager.Instance.onStartGame += () => DoMove(gameStartedTransform);
    }

    void Start()
    {
        DoMove(gameReadyTransform);
    }

    public void DoMove(Transform target)
    {
        float time = Vector3.Distance(transform.position, target.position) / cameraSpeed;
        transform.DOMove(target.position, time);
        transform.DORotate(target.rotation.eulerAngles, time);
    }
}
