﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Narrative",
                 "MarisaSay",
                 "Marisa's message")]
public class SayMarisa : Say
{
    public MarisaExpressions expression;
    public override void OnEnter()
    {
        character = GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>();
        portrait = character.Portraits[0];
        float waitTime = storyText.Length > 70 ? 9 : 4;
        timer tmr = GameObject.FindGameObjectWithTag("MessageTimer").GetComponent<timer>();
        tmr.timerReset(this);
        base.OnEnter();
        tmr.timerStart(waitTime);
    }

    public override string GetSummary()
    {
        return base.GetSummary();
    }
}