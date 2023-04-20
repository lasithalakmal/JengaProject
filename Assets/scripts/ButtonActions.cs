using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    public void testMyStack() {
        Physics.autoSimulation = true;
        Debug.Log("destroy "+ GlobalData.GetGlobalData().current_focus_stack);
        if (GlobalData.GetGlobalData().current_focus_stack == 1) {
            foreach (Block b in GameController.g.bstack_1) {
                b.GetComponent<Rigidbody>().WakeUp();
                if(b.blockData.mastery == 0) {
                    if (!GameController.g.glass_crack.isPlaying) {
                        GameController.g.glass_crack.Play();
                    }
                    b.destroyBlock();
                }
            }
        } else if (GlobalData.GetGlobalData().current_focus_stack == 2) {
            Debug.Log("destroy 2");
            foreach (Block b in GameController.g.bstack_2) {
                b.GetComponent<Rigidbody>().WakeUp();
                Debug.Log(b.blockData.mastery+" "+ b.block_type);
                if (b.blockData.mastery == 0) {
                    if (!GameController.g.glass_crack.isPlaying) {
                        GameController.g.glass_crack.Play();
                    }
                    b.destroyBlock();
                }
            }
        } else if (GlobalData.GetGlobalData().current_focus_stack == 3) {
            foreach (Block b in GameController.g.bstack_3) {
                b.GetComponent<Rigidbody>().WakeUp();
                if (b.blockData.mastery == 0) {
                    if (!GameController.g.glass_crack.isPlaying) {
                        GameController.g.glass_crack.Play();
                    }
                    b.destroyBlock();
                }
            }
        }
    }

    public void moveToStack(int stackId) {
        GlobalData.GetGlobalData().current_focus_stack = stackId;
        if (stackId == 1) {
            LeanTween.moveLocalX(GameController.g.camAxis, GameController.g.stack_1.transform.position.x, 1).setEase(LeanTweenType.easeOutExpo);
        } else if (stackId == 2) {
            LeanTween.moveLocalX(GameController.g.camAxis, GameController.g.stack_2.transform.position.x, 1).setEase(LeanTweenType.easeOutExpo);
        } else if (stackId == 3) {
            LeanTween.moveLocalX(GameController.g.camAxis, GameController.g.stack_3.transform.position.x, 1).setEase(LeanTweenType.easeOutExpo);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
