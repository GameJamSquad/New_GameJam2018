using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedTank : MonoBehaviour
{



	void Start ()
    {
        StartCoroutine(DestroyTank());
	}

    IEnumerator DestroyTank()
    {
        yield return new WaitForSeconds(8f);
        Destroy(this.gameObject);
    }
}
