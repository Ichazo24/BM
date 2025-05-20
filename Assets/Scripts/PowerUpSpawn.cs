using UnityEngine;
using System.Collections.Generic;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> powerUpPrefs = new List<GameObject>();

    private void OnDisable()
    {
        int rand = Random.Range(0, powerUpPrefs.Count);
        Instantiate(powerUpPrefs[rand], transform.position, Quaternion.identity);
    }
}
