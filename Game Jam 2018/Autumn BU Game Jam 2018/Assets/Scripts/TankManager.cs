using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    public GameManager gManager;

    public string horizontal = "Horizontal_P1";
    public string vertical = "Vertical_P1";
    public string fireGun = "Fire1_P1";
    public string turretRotation = "TurretRotation_P1";

    public string horizontalPS4 = "HorizontalPS4_P1";
    public string verticalPS4 = "HorizontalPS4_P1";
    public string fireGunPS4 = "HorizontalPS4_P1";
    public string turretRotationPS4 = "HorizontalPS4_P1";

    public MeshRenderer bodyRenderer, turretRenderer, trackRenderer;
    public MeshCollider bodyCol, turretCol;

    bool hasSpawnedDeadBody = false;
    public GameObject destroyedBody;

    public int playerNumber;
    public List<Material> pMaterials;

    //[HideInInspector]
    public int health = 5;

    public int maxHealth = 5;
    public int score = 0;

    public Rigidbody rb, turretRb;
    public float movementForce, rotationSpeed = 50f, turretRotationSpeed = 50f;

    public Transform turretPos;
    bool canShot = true;
    public Transform firePoint;
    public GameObject bullet;
    public float gunCooldown = 1f;

	void Start ()
    {
        gManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        health = maxHealth;

        horizontal = "Horizontal_P" + playerNumber;
        vertical = "Vertical_P" + playerNumber;
        fireGun = "Fire1_P" + playerNumber;
        turretRotation = "TurretRotation_P" + playerNumber;

        horizontalPS4 = "HorizontalPS4_P" + playerNumber;

        Material[] turretMats = turretRenderer.materials;
        turretMats[1] = pMaterials[playerNumber - 1];

        turretRenderer.materials = turretMats;
    }

	void Update ()
    {
        if(health <= 0)
        {
            ExplodeTank();
        }
	}

    void FixedUpdate()
    {
        if (!gManager.isGamePaused)
        { 
            InputManager();
        }
    }

    void ExplodeTank()
    {
        turretRenderer.enabled = false;
        bodyRenderer.enabled = false;
        trackRenderer.enabled = false;

        bodyCol.enabled = false;
        turretCol.enabled = false;

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        if (!hasSpawnedDeadBody)
        {
            Instantiate(destroyedBody, transform.position, transform.rotation);
            hasSpawnedDeadBody = true;
        }


        int respawnPoint = Random.Range(0, gManager.respawnPoints.Count);

        transform.position = gManager.respawnPoints[respawnPoint].position;

        yield return new WaitForSeconds(gManager.respawnTimer);

        turretRenderer.enabled = true;
        bodyRenderer.enabled = true;
        trackRenderer.enabled = true;

        bodyCol.enabled = true;
        turretCol.enabled = true;

        health = maxHealth;
        hasSpawnedDeadBody = false;
    }

    public void AdjustScore()
    {
        score++;
        gManager.CheckScores();
    }

    void FireGun()
    {
        if(canShot == true)
        {
            GameObject curBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

            curBullet.GetComponent<Bullet>().tSource = this;

            StartCoroutine(GunCooldown());
            canShot = false;
        }
    }

    IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(gunCooldown);
        canShot = true;
    }

    void UpdateTurretPosition()
    {
        turretRb.transform.position = turretPos.position;
    }

    void InputManager()
    {
        if (health >= 1)
        {

            UpdateTurretPosition();

            if (Input.GetAxis(vertical) > 0.1f)
            {
                rb.AddForce(transform.forward * movementForce);
            }
            if (Input.GetAxis(vertical) < -0.1f)
            {
                rb.AddForce(-transform.forward * (movementForce / 2f));
            }
            if (Input.GetAxis(horizontal) < -0.1f)
            {
                Vector3 m_EulerAngleVelocity = new Vector3(0, -rotationSpeed, 0);
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (Input.GetAxis(horizontal) > 0.1f)
            {
                Vector3 m_EulerAngleVelocity = new Vector3(0, rotationSpeed, 0);
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            if (Input.GetAxis(turretRotation) > 0.1f)
            {
                Vector3 m_EulerAngleVelocity = new Vector3(0, turretRotationSpeed, 0);
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
                turretRb.MoveRotation(turretRb.rotation * deltaRotation);
            }
            if (Input.GetAxis(turretRotation) < -0.1f)
            {
                Vector3 m_EulerAngleVelocity = new Vector3(0, -turretRotationSpeed, 0);
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
                turretRb.MoveRotation(turretRb.rotation * deltaRotation);
            }

            if (Input.GetAxis(fireGun) > 0.1f)
            {
                FireGun();
            }
        }
    }
}
