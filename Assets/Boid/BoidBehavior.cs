using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{

    private const float MAX_VEL = 12f;
    private const float DIST = 36f;
    private const float SENSE_RAD = 6f;
    private const float AVOID_WALL_RAD = 8f;
    private const float AVOID_BOIDS_RAD = 1f;
    private const float SENSE_ANG = 150;

    private const float DIR_SYNC_COEF   = .2f;
    private const float FLOCK_CENT_COEF = 0.3f;
    private const float COLL_AVOID_COEF = 5f;
    private const float SEPARATION_COEF = 0f;

    public Rigidbody body;
    public Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        body.transform.position = new Vector3(Random.Range(0, DIST), Random.Range(0, DIST), Random.Range(0, DIST));
        body.velocity = new Vector3(Random.Range(-MAX_VEL, MAX_VEL), Random.Range(-MAX_VEL, MAX_VEL), Random.Range(-MAX_VEL, MAX_VEL));
        tag = "boid";
    }

    private void FixedUpdate()
    {

        body.transform.forward = body.velocity;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("boid"))
        {
            Physics.IgnoreCollision(collision.collider, coll);
        }
    }

    public void update(ArrayList b)
    {

        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;

        for (int i = 0; i < b.Count; i++)
        {
            GameObject elt = b[i] as GameObject;
            BoidBehavior cs = elt.GetComponent<BoidBehavior>();
            Rigidbody eltBod = cs.body;
            if (!eltBod.Equals(body))
            {
                

                Vector3 pathTo = eltBod.position - body.position;
                float dist = pathTo.magnitude;
                float ang = Vector3.Angle(body.velocity, pathTo);

                /*if (dist <= AVOID_BOIDS_RAD && ang <= SENSE_ANG)
                {
                    alignment += syncDir(eltBod);

                    separation += seperation(pathTo, dist);

                }*/
                if (dist <= SENSE_RAD && ang <= SENSE_ANG)
                {
                    Debug.DrawLine(body.position, eltBod.position);

                    alignment += syncDir(eltBod);

                    cohesion += flockCentering(pathTo);

                }
                
            }
        }

        body.velocity += (alignment.normalized * DIR_SYNC_COEF) + (cohesion.normalized * FLOCK_CENT_COEF) + (separation * SEPARATION_COEF);

        if (collisionImpending())
        {
            Vector3 dir = findSafePath();
            body.velocity += COLL_AVOID_COEF * dir.normalized;
        }


        body.velocity = body.velocity.normalized * MAX_VEL;

        Debug.DrawLine(body.position + body.velocity.normalized, body.position + (2 * body.velocity.normalized), Color.blue);

    }
    private Vector3 syncDir(Rigidbody eltBod)
    {
        return eltBod.velocity.normalized;
    }

    private Vector3 flockCentering(Vector3 pathTo)
    {
        return pathTo.normalized;
    }

    private Vector3 seperation(Vector3 pathTo, float dist)
    {
        return Mathf.Pow((2 - (dist / AVOID_BOIDS_RAD)), 2) * pathTo.normalized * -1;
    }

    //Helper Methods

    private bool collisionImpending()
    {
        bool v = Physics.SphereCast(body.position, 0.2f, body.velocity, out _, AVOID_WALL_RAD);
        return v;
    }

    private Vector3 findSafePath()
    {
        Vector3[] paths = PathFinder.directions;

        for (int i = 0; i < paths.Length; i++)
        {
            

            Vector3 path = body.transform.TransformDirection(paths[i]);
            Ray ray = new Ray(body.position, path);

            if (!Physics.SphereCast(ray, 0.2f, AVOID_WALL_RAD))
            {
                Debug.DrawLine(body.position, body.position + path, Color.green);
                return path;
            }
            Debug.DrawLine(body.position, body.position + path, Color.red);
        }
        return body.velocity;

    }

}
 