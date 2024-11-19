using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public int ingotsCollected = 0;
    [SerializeField] public int maxRequired = 10;
    [SerializeField] public TextMeshProUGUI ingotsText;
    [SerializeField] public GameObject[] foods = new GameObject[7];
    [SerializeField] public GameObject ingotPrefab; 
    
    private List<Vector3> spawnCoordinates = new List<Vector3>
    {
        new Vector3(-6.989474f, 11.19647f, -24.35629f),
        new Vector3(7.856296f, 11.19271f, -13.45396f),
        new Vector3(-2.751832f, 7.650045f, -12.85605f),
        new Vector3(-3.487231f, 7.652415f, -19.72379f),
        new Vector3(-3.359694f, 7.654273f, -25.10151f),
        new Vector3(3.676682f, 7.649971f, -12.64292f),
        new Vector3(4.750251f, 7.654142f, -24.72229f),
        new Vector3(4.606205f, 4.151205f, -13.12213f),
        new Vector3(4.748782f, 4.155219f, -24.74658f),
        new Vector3(4.357959f, 4.153376f, -19.40929f),
        new Vector3(-3.885405f, 4.155009f, -24.1388f),
        new Vector3(-4.334575f, 4.153254f, -19.05769f),
        new Vector3(-3.493446f, 4.151096f, -12.80838f),
        new Vector3(5.199851f, 0.6535461f, -13.14727f),
        new Vector3(4.950836f, 0.6557709f, -19.59271f),
        new Vector3(5.303323f, 0.6571018f, -23.44794f),
        new Vector3(1.413949f, 0.6637927f, -24.28834f),
        new Vector3(-3.24834f, 0.6534809f, -12.95861f),
        new Vector3(-3.592344f, 0.6556702f, -19.3023f),
        new Vector3(-3.848962f, 0.657256f, -23.89654f),
        new Vector3(2.081505f, 2.379609f, -24.83345f),
        new Vector3(-0.5133374f, 7.656547f, -13.14968f),
        new Vector3(1.818182f, 5.884153f, -24.7564f),
        new Vector3(0.5642124f, 0.6604223f, -14.52306f)
    };

    public void Start()
    {
        List<int> selectedIngotIndices = new List<int>();

        while (selectedIngotIndices.Count < 12)
        {
            int randomIndex = Random.Range(0, spawnCoordinates.Count);
            if (!selectedIngotIndices.Contains(randomIndex))
            {
                Vector3 ingotPosition = spawnCoordinates[randomIndex];
                Instantiate(ingotPrefab, ingotPosition, Quaternion.identity);
                selectedIngotIndices.Add(randomIndex);
            }
        }

        for (int i = 0; i < spawnCoordinates.Count; i++)
        {
            if (!selectedIngotIndices.Contains(i))
            {
                GameObject randomFood = foods[Random.Range(0, foods.Length)];
                Instantiate(randomFood, spawnCoordinates[i], Quaternion.identity);
            }
        }
    }

    public void UpdateText()
    {
        ingotsText.text = $"Ingots Collected: {ingotsCollected} / {maxRequired}";
    }

    public void CheckIfDied()
    {
        PlayerManager playermanager = player.GetComponent<PlayerManager>();
        if (playermanager.currentHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
    public void Update()
    {
        UpdateText();
        CheckIfDied();
    }
}
