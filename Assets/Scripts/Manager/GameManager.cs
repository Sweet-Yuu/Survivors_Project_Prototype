using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Tutorial Settings")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button startGameButton;
    private bool isTutorialActive = true;

    [Header("Time Score Settings")]
    [SerializeField] public float score = 60;
    public TextMeshProUGUI scoreText;

    [Header("Portal Settings")]
    public List<GameObject> portalPrefab = new List<GameObject>();
    public bool isPortalSpawned = false;
    public Transform playerTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            isTutorialActive = true;
            Time.timeScale = 0f;
        }
        else
        {
            isTutorialActive = false;
            Time.timeScale = 1f;
        }

        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(CloseTutorialAndStart);
        }
    }

    private void Update()
    {
        if (isTutorialActive) return;
        if (isPortalSpawned) return;
        if (Player.IsDead) return;

        if (score > 0)
        {
            score -= Time.deltaTime;
            scoreText.text = Mathf.RoundToInt(score).ToString();

            if (score <= 5)
            {
                scoreText.color = Color.red;
            }
        }
        else
        {
            scoreText.color = Color.red;
            scoreText.text = "0";
            OnTimeOut();
        }
    }

    void CloseTutorialAndStart()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        isTutorialActive = false;

        startGameButton.onClick.RemoveListener(CloseTutorialAndStart);
    }

    void OnTimeOut()
    {
        Player.Instance.IsInvincible(true);
        isPortalSpawned = true;
        ClearAllEnemies();
        SpawnPortals();
    }

    public void ClearAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    void SpawnPortals()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        if (portalPrefab == null || portalPrefab.Count == 0)
        {
            Debug.LogError("No portal prefabs assigned!");
            return;
        }

        List<GameObject> tempPortalList = new List<GameObject>(portalPrefab);

        for (int i = 0; i < 2; i++)
        {
            if (tempPortalList.Count == 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, tempPortalList.Count);
            GameObject portalToSpawn = tempPortalList[randomIndex];

            Vector3 randomViewportPos = new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), mainCam.nearClipPlane);
            Vector3 spawnPos = mainCam.ViewportToWorldPoint(randomViewportPos);
            spawnPos.z = 0;

            Instantiate(portalToSpawn, spawnPos, Quaternion.identity);

            tempPortalList.RemoveAt(randomIndex);
        }
    }
}