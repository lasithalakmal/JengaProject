using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController g;

    public GameObject stack_1;
    public GameObject stack_2;
    public GameObject stack_3;

    public Block wood, glass, stone;
    public DetailsPanel details_panel;
    public GameObject camAxis;
    public AudioSource glass_crack;

    public List<Block> bstack_1;
    public List<Block> bstack_2;
    public List<Block> bstack_3;

    private void Awake() {
        g = this;
    }


    void Start() {
        Physics.autoSimulation = false;
        //glass.spawBlock(new Vector3(12, 0.6f, 1), 90, stack_1.transform, "abc");
        bstack_1 = new List<Block>();
        bstack_2 = new List<Block>();
        bstack_3 = new List<Block>();
        StartCoroutine(ServerManager.GetRequest("https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack"));
    }


    public void analyzeJsonText(string jtext) {
        GlobalData gdata = GlobalData.GetGlobalData();
        gdata.current_focus_stack = 2;
        gdata.blocks_by_grades = new Dictionary<string, List<BlockData>>();
        List<BlockData> all_blocks = JsonConvert.DeserializeObject<List<BlockData>>(jtext);
        foreach (BlockData b in all_blocks) {
            List<BlockData> blockDatas;
            if (gdata.blocks_by_grades.GetValueOrDefault(b.grade, null) == null) {
                blockDatas = new List<BlockData>();
                blockDatas.Add(b);
                gdata.blocks_by_grades.Add(key: b.grade, blockDatas);
            } else {
                blockDatas = gdata.blocks_by_grades.GetValueOrDefault(b.grade, null);
                blockDatas.Add(b);
            }
        }
        sortStackProcess(gdata);
        //StartCoroutine(spawnStacks(gdata));
    }

    public void sortStackProcess(GlobalData gdata) {
        List<BlockData> sixth = null, seventh = null, eighth = null;
        List<BlockData> sorted_sixth = new List<BlockData>();
        List<BlockData> sorted_seventh = new List<BlockData>();
        List<BlockData> sorted_eights = new List<BlockData>();
        foreach (string b in gdata.blocks_by_grades.Keys) {
            if (b == "6th Grade") {
                sixth = gdata.blocks_by_grades.GetValueOrDefault(b, null);
            } else if (b == "7th Grade") {
                seventh = gdata.blocks_by_grades.GetValueOrDefault(b, null);
            } else if (b == "8th Grade") {
                eighth = gdata.blocks_by_grades.GetValueOrDefault(b, null);
            }
        }
        sortSingleStack_By_Domain(sixth,sorted_sixth);
        sortSingleStack_By_Domain(seventh,sorted_seventh);
        sortSingleStack_By_Domain(eighth, sorted_eights);
        StartCoroutine(spawnStacks(sorted_sixth,gdata));
        StartCoroutine(spawnStacks(sorted_seventh, gdata));
        StartCoroutine(spawnStacks(sorted_eights, gdata));
    }


    public void sortSingleStack_By_Domain(List<BlockData> lb, List<BlockData> final_list) {
        SortedDictionary<string, List<BlockData>> ascending_list = new SortedDictionary<string, List<BlockData>>();
        foreach (BlockData b in lb) {
            if (ascending_list.ContainsKey(b.domain)) {
                ascending_list.GetValueOrDefault(b.domain, null).Add(b);
            } else {
                ascending_list[b.domain] = new List<BlockData> { b };
            }
        }
        foreach (List<BlockData> al in ascending_list.Values) {
            sortSingleStack_By_Clustername(al, final_list);
        }
    }

    public void sortSingleStack_By_Clustername(List<BlockData> lb, List<BlockData> final_list) {
        SortedDictionary<string, List<BlockData>> ascending_list = new SortedDictionary<string, List<BlockData>>();
        foreach (BlockData b in lb) {
            if (ascending_list.ContainsKey(b.domain)) {
                ascending_list.GetValueOrDefault(b.domain, null).Add(b);
            } else {
                ascending_list[b.domain] = new List<BlockData> { b };
            }
        }
        foreach (List<BlockData> al in ascending_list.Values) {
            sortSingleStack_By_StandardId(al, final_list);
        }
    }

    public void sortSingleStack_By_StandardId(List<BlockData> lb, List<BlockData> final_list) {
        SortedDictionary<string, List<BlockData>> ascending_list = new SortedDictionary<string, List<BlockData>>();
        foreach (BlockData b in lb) {
            if (ascending_list.ContainsKey(b.domain)) {
                ascending_list.GetValueOrDefault(b.domain, null).Add(b);
            } else {
                ascending_list[b.domain] = new List<BlockData> { b };
            }
        }
        foreach (List<BlockData> al in ascending_list.Values) {
            foreach(BlockData b in al) {
                Debug.Log(b.id);
                final_list.Add(b);
            }
        }
    }

    public IEnumerator spawnStacks(List<BlockData> lb,GlobalData gdata) {
        //Debug.Log(lb.Count);
        int row = 0;
        int column = 0;
        string prev_stack = "";
        foreach (BlockData block in lb) {
            if (prev_stack != block.grade) {
                row = 0;
                column = 0;
                prev_stack = block.grade;
            }
            float[] xz = gdata.stack_row[row % 2][column];
            float y = gdata.gap * ((row + 1) * 2 - 1);
            //Debug.Log(row + " " + column + " " + xz[0] + " " + xz[1] + " " + y);
            if (block.grade == "6th Grade") {
                prev_stack = block.grade;
                if (block.mastery == 0) {
                    glass.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_1.transform, block);
                } else if (block.mastery == 1) {
                    wood.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_1.transform, block);
                } else if (block.mastery == 2) {
                    stone.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_1.transform, block);
                }
            } else if (block.grade == "7th Grade") {
                //Debug.Log(block.mastery);
                float x = stack_2.transform.position.x;
                if (block.mastery == 0) {
                    glass.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_2.transform, block);
                } else if (block.mastery == 1) {
                    wood.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_2.transform, block);
                } else if (block.mastery == 2) {
                    stone.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_2.transform, block);
                }
            } else if (block.grade == "8th Grade") {
                float x = stack_2.transform.position.x;
                if (block.mastery == 0) {
                    glass.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_3.transform, block);
                } else if (block.mastery == 1) {
                    wood.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_3.transform, block);
                } else if (block.mastery == 2) {
                    stone.spawBlock(new Vector3(xz[0], y, xz[1]), gdata.z_angles[row % 2], stack_3.transform, block);
                }
            }
            column++;
            if (column == 3) {
                column = 0;
                row++;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
