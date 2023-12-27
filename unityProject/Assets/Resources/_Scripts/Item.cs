using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Item : MonoBehaviour {
    public bool canStack = true;
    public int deckID = 0;
    public bool onSelect;
    public bool onDrag;
    public bool onHover;
    public GameObject selectOutline;
    public GameObject dragOutline;
    public GameObject hoverOutline;
    public float force = 5f;
    protected Rigidbody rb;
    public Item parent;
    public List<Item> deckStack;
    public int process = 1000;

    private void Awake() {
        this.rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        this.OutlineUpdate();
        this.CooldownUpdate();
    }
    private void Update() {
        this.SolveItem();
        if (!this.parent) {
            this.deckID = this.GetInstanceID();
        }
        foreach (var childItem in this.deckStack) {
            int i = this.deckStack.IndexOf(childItem);
            if(childItem.deckID != this.deckID) {
                for (int j = this.deckStack.Count - 1; j > i - 1; j--) {
                    if (childItem != this.deckStack[j]) childItem.AddOnStack(this.deckStack[j]);
                    this.deckStack.RemoveAt(j);
                }
                childItem.parent = null;
                break;
            } else if (!childItem.onDrag) {
                childItem.parent = this;
                Vector3 displayOffset = Vector3.back + Vector3.up * 0.01f;
                float animSpeed = 120 * Time.deltaTime;
                if (i - 1 == -1) {
                    childItem.transform.position = Vector3.Lerp(childItem.transform.position, this.transform.position + (displayOffset * 2), animSpeed);
                }
                else {
                    childItem.transform.position = Vector3.Lerp(childItem.transform.position, deckStack[i - 1].transform.position + displayOffset, animSpeed);
                }
                childItem.GetComponent<Rigidbody>().useGravity = false;
                childItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        if (collider.TryGetComponent<Item>(out Item colliderItem) && this.deckID != colliderItem.deckID) {
            Vector3 direction = this.transform.position - collider.transform.position;
            float currentDistance = direction.magnitude;
            this.rb.velocity += (this.force / (Random.Range(-0.5f, 0.5f) + currentDistance * currentDistance) *
                                ((Vector3.forward * Random.Range(-0.1f, 0.1f) + Vector3.right *
                                Random.Range(-0.1f, 0.1f)) + direction));
        }
    }
    public bool CheckIfOnStack(Item item){
        foreach (var itemStacked in this.deckStack){
            if(itemStacked == item){
                return true;
            }
        }
        return false;
    }
    public Item LookForParent() {
        if (this.parent) {
            return this.parent.GetComponent<Item>().LookForParent();
        }
        return this;
    }
    public abstract void SolveItem();
    public void CooldownUpdate() {
        if (this.process > 0) {
            this.process--;
        }
    }
    public void AddOnStack(Item item){
        if (!CheckIfOnStack(item)) {
            item.deckID = this.deckID;
            item.parent = this;
            this.deckStack.Add(item);
            if(item.deckStack.Count > 0) {
                foreach (var stackedItem in item.deckStack) {
                    this.AddOnStack(stackedItem);
                }
                item.deckStack.Clear();
            }
        }
    }
    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.layer == 6 && collider.TryGetComponent<Item>(out Item item) && this.canStack) {
            if(item.deckID == item.GetInstanceID()) { 
                if (this.gameObject.layer == 7) {
                    this.AddOnStack(item);
                } else if(this.gameObject.layer == 6) {
                    if(!item.canStack || item.transform.position.z < this.transform.position.z) {
                        this.LookForParent().AddOnStack(item);
                    }
                }
            }
        }
    }
}