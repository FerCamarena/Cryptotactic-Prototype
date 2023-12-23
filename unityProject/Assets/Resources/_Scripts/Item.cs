using UnityEngine;
public class Item : MonoBehaviour {
    public bool onSelect;
    public bool onDrag;
    public bool onHover;
    public GameObject selectOutline;
    public GameObject dragOutline;
    public GameObject hoverOutline;
    private void FixedUpdate() {
        OutlineUpdate();
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
}