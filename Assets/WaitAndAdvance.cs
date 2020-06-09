using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flow",
"Wait before Advance",
"Wait before advancing text")]
public class WaitAndAdvance : Command
{
    public float seconds;
    // Start is called before the first frame update

    public override void OnEnter()
    {
        Invoke ("OnWaitComplete", 3f);
    }

    public override string GetSummary(){
        return "Wait before automatically advancing text";
    }

    protected virtual void OnWaitComplete()
        {
            Continue();
        }
}
