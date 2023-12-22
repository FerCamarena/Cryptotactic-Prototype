using UnityEngine;
public class Cursor : MonoBehaviour {
    private GameObject selectedItem;
    private Vector3 cursorPoint;
    private Vector3 cursorOffset;
    Item selection;
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit)) {
            if(!this.selectedItem && hit.collider.gameObject.CompareTag("Item")) {
                this.selectedItem = hit.collider.gameObject;
                this.cursorOffset = this.selectedItem.transform.position - hit.point;
                if(this.selectedItem.TryGetComponent<Item>(out this.selection) && (!this.selection || !this.selection.selected)) { 
                    this.selection.selected = true;
                }
            }
        }else if(Input.GetMouseButton(0) && this.selectedItem) {
            if (this.selectedItem.CompareTag("Item")) {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
                mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);
                mousePosition.z = Camera.main.transform.position.y * 0.9f;
                this.cursorPoint = Camera.main.ScreenToWorldPoint(mousePosition);
                this.selectedItem.transform.position = this.cursorPoint + this.cursorOffset;
                //Vector3 cursorDirection = (this.cursorPoint - selectedItem.transform.position).normalized;
                //selectedItem.transform.zposition = Quaternion.LookRotation(cursorDirection);
            }
        } else if(Input.GetMouseButtonUp(0)) {
            if (this.selectedItem) { 
                this.selectedItem = null;
            }
            if (this.selection) { 
                this.selection.selected = false;
            }
        }
    }
}