using UnityEngine;
public class Cursor : MonoBehaviour {
    private GameObject selectedItem;
    private Vector3 cursorPoint;
    private Vector3 cursorOffset;
    private Vector2 mouseStart;
    private Vector2 mouseEnd;
    Item selection;
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Item")) {
            if (Input.GetMouseButtonDown(0)) {
                this.mouseStart = Input.mousePosition;
                this.selectedItem = hit.collider.gameObject;
                this.cursorOffset = this.selectedItem.transform.position - hit.point;
                if(this.selectedItem.TryGetComponent<Item>(out this.selection)) {
                    this.selection.onHover = false;
                }
            } else if (!Input.GetMouseButton(0)) {
                if (hit.collider.gameObject.CompareTag("Item") && hit.collider.TryGetComponent<Item>(out this.selection)){
                    this.selection.onHover = true;
                    if (Input.GetKeyDown(KeyCode.S)){
                        this.selection.onSelect = !this.selection.onSelect;
                    }
                }
            }
        }
        if(Input.GetMouseButton(0) && this.selectedItem) {
            this.mouseEnd = Input.mousePosition;
            if (this.selection) {
                this.selection.onDrag = true;
                this.selection.onHover = false;
            }
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);
            mousePosition.z = Camera.main.transform.position.y * 0.98f;
            this.cursorPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 newPosition;
            if (Vector2.Distance(this.mouseStart, this.mouseEnd) >= 25) {
                newPosition = this.cursorPoint + this.cursorOffset;
            } else {
                newPosition = new Vector3(this.selectedItem.transform.position.x, 2, this.selectedItem.transform.position.z);
            }
            this.selectedItem.transform.position = Vector3.Lerp(this.selectedItem.transform.position, newPosition, 100 * Time.deltaTime);
            //Vector3 cursorDirection = (this.cursorPoint - selectedItem.transform.position).normalized;
            //selectedItem.transform.zposition = Quaternion.LookRotation(cursorDirection);
        }
        if(Input.GetMouseButtonUp(0)) {
            if (this.selection) {
                if(Vector2.Distance(this.mouseStart,this.mouseEnd) >= 25) {
                    this.selection.deckID = this.selection.GetInstanceID();
                }
                this.selection.onDrag = false;
            }
            if (this.selectedItem) { 
                this.selectedItem.GetComponent<Rigidbody>().useGravity = true;
            }
            this.selection = null;
            this.selectedItem = null;
        }
    }
}