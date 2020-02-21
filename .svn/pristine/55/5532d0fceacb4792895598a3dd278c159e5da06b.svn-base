using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position =
            new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, 0);

        if (transform.position.y > 5.4f)
            Destroy(gameObject);
    }
}
