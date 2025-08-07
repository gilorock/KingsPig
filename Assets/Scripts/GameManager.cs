using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance;

    [Header("Player Settings")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerRespawnPoint;
    [SerializeField] private float respawnPlayerDelay;
    [SerializeField] private PlayerController playerController;
    
    public PlayerController PlayerController => playerController;

    [Header("Respawn Settings")]
    public bool hasCheckPointActive;
    public Vector3 CheckPointRespawnPosition;




    [Header("Diamond Manager")]
    [SerializeField] private int diamondCollected;
    [SerializeField] private bool diamondHaveRandomLook;
    
    public int DiamondCollected => diamondCollected;
    private void Awake()
    {
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);
        
    }

    public void RespawnPlayer()
    {
        if (hasCheckPointActive) playerRespawnPoint.position = CheckPointRespawnPosition;
        StartCoroutine(RespawnPlayerCorutine());


    }
    

    IEnumerator RespawnPlayerCorutine()
    {
       yield return new WaitForSeconds(respawnPlayerDelay);
       GameObject newPlayer = Instantiate(playerPrefab, playerRespawnPoint.position, Quaternion.identity);
        newPlayer.name = "Player";
       playerController = newPlayer.GetComponent<PlayerController>();

    }
        

    public void AddDiamond() => diamondCollected++;
    public bool DiamondHaveRandomLook() => diamondHaveRandomLook;    
    
}
