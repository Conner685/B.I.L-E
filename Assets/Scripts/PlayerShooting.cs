using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire = true;
    private float fireRateTimer;
    public float fireRate;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            fireRateTimer += Time.deltaTime;
            if(fireRateTimer > fireRate)
            {
                canFire = true;
                fireRateTimer = 0;
            }
        }

        if (Input.GetButton("Fire1") && canFire)
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);

            canFire = false;
        }
    }
}
