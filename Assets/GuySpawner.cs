using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuySpawner : MonoBehaviour
{
    [SerializeField]
    Transform leftBound;
    [SerializeField]
    Transform rightBound;

    [SerializeField]
    float minDelay;
    [SerializeField]
    float maxDelay;

    [SerializeField]
    float startDelay;

    private float delay;
    private float timerTime;

    private void Start()
    {
        delay = startDelay;
    }
    private void Update()
    {
        if(timerTime < delay)
        {
            timerTime += Time.deltaTime;
        }
        else
        {
            timerTime = 0;
            print("spawn");
            SpawnGuy(new Vector3(Random.Range(leftBound.position.x,leftBound.position.y),-1f,rightBound.position.z));
            
            delay = Random.Range(minDelay, maxDelay);
        }
    }
    private void SpawnGuy(Vector3 position)
    {
        Pool.instance.SpawnGuy(position);
    }

}
