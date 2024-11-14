using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    protected Vector2 direction;

    protected virtual void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
