using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Narrative",
                 "MarisaSay",
                 "Marisa's message")]
public class SayMarisa : Say
{
    public MarisaExpression expression;
    public override void OnEnter()
    {
        character = GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>();
        portrait = GetCharacterPortrait();
        character.SetSayDialog.CharacterImage.CrossFadeAlpha(1, 0.25f, true);
        float waitTime = storyText.Length > 70 ? 9 : 4.5f;
        timer tmr = GameObject.FindGameObjectWithTag("MessageTimer").GetComponent<timer>();
        tmr.timerReset(waitTime, Continue);
        base.OnEnter();
    }

    public override string GetSummary()
    {
        return base.GetSummary();
    }

    private Sprite GetCharacterPortrait()
    {
        switch (expression)
        {
            case MarisaExpression.HAPPY:
                GetFlowchart().SetStringVariable("MarisaExpression", "HAPPY");
                return character.Portraits[0];
            case MarisaExpression.GRINNING:
                GetFlowchart().SetStringVariable("MarisaExpression", "GRINNING");
                return character.Portraits[1];
            case MarisaExpression.EXASPERATED:
                GetFlowchart().SetStringVariable("MarisaExpression", "EXASPERATED");
                return character.Portraits[2];
            case MarisaExpression.INQUISITIVE:
                GetFlowchart().SetStringVariable("MarisaExpression", "INQUISITIVE");
                return character.Portraits[3];
            case MarisaExpression.SAD:
                GetFlowchart().SetStringVariable("MarisaExpression", "SAD");
                return character.Portraits[4];
            case MarisaExpression.SURPRISED:
                GetFlowchart().SetStringVariable("MarisaExpression", "SURPRISED");
                return character.Portraits[5];
            case MarisaExpression.THINKING:
                GetFlowchart().SetStringVariable("MarisaExpression", "THINKING");
                return character.Portraits[6];
            default:
                return null;
        }
    }

    public override void Continue()
    {
        GameObject.FindObjectOfType<GameFlow>().HandleContinue(this, BaseContinue);
    }

    private void BaseContinue()
    {
        base.Continue();
    }
}
