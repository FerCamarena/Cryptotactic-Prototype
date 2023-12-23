using UnityEngine;
public class Mailbox : Item {
    public GameObject basicMail;
    public uint left;
    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            if(this.left > 0) {
                this.left--;
                GameObject newmail = Instantiate(this.basicMail, this.transform.position, Quaternion.identity);
                newmail.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 25.0f;
            }
        }
    }
}