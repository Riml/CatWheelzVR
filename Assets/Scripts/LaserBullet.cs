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
        Destroy(this.gameObject);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Rat")  
            Destroy(gameObject);
    }
}
