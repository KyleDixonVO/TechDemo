using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteor;
    private Vector3 randomizer;
    public float meteorDelay;
    public float lastDrop;
    // Start is called before the first frame update
    void Start()
    {
        lastDrop = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastDrop + meteorDelay)
        {
            randomizer = new Vector3(this.transform.position.x + Random.Range(-5.0f, 5.0f), this.gameObject.transform.position.y, this.transform.position.z + Random.Range(-5.0f, 5.0f));
            Instantiate(meteor, randomizer, Quaternion.identity);
            lastDrop = Time.time;
            meteorDelay = Random.Range(0.5f, 5.0f);
        }
    }
}
