using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerLastPoint : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            
            if(direction.z>0)
            {
                GameManager.ins.crawler.isCrawlerChasing = false;
            }
            
            else if (direction.z < 0)
            {
                GameManager.ins.crawler.isCrawlerChasing = true;
            }
        }
    }
}
