using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerBombManager : MonoBehaviour
{
    InputManager InputManager;
    public GameObject bombprefab;
    [SerializeField] Transform bombPoolParent;

    [Header("Bomb Stats")]
    [SerializeField] int maxBombs;
    [SerializeField] int bombRange;

    List<GameObject> bombsPool = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        for (int i = 0; i < maxBombs; i++)
        {
            GameObject bomb = Instantiate(bombprefab, bombPoolParent);
            bomb.SetActive(false);
            bombsPool.Add(bomb);
        }
    }

    private void OnEnable()
    {
    InputManager.onBombP.AddListener(DeployBomb);
    }

private void OnDisable()
{
        maxBombs = 1;
        bombRange = 1;
    InputManager.onBombP.RemoveListener(DeployBomb);
}

private void DeployBomb()
{
        foreach(GameObject bomb in bombsPool)
        {
            if (bomb.activeSelf) continue;
            bomb.transform.position = transform.position;
            bomb.GetComponent<Bomb>().SetBombRange(bombRange);
            bomb.SetActive(true);
            return;
        }
}

    public void AddExtraBomb()
    {
        maxBombs++;
        GameObject bomb = Instantiate(bombprefab, bombPoolParent);
        bomb.SetActive(false);
        bombsPool.Add(bomb);
    }

    public void AddExtraRange()
    {
        bombRange++;
    }
    
}
