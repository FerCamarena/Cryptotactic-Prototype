using UnityEngine;
public class Mail : Item {
    public int defaultCooldown = 250;
    public bool solved;
    public bool infected;
    public bool scanned;
    public override void ItemUpdate() {
        if (Input.GetMouseButtonDown(1) && this.onHover) {
            this.solved ^= true;
        }
    }
}