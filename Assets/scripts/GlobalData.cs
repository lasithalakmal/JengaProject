using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData {

    public Dictionary<string, List<BlockData>> blocks_by_grades;

    static GlobalData gd;
    public int current_focus_stack;

    //Odd stack coordinates (x,z)
    public float[][][] stack_row = new float[][][]{
        new float[][]{
            new float[]{ 0, -3},
            new float[]{ 0, 0},
            new float[]{ 0, 3},
        },
        new float[][]{
            new float[]{ -3, 0},
            new float[]{ 0, 0},
            new float[]{ 3, 0},
        }
    };


    public float[] z_angles = new float[2] { 0, 90};
    public float gap = 0.6f;

    public static GlobalData GetGlobalData() {
        if (gd == null) {
            gd = new GlobalData();
        }
        return gd;
    }

}
