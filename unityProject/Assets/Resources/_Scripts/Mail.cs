using UnityEngine;
public class Mail : Item {
    public bool solved;
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            this.solved ^= true;
        }
    }
}