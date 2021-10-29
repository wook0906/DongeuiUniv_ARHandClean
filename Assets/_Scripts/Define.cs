using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Infection
    {
        None,
        Left = 1<<0,
        Right = 1<<1,
        Both = Left | Right
    }
    public enum Side
    {
        Left,
        Right,
    }
    public enum HandCleanRecord
    {
        None,

        S1_1,
        S1_2,
        S1_3,
        S1_4,
        S1_5,
        S1_6,
        S1_7,
        S1_8,
        S1_9,
        S1_10,

        S2_1,
        S2_2,
        S2_3,
        S2_4,
        S2_5,
        S2_6,
        S2_7,
        S2_8,
        S2_9,
        S2_10,
        S2_11,
        S2_12,

        S3_1,
        S3_2,
        S3_3,
        S3_4,
        S3_5,
        S3_6,
        S3_7,
        S3_8,
        S3_9,
        S3_10,
        S3_11,

        S4_1,
        S4_2,
        S4_3,
        S4_4,
        S4_5,
        S4_6,
        S4_7,
        S4_8,
        S4_9,
        S4_10,
        S4_11,
        S4_12,
        S4_13,
        S4_14,
    }
    public enum Views
    {
        //None,
        Left_Patient_Stand,
        Left_Patient_LeftView,
        Left_BedLever,
        Left_Patient_RightView_CloseUp,
        Left_Patient_RightView,
        Both,
        Right_Patient_Sit,
        Right_Patient,
        Right_Bed_Pee,
        Right_BedLever,
    }
   
    public enum Scene
    {
        UnKnown,
        Scene_1,
        Scene_2,
        Scene_3,
        Scene_4,
        Max,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum MouseEvent
    {
        LeftPress,
        LeftPointerDown,
        LeftPointerUp,
        LeftClick,
        RightPress,
        RightPointerDown,
        RightPointerUp,
        RightClick,
    }
}
