using UnityEngine;

public class Item : MonoBehaviour {
    public bool selected;
    public bool dragged;
    public bool hovered;
    public GameObject selection;
    public GameObject drag;
    public GameObject hover;
    private void Start() {
    }
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject) {
            if(Input.GetKeyDown(KeyCode.F)) {
                selected ^= true;
            }
        }
        if(selected) { 
            selection.SetActive(true);
        } else {
            selection.SetActive(false);
        }
        if(dragged) {
            drag.SetActive(true);
        } else {
            drag.SetActive(false);
        }
        if(hovered) {
            hover.SetActive(true);
        } else {
            hover.SetActive(false);
        }
        hovered = false;
    }
}