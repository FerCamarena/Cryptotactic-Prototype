using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour {
    public bool canStack = true;
    public int deck;
    public bool onSelect;
    public bool onDrag;
    public bool onHover;
    public GameObject selectOutline;
    public GameObject dragOutline;
    public GameObject hoverOutline;
    public float force = 5f;
    protected Rigidbody rb;

    private void Awake() {
        this.rb = GetComponent<Rigidbody>();
        this.deck = this.gameObject.GetInstanceID();
    }
    private void FixedUpdate() {
        OutlineUpdate();
        // Iterate through all children and filter by the "Object" tag
        int place = 0;
        if (!this.transform.parent) {
            this.deck = this.GetInstanceID();
        }
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.gameObject.layer== 6) {
                if(child.GetComponent<Item>().deck != this.deck) { 
                    child.parent = null;
                } else if (!child.GetComponent<Item>().onDrag) { 
                    place++;
                    child.transform.position = Vector3.Lerp(child.transform.position, this.transform.position + Vector3.back * (place + 1) + Vector3.up * 0.005f * (place), 0.25f);
                    child.GetComponent<Rigidbody>().useGravity = false;
                    child.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }
    private void OutlineUpdate() {
        if(this.onSelect) {
            this.selectOutline.SetActive(true);
        } else {
            this.selectOutline.SetActive(false);
        }
        if(this.onDrag) {
            this.dragOutline.SetActive(true);
        } else {
            this.dragOutline.SetActive(false);
        }
        if(this.onHover) {
            this.hoverOutline.SetActive(true);
        } else {
            this.hoverOutline.SetActive(false);
        }
        this.onHover = false;
    }
    
    private void OnTriggerStay(Collider collider) {
        if (collider.TryGetComponent<Item>(out Item colliderItem) && this.deck != colliderItem.deck) {
            Vector3 direction = this.transform.position - collider.transform.position;
            float currentDistance = direction.magnitude;
            this.rb.velocity += (this.force / (Random.Range(-0.5f, 0.5f) + currentDistance * currentDistance) *
                                ((Vector3.forward * Random.Range(-0.1f, 0.1f) + Vector3.right *
                                Random.Range(-0.1f, 0.1f)) + direction));
        }
    }
    private void OnTriggerEnter(Collider collider) {
        if (this.gameObject.layer == 7) {
            if(collider.gameObject.layer == 6 && this.canStack && !this.onDrag) { 
                collider.GetComponent<Item>().deck = this.deck;
                collider.transform.parent = this.transform;
            }
        }
        
    }
}