using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    bool isObjectOnSight(out RaycastHit hit) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    public void clickBlock(BlockData bl) {
        GameController.g.details_panel.openPanel(bl);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            if (isObjectOnSight(out RaycastHit hit)) {
                Block b = hit.collider.gameObject.GetComponent<Block>();
                if (b != null) {
                    clickBlock(b.blockData);
                }
            }
        }
    }
}
