using UnityEngine;

public class RepellingObjects : MonoBehaviour {
    public float repelForce = .5f;
    public float distance = 9.9f;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.CompareTag("Element")) {
            Vector3 direction = this.transform.position - collider.transform.position;
            float currentDistance = direction.magnitude;
            if(currentDistance < distance) {
                rb.velocity += 1f / (0.01f + currentDistance) * ((Vector3.forward * Random.Range(0.0f, .01f) + Vector3.right * Random.Range(0.0f, .01f)) + direction) * repelForce;
            }
        }

    }
}