using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();

    public float spawnerTime;
    float countTime;
    Vector3 spawnerPosition;
    void Update()
    {
        countTime += Time.deltaTime;
        spawnerPosition = transform.position;
        spawnerPosition.x = Random.Range(-3f, 3f);

        if (countTime >= spawnerTime)
        {
            CreatePlatfprm();
            CreatePlatfprm();
            CreatePlatfprm();
            countTime = 0;
        }
    }

    private void CreatePlatfprm()
    {
        int index = Random.Range(0, platforms.Count);
        int spikeNum = 0;
        if (index == 4)
        {
            spikeNum++;
        }
        if (spikeNum > 1)
        {
            spikeNum = 0;
            countTime = spawnerTime;
            return;
        }
        GameObject gameObject = Instantiate(platforms[index], spawnerPosition, Quaternion.identity);
        gameObject.transform.SetParent(this.gameObject.transform);
    }
}
