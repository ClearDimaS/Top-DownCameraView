using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    float speed;
    public void Init(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public static Action<GameObject> OnBulletOutOfBounds;

    private void FixedUpdate()
    {
        transform.position += (-transform.forward * speed * Time.deltaTime);

        if (ScreenViewManager.instance.IsInsideBounds(transform.position, 0.2f) == false)
            OnBulletOutOfBounds(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBulletOutOfBounds(gameObject);
    }

}
