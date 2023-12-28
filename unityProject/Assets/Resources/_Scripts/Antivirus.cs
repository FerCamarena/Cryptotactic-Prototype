using UnityEngine;
public class Antivirus : Item {
    public int defaultCooldown = 600;
    public override void ItemUpdate() {
        if (this.process <= 0 && this.deckStack.Count > 0) {
            if (this.deckStack[0].TryGetComponent<Mail>(out Mail mail)) {
                if(mail.infected) {
                    mail.infected = false;
                    this.DropFirst(1);
                } else { 
                    this.DropFirst(-1);
                }
                this.process = this.defaultCooldown;
            }
        }
    }
    public void DropFirst(int direction){
        this.deckStack[0].parent = null;
        float randomX = Random.Range(0.3f, 0.5f) * direction;
        float randomY = Random.Range(0.85f, 1f);
        float randomZ = Random.Range(0.1f, 0.2f) * (Random.value > 0.5f ? 1 : -1);
        this.deckStack[0].GetComponent<Rigidbody>().velocity = new Vector3(randomX, randomY, randomZ) * 50.0f;
        this.deckStack[0].GetComponent<Rigidbody>().useGravity = true;
        this.deckStack.RemoveAt(0);
    }
}