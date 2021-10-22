using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Views
    {
        None,
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
