using UnityEngine;
public class Mailbox_Spt : MonoBehaviour {
    public GameObject basicMail;
    private void Start() {
        GameObject mail1 = Instantiate(this.basicMail);
        mail1.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
        GameObject mail2 = Instantiate(this.basicMail);
        mail2.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
        GameObject mail3 = Instantiate(this.basicMail);
        mail3.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
        GameObject mail4 = Instantiate(this.basicMail);
        mail4.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
        GameObject mail5 = Instantiate(this.basicMail);
        mail5.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
        GameObject mail6 = Instantiate(this.basicMail);
        mail6.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
        GameObject mail7 = Instantiate(this.basicMail);
        mail7.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100.0f;
    }
}
