using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class LayersManager : MonoBehaviour
{
    PlayerInputManager playerInputManager;

    [SerializeField] List<Transform> spawnPos;
    [SerializeField] List<Material> playerMaterial;

    private void Awake()
    {
        
        playerInputManager = GetComponent<PlayerInputManager>();
        
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.position = spawnPos[playerInputManager.playerCount-1].position;
        playerInput.gameObject.GetComponent<MeshRenderer>().material = playerMaterial[playerInputManager.playerCount-1];
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("Player has left");
        
    }
}
