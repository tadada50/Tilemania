using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForPickup = 100;
    // CircleCollider2D myCollider;
    GameSession gameSession;
    bool wasCollected = false;
    void Start()
    {
        // myCollider = GetComponent<CircleCollider2D>();
        gameSession = FindFirstObjectByType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasCollected){
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            gameSession.AddToScore(pointsForPickup);
            Destroy(gameObject);
        }
    }
}
