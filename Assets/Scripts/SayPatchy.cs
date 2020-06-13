﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Narrative",
                 "PatchySay",
                 "Patchouli's message")]
public class SayPatchy : Say
{
    public PatchyExpression expression;
    public override void OnEnter()
    {
        if (storyText.Equals("{x}"))
        {
            base.OnEnter();
            return;
        }
        character = GameObject.FindGameObjectWithTag("PatchyCharacter").GetComponent<Character>();
        portrait = GetMarisaDimPortrait();
        character.SetSayDialog.CharacterImage.CrossFadeAlpha(0.5f, 0.25f, true);
        switch (expression)
        {
            case PatchyExpression.NEUTRAL:
                GetFlowchart().SetStringVariable("PatchyExpression", "NEUTRAL");
                break;
            case PatchyExpression.HAPPY:
                GetFlowchart().SetStringVariable("PatchyExpression", "HAPPY");
                break;
        }
        GetFlowchart().ExecuteBlock("PatchyPortrait");

        float waitTime = storyText.Length > 70 ? 9 : 4;
        timer tmr = GameObject.FindGameObjectWithTag("MessageTimer").GetComponent<timer>();
        tmr.timerReset(PatchyMessageExit);
        base.OnEnter();
        tmr.timerStart(waitTime);
    }

    public override string GetSummary()
    {
        return base.GetSummary();
    }

    private Sprite GetMarisaDimPortrait()
    {
        switch (GetFlowchart().GetStringVariable("MarisaExpression"))
        {
            case "HAPPY":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[0];
            case "GRINNING":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[1];
            default:
                return null;
        }
    }

    private void PatchyMessageExit()
    {
        GetFlowchart().ExecuteBlock("PatchySayEnd");
    }
}
