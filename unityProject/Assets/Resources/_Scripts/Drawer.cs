using UnityEngine;
public class Drawer : Item {
    public uint process;
    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.layer == this.gameObject.layer) {
            Vector3 direction = this.transform.position - collider.transform.position;
            float currentDistance = direction.magnitude;
            this.rb.velocity += (this.force / (Random.Range(-0.5f, 0.5f) + currentDistance * currentDistance) *
                                ((Vector3.forward * Random.Range(-0.1f, 0.1f) + Vector3.right *
                                Random.Range(-0.1f, 0.1f)) + direction));
        } else if (collider.gameObject.layer == 6) {
            /*
            if (this.process == 0) {
                if (collider.TryGetComponent<Mail>(out Mail mail)) {
                    if(mail.solved) {
                        Debug.Log("Solved");
                    } else {
                        Debug.Log("Unsolved");
                    }
                    Destroy(collider.gameObject, 0.1f);
                    this.process = 200;
                }
            } else {
                this.process--;
            }
            */
        }
    }
}