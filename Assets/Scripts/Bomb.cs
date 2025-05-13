using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bomb : MonoBehaviour
{
    [SerializeField] int gridOffset;
    [SerializeField] int spawnHeight;

    [Header("Eplotion Cast")]
    [SerializeField] float sphereCastRad;
    [SerializeField] LayerMask layerMask;

    [Header ("Bomb Stats")]
    [SerializeField] int range;
    [SerializeField] float explotionTimer;
    float spawnTime;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.z);

        int divMain = (int)MathF.Floor(MathF.Abs(spawnPos.x / gridOffset));
        float sobras = spawnPos.x % gridOffset;
        if (MathF.Abs(sobras) > gridOffset / 2) divMain++;
        spawnPos.x = divMain * gridOffset;

        divMain = (int)MathF.Floor(MathF.Abs(spawnPos.y / gridOffset));
        sobras = spawnPos.y % gridOffset;
        if(MathF.Abs(sobras) > gridOffset/2) divMain++;
        spawnPos.y = divMain * -gridOffset;

        transform.position = new Vector3(spawnPos.x, spawnHeight, spawnPos.y);
    }

    void Update ()
    {
        if(Time.time - spawnTime >= explotionTimer)
        {
            animator.SetTrigger("Explode");
            spawnTime = Time.time;
        }
    }

    public void Explode() 
    {
        ExplodeInDirection(Vector3.right);
        ExplodeInDirection(Vector3.left);
        ExplodeInDirection(Vector3.forward);
        ExplodeInDirection(Vector3.back);
    }

    void ExplodeInDirection(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastRad, range, layerMask);
        Array.Sort(hits,(a,b) => a.distance.CompareTo(b.distance));
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Unbreakable") break;
                hit.transform.gameObject.SetActive(false);
                if (hit.transform.tag == "Breakable") break;
            }
        
        }
    }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
    
    public void SetBombRange(int range) 
    { 
        this.range = range;
    }
}
    

