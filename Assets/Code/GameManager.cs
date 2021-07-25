using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;

    [Header("Battle Settings")]
    public float playerMaxHealth = 100f;
    public float playerHealth;
    public event Action onPlayerDamage;
    public float playerDamage = 5f;

    public float enemyMaxHealth = 100f;
    public float enemyHealth;
    public event Action onEnemyDamage;
    public float enemyAttackRate = 0.25f;
    public float enemyDamage = 2f;


    [Header("Storm Settings")]
    public bool isInStorm;
    [Range(0, 100f)] public float stormChance = 65f;
    public float minStormDuration = 30f;
    public float maxStormDuration = 50f;
    public event Action onStartStorm;
    public event Action onStopStorm;

    public bool gameStarted;
    public event Action onStartGame;

    public bool gameOver;
    public event Action onGameOver;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        playerHealth = playerMaxHealth;
        enemyHealth = enemyMaxHealth;

        onStartGame += () => StartCoroutine(UpdateStormChance());
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void StartGame()
    {
        gameStarted = true;
        onStartGame?.Invoke();
    }

    public void DoPlayerDamage()
    {
        playerHealth -= enemyDamage;
        if (enemyHealth <= 0f)
        {
            EndGame();
        }
        onPlayerDamage?.Invoke();
    }

    public void DoEnemyDamage()
    {
        enemyHealth -= playerDamage;
        if (enemyHealth <= 0f)
        {
            EndGame();
        }
        onEnemyDamage?.Invoke();
    }

    public void EndGame()
    {

    }

    #region Storm
    IEnumerator UpdateStormChance()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            float chance = UnityEngine.Random.Range(1f, 100f);
            if (chance <= stormChance)
            {
                if (!isInStorm)
                {
                    StartStorm();
                }
            }
        }
    }

    public void StartStorm()
    {
        isInStorm = true;
        onStartStorm?.Invoke();
    }

    public void StopStorm()
    {
        isInStorm = false;
        onStopStorm?.Invoke();
    }

    #endregion
}
