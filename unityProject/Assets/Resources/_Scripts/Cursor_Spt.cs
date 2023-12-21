using UnityEngine;
public class Cursor_Spt : MonoBehaviour {
    private GameObject selectedItem;
    private Vector3 cursorPoint;
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                if(!this.selectedItem) {
                    this.selectedItem = hit.collider.gameObject;
                }
            }
        }else if(Input.GetMouseButton(0) && this.selectedItem) {
            if (selectedItem.CompareTag("Item")) {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
                mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);
                mousePosition.z = Camera.main.transform.position.y * 0.9f;
                this.cursorPoint = Camera.main.ScreenToWorldPoint(mousePosition);
                this.selectedItem.transform.position = this.cursorPoint;
                //Vector3 cursorDirection = (this.cursorPoint - selectedItem.transform.position).normalized;
                //selectedItem.transform.zposition = Quaternion.LookRotation(cursorDirection);
            }
        } else if(Input.GetMouseButtonUp(0)) {
            this.selectedItem = null;
        }
    }
}