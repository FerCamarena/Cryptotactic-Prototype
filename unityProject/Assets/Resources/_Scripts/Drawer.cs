using UnityEngine;
public class Drawer : Item {
    public override void SolveItem() {
        if (this.process <= 0 && this.deckStack.Count > 0) {
            if (this.deckStack[0].TryGetComponent<Mail>(out Mail mail)) {
                if(mail.solved) {
                    Debug.Log("Solved");
                } else {
                    Debug.Log("Unsolved");
                }
                this.deckStack.RemoveAt(0);
                Destroy(mail.gameObject);;
                this.process = 1000;
            }
        } else if(this.process > 0 && this.deckStack.Count > 0) {
            this.process--;
        }
    }
}