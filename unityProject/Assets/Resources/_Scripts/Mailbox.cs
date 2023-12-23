using TMPro;
using UnityEngine;
public class Mailbox : Item {
    public GameObject basicMail;
    public uint left;
    public TextMeshPro mailCount;
    private void Update() {
        if (Input.GetMouseButtonDown(1) && this.onHover) {
            if(this.left > 0) {
                this.left--;
                float randomX = Random.Range(0.05f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                float randomY = Random.Range(1.0f, 1.25f) * (Random.value > 0.5f ? 1 : -1);
                float randomZ = Random.Range(0.05f, 0.75f) * (Random.value > 0.5f ? 1 : -1);
                GameObject newmail = Instantiate(this.basicMail, this.transform.position, Quaternion.identity);
                newmail.GetComponent<Rigidbody>().velocity = new Vector3(randomX, randomY, randomZ) * 50.0f;
            }
        }
        this.mailCount.text = this.left.ToString();
    }
}