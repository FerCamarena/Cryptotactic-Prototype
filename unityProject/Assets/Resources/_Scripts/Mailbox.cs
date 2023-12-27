using TMPro;
using UnityEngine;
public class Mailbox : Item {
    public GameObject basicMail;
    public uint mailsLeft;
    public TextMeshPro mailLeftDisplay;
    public override void SolveItem() {
        if (Input.GetMouseButtonDown(1) && this.onHover) {
            if(this.mailsLeft > 0) {
                this.mailsLeft--;
                float randomX = Random.Range(0.05f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                float randomY = Random.Range(1.0f, 1.25f) * (Random.value > 0.5f ? 1 : -1);
                float randomZ = Random.Range(0.05f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                GameObject newmail = Instantiate(this.basicMail, this.transform.position, Quaternion.identity);
                newmail.GetComponent<Rigidbody>().velocity = new Vector3(randomX, randomY, randomZ) * 50.0f;
            }
        }
        this.mailLeftDisplay.text = this.mailsLeft.ToString();
        if(this.process <= 0) {
            this.process = 1000;
            this.mailsLeft++;
        }
    }
}