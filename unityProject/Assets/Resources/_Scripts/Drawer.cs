public class Drawer : Item {
    public int defaultCooldown = 400;
    public override void ItemUpdate() {
        if (this.process <= 0 && this.deckStack.Count > 0) {
            if (this.deckStack[0].TryGetComponent<Mail>(out Mail mail)) {
                if(mail.solved) {
                } else {
                }
                this.deckStack.RemoveAt(0);
                Destroy(mail.gameObject);
                this.process = this.defaultCooldown;
            }
        }
    }
}