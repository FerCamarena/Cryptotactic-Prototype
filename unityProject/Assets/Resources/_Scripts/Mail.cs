using UnityEngine;
public class Mail : Item {
    public bool solved;
    public override void SolveItem() {
        if (Input.GetMouseButtonDown(1) && this.onHover) {
            this.solved ^= true;
        }
    }
}