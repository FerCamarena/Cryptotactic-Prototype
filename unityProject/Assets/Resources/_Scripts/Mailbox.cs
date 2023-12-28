using TMPro;
using UnityEngine;
public class Mailbox : Item {
    public int defaultCooldown = 500;
    public GameObject basicMail;
    public uint mailsLeft;
    public TextMeshPro mailLeftDisplay;
    public override void ItemUpdate() {
        this.mailLeftDisplay.text = this.mailsLeft.ToString();
        if(this.process <= 0) {
            this.process = this.defaultCooldown;
            this.mailsLeft++;
        }
        if (Input.GetMouseButtonDown(1) && this.onHover) {
            if(this.mailsLeft > 0) {
                this.mailsLeft--;
                float randomX = Random.Range(0.01f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                float randomY = Random.Range(0.75f, 1.0f);
                float randomZ = Random.Range(0.01f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                if (randomX < 0.2f && randomZ < 0.2f) {
                    randomX = Random.Range(0.5f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                }
                GameObject newmail = Instantiate(this.basicMail, this.transform.position, Quaternion.identity);
                newmail.GetComponent<Rigidbody>().velocity = new Vector3(randomX, randomY, randomZ) * 50.0f;
            }
        }
    }
    public override void ProcessUpdate() {
        if (this.process > 0 && !this.onDrag) {
            this.process--;
        }
    }
}