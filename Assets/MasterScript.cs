using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{

    public ArrayList boids = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = Resources.Load("Boid") as GameObject;
        for (int i = 0; i < 100; i++)
        {
            _ = boids.Add(Instantiate(prefab));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < boids.Count; i++)
        {
            GameObject temp = boids[i] as GameObject;
            BoidBehavior cs = temp.GetComponent<BoidBehavior>();
            cs.update(boids);
        }
    }
}
