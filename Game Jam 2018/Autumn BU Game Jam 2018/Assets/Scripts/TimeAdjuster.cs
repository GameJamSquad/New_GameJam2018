using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAdjuster : MonoBehaviour
{

    public MeshRenderer mRenderer;
    public MeshCollider mCollider;
    public GameManager gManager;

    public float adjustAmount;
    public float cooldownTime;

    public bool isSlowDown = false;

	void Start ()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
	}

    private void FixedUpdate()
    {
        transform.Rotate((Vector3.up * 32) * Time.deltaTime);
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        mRenderer.enabled = true;
        mCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!gManager.speedupOn || !gManager.slowdownOn)
            {
                gManager.AdjustTimeSpeed(adjustAmount);
                mRenderer.enabled = false;
                mCollider.enabled = false;

                if (isSlowDown)
                {
                    gManager.slowdownOn = true;
                }
                else
                {
                    gManager.speedupOn = true;
                }

                StartCoroutine(StartCooldown());
            }
        }
    }
}
