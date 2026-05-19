using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public float score = 60;
    public GameObject portalPrefab;
    public Transform playerTransform;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    

    public bool isPortalSpawned = false;


    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //Cursor.visible = false;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Update()
    {
        if (score > 0)
        {
            score -= Time.deltaTime;
            scoreText.text = Mathf.RoundToInt(score).ToString();
        }
        else if (!isPortalSpawned)
        {
            SpawnPortal();
        }
        else if (Player.Instance.Health.CurrentHealth<=0)
        {
            GameOver();
        }
    }
    void SpawnPortal()
    {
        Camera mainCam = Camera.main;
        isPortalSpawned = true;
        
        if(mainCam != null)
        {
            Vector3 randomViewportPos = new Vector3(Random.Range(0.2f, 0.8f),  Random.Range(0.2f, 0.8f),mainCam.nearClipPlane);
            Vector3 spawnPos = mainCam.ViewportToWorldPoint(randomViewportPos);
            spawnPos.z = 0; // Ensure the portal spawns at the correct depth
            Instantiate(portalPrefab, spawnPos, Quaternion.identity);
            Debug.Log($"Portal spawned at: {spawnPos}");
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }
    }
}