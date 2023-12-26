using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour {
    public bool canStack = true;
    public int deck = 0;
    public bool onSelect;
    public bool onDrag;
    public bool onHover;
    public GameObject selectOutline;
    public GameObject dragOutline;
    public GameObject hoverOutline;
    public float force = 5f;
    protected Rigidbody rb;
    public GameObject parent;
    public List<GameObject> derivates;

    private void Awake() {
        this.rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        OutlineUpdate();
    }
    private void Update() {
        if (!this.parent) {
            this.deck = this.GetInstanceID();
        }
        for (int i = 0; i < derivates.Count; i++) {
            Item childItem = derivates[i].GetComponent<Item>();
            if (childItem.gameObject.layer == 6) {
                if(childItem.deck != this.deck) {
                    this.derivates.RemoveAt(i);
                    childItem.parent = null;
                } else if (!childItem.onDrag) {
                    Vector3 cardOffset = Vector3.back + Vector3.up * 0.01f;
                    float animSpeed = 120 * Time.deltaTime;
                    if (i - 1 == -1) {
                        childItem.transform.position = Vector3.Lerp(childItem.transform.position, this.transform.position + (cardOffset * 2), animSpeed);
                    }
                    else {
                        childItem.transform.position = Vector3.Lerp(childItem.transform.position, derivates[i - 1].transform.position + cardOffset, animSpeed);
                    }
                    childItem.GetComponent<Rigidbody>().useGravity = false;
                    childItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            if(this.canStack && collider.TryGetComponent<Item>(out Item item)) {
                if(item.deck == item.GetInstanceID()){
                    item.deck = this.deck;
                    if(item.parent != this.gameObject) { 
                        item.parent = this.gameObject;
                        derivates.Add(item.gameObject);
                    }
                }
            }
        }
    }
}