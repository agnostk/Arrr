using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyCannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform targetTransform;

    [Range(0f, 90f)] public float accuracy;

    public float fireRate;
    public float force;


    void Awake()
    {
        GameManager.Instance.onStartGame += () => StartCoroutine(AutoShoot());
    }

    void Start()
    {
        float startRotation = transform.eulerAngles.y + (90f * ((accuracy - 90f) / 90f));
        float targetRotation = transform.eulerAngles.y - (90f * ((accuracy - 90f) / 90f));

        transform.rotation = Quaternion.Euler(35f, startRotation, 0f);

        Aim(startRotation, targetRotation);
    }

    public void Aim(float startRotation, float targetRotation)
    {
        float duration = Random.Range(0.3f, 1f);
        transform.DORotate(Vector3.right * 35f + Vector3.up * targetRotation, duration).OnComplete(() => Aim(targetRotation, startRotation));
    }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            if (!GameManager.Instance.gameOver)
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        GameObject cannonball = Instantiate(cannonballPrefab, transform.position, Quaternion.identity);

        float yNoise = Random.Range(-5f, 5f);

        Vector3 forceDir = transform.forward * force + Vector3.up * yNoise;

        cannonball.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);
    }
}
