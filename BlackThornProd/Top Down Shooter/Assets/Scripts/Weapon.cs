﻿using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float _shotTime;


    // Update is called once per frame
    void Update()
    {
        // Point weapon at mouse pointer
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= _shotTime)
            {
                Quaternion bulletRotate = Quaternion.Euler(0f, 0f, -90f);
                Instantiate(projectile, shotPoint.position, transform.rotation * bulletRotate);
                _shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}