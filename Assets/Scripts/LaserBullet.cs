using UnityEngine;
using System.Collections;

public class LaserBullet : MonoBehaviour {

    public float activeLaserTimeLenght = 0.5f;

    void Start()
    {
        StartCoroutine(SelfDestruction());
    }

    IEnumerator SelfDestruction() {
        yield return new WaitForSeconds(activeLaserTimeLenght);
        Destroy(gameObject);

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("shooting at" +collision.gameObject.name);
        if (collision.gameObject.tag=="Rat")  
            Destroy(gameObject);
    }
}
