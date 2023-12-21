using UnityEngine;

public class Repeler_Spt : MonoBehaviour {
    public float force = 5f;
    private Rigidbody rb;

    private void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.CompareTag("Item")) {
            Vector3 direction = this.transform.position - collider.transform.position;
            float currentDistance = direction.magnitude;
            //if(currentDistance < this.distance) {
            this.rb.velocity += (1.0f / (Random.Range(-0.5f, 0.5f) + currentDistance * currentDistance) *
                                ((Vector3.forward * Random.Range(-0.1f, 0.1f) + Vector3.right *
                                Random.Range(-0.1f, 0.1f)) + direction) * this.force);
            //}
        }
    }
}