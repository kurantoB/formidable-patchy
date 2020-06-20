using System.Collections;
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
        character = GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>();
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
            case PatchyExpression.PATCHYSPLAINING:
                GetFlowchart().SetStringVariable("PatchyExpression", "PATCHYSPLAINING");
                break;
            case PatchyExpression.LAMENTING:
                GetFlowchart().SetStringVariable("PatchyExpression", "LAMENTING");
                break;
            case PatchyExpression.FLUSTERED:
                GetFlowchart().SetStringVariable("PatchyExpression", "FLUSTERED");
                break;
            case PatchyExpression.MIFFED:
                GetFlowchart().SetStringVariable("PatchyExpression", "MIFFED");
                break;
            case PatchyExpression.READING:
                GetFlowchart().SetStringVariable("PatchyExpression", "READING");
                break;
            case PatchyExpression.DOKIDOKI:
                GetFlowchart().SetStringVariable("PatchyExpression", "DOKIDOKI");
                break;
        }
        GetFlowchart().ExecuteBlock("PatchyPortrait");

        float waitTime = storyText.Length > 70 ? 9 : 4;
        if (GameObject.FindGameObjectWithTag("MessageTimer") != null)
        {
            timer tmr = GameObject.FindGameObjectWithTag("MessageTimer").GetComponent<timer>();
            tmr.timerReset(waitTime, Continue);
        }
        base.OnEnter();
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
            case "EXASPERATED":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[2];
            case "INQUISITIVE":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[3];
            case "SAD":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[4];
            case "SURPRISED":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[5];
            case "THINKING":
                return GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>().Portraits[6];
            default:
                return null;
        }
    }

    public override void Continue()
    {
        if (GameObject.FindObjectOfType<GameFlow>() != null)
        {
            GameObject.FindObjectOfType<GameFlow>().HandleContinue(this, BaseContinue);
        } else
        {
            base.Continue();
        }
    }

    private void BaseContinue()
    {
        base.Continue();
    }
}
