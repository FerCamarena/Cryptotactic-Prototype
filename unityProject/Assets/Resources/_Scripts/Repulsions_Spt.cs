using UnityEngine;

public class Repulsions_Spt : MonoBehaviour {
    public float force = 5f;
    public float distance = 10f;
    private Rigidbody rb;

    private void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.CompareTag("Item")) {
            Vector3 direction = this.transform.position - collider.transform.position;
            float currentDistance = direction.magnitude;
            if(currentDistance < this.distance) {
                this.rb.velocity += 1.0f / (Random.Range(-0.1f, 0.1f) + currentDistance) *
                                    ((Vector3.forward * Random.Range(-0.01f, 0.01f) + Vector3.right *
                                    Random.Range(-0.01f, 0.01f)) + direction) * this.force;
            }
        }
    }
}