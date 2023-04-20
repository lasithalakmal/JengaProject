using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public string block_type;

    public BlockData blockData;

    [SerializeField]
    ParticleSystem explode;

    void Start() {

    }

    public void destroyBlock() {
        explode.Play();
        Debug.Log("desssssssssssss " + block_type);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 1);
    }

    public void spawBlock(Vector3 pos, float z_rot, Transform parent, BlockData blockData) {
        Block b;
        if (block_type == "Glass") {
            b = Instantiate(GameController.g.glass, parent);
        } else if (block_type == "Wood") {
            b = Instantiate(GameController.g.wood, parent);
        } else {
            b = Instantiate(GameController.g.stone, parent);
        }

        if (blockData.grade == "6th Grade") {
            GameController.g.bstack_1.Add(b);
        } else if (blockData.grade == "7th Grade") {
            GameController.g.bstack_2.Add(b);
        } else if (blockData.grade == "8th Grade") {
            GameController.g.bstack_3.Add(b);
        }

        b.gameObject.transform.localPosition = pos;
        b.transform.eulerAngles = new Vector3(-90f, 0, z_rot);
        b.blockData = blockData;
    }

    // Update is called once per frame
    void Update() {

    }
}
