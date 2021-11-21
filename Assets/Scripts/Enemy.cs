using UnityEngine;



public class Enemy: MonoBehaviour {
  Rigidbody rb;
  GameObject player;
  public float speed = 5.0f;


  void Awake() {
    rb = GetComponent<Rigidbody>();
    player = GameObject.Find("Player");
  }

  void Update() {
    rb.AddForce((player.transform.position - transform.position).normalized * speed * 0.1f);
  }

  void FixedUpdate() {
    if (transform.position.y < -20.0f) {
      SpawnManager.instance.DestroyEnemy(gameObject);
    }
  }
}
