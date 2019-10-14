using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteSpawn : MonoBehaviour
{
    public GameObject[] Salle;
    private int i;

    private void Start()
    {

        i = Random.Range(0, Salle.Length) ;
        Instantiate(Salle[i], gameObject.transform);
    }
}
