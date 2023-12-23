using UnityEngine;
public class Cursor : MonoBehaviour {
    private GameObject selectedItem;
    private Vector3 cursorPoint;
    private Vector3 cursorOffset;
    Item selection;
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Item")) {
            if (Input.GetMouseButtonDown(0)) {
                this.selectedItem = hit.collider.gameObject;
                this.cursorOffset = this.selectedItem.transform.position - hit.point;
                if(this.selectedItem.TryGetComponent<Item>(out this.selection)) {
                    this.selection.hovered = false;
                }
            } else if (!Input.GetMouseButton(0)) {
                if (hit.collider.gameObject.CompareTag("Item") && hit.collider.TryGetComponent<Item>(out this.selection)){
                    this.selection.hovered = true;
                }
            }
        }
        if(Input.GetMouseButton(0) && this.selectedItem) {
            this.selection.dragged = true;
            this.selection.hovered = false;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);
            mousePosition.z = Camera.main.transform.position.y * 0.99f;
            this.cursorPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            this.selectedItem.transform.position = this.cursorPoint + this.cursorOffset;
            //Vector3 cursorDirection = (this.cursorPoint - selectedItem.transform.position).normalized;
            //selectedItem.transform.zposition = Quaternion.LookRotation(cursorDirection);
        }
        if(Input.GetMouseButtonUp(0)) {
            this.selectedItem = null;
            if (this.selection) {
                this.selection.hovered = false;
                this.selection.dragged = false;
            }
        }
    }
}