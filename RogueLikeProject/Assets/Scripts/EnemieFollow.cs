using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Transform target;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        target = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
   
}

