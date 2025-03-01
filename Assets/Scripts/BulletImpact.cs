using System.Collections;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Bullet hit");
    //     StartCoroutine(SelfDestroy());
    // }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("Bullet hit");
    //     StartCoroutine(SelfDestroy());
    // }

    IEnumerator SelfDestroy(){
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
