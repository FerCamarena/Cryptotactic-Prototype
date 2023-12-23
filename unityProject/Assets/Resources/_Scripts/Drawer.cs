using UnityEngine;
public class Drawer : Item {
    public uint process;
    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.layer == 6) {
            if (this.process == 0) {
                if (collision.TryGetComponent<Mail>(out Mail mail)) {
                    if(mail.solved) {
                        Debug.Log("Solved");
                    } else {
                        Debug.Log("Unsolved");
                    }
                    Destroy(collision.gameObject, 0.1f);
                    this.process = 105;
                }
            } else {
                this.process--;
            }
        }
    }
}