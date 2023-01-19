using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerRespawn : MonoBehaviour
{
    public bool isReSpawn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isReSpawn = true;
        }
    }
}