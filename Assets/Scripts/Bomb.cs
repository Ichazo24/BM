using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bomb : MonoBehaviour
{
    [SerializeField] int gridOffSet;
    [SerializeField] int spawnHeight;

    [Header("Explotion Cast")]
    [SerializeField] float sphereCastRad;
    [SerializeField] LayerMask layerMask;

    [Header("Bomb stats")]
    [SerializeField] int range;
    [SerializeField] float explosionTimer;
    float spawnTime;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.z);
        //x
        int divMain = (int)MathF.Floor(MathF.Abs(spawnPos.x / gridOffSet));
        float sobras = spawnPos.x % gridOffSet;
        if (MathF.Abs(sobras) > gridOffSet / 2) divMain++;
        spawnPos.x = divMain * gridOffSet;
        // y (z)
        divMain = (int)MathF.Floor(MathF.Abs(spawnPos.y / gridOffSet));
        sobras = spawnPos.y % gridOffSet;
        if (MathF.Abs(sobras) > gridOffSet / 2) divMain++;
        spawnPos.y = divMain * -gridOffSet;

        transform.position = new Vector3(spawnPos.x, spawnHeight, spawnPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime >= explosionTimer)
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
        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Unbreakable") break;
                if (hit.transform.tag == "Player") 
                    hit.transform.GetComponent<MovementController>().DisablePlayer();
                else 
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
        this.range = range * gridOffSet;
    }
}
