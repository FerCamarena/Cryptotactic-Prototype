using UnityEngine;
public class Cursor : MonoBehaviour {
    private GameObject selectedItem;
    private Vector3 cursorPoint;
    [SerializeField] private Material hoverMaterial;
    Item selection;
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                if(!this.selectedItem) {
                    this.selectedItem = hit.collider.gameObject;
                    this.selectedItem.TryGetComponent<Item>(out selection);
                    if(!this.selection || !this.selection.selected) { 
                        this.selection.selected = true;
                    }
                }
                if (this.selectedItem.CompareTag("Item")) {
                    Material[] originalMaterials = this.selectedItem.GetComponent<MeshRenderer>().sharedMaterials;
                    Material[] newMaterials = new Material[originalMaterials.Length + 1];

                    for (int i = 0; i < originalMaterials.Length; i++) {
                        newMaterials[i] = originalMaterials[i];
                    }

                    newMaterials[originalMaterials.Length] = this.hoverMaterial;

                    this.selectedItem.GetComponent<MeshRenderer>().sharedMaterials = newMaterials;
                }
            }
        }else if(Input.GetMouseButton(0) && this.selectedItem) {
            if (this.selectedItem.CompareTag("Item")) {
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
            Material[] originalMaterials = this.selectedItem.GetComponent<MeshRenderer>().sharedMaterials;
            Material[] newMaterials = new Material[originalMaterials.Length-2];

            for (int i = 0; i < originalMaterials.Length-2; i++) {
                //SI EL MATERIAL == HOVER NO LO AGREGA
                newMaterials[i] = originalMaterials[i];
            }
            //FALTA CAMBIAR EL MATERIAL POR UNICAMENTE REMPLAZAR EL QUE SE USA PARA HOVER NO EL ULTIMO.
            //originalMaterials[originalMaterials.Length-1] = null;
            this.selectedItem.GetComponent<MeshRenderer>().sharedMaterials = newMaterials;

            this.selectedItem = null;
            this.selection.selected = false;
        }
    }
}