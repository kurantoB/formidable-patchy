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
        if (storyText.Equals("{x}"))
        {
            base.OnEnter();
            return;
        }
        character = GameObject.FindGameObjectWithTag("MarisaCharacter").GetComponent<Character>();
        portrait = GetCharacterPortrait();
        character.SetSayDialog.CharacterImage.CrossFadeAlpha(1, 0.25f, true);
        float waitTime = storyText.Length > 70 ? 9 : 4;
        timer tmr = GameObject.FindGameObjectWithTag("MessageTimer").GetComponent<timer>();
        tmr.timerReset(MarisaMessageExit);
        base.OnEnter();
        tmr.timerStart(waitTime);
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
            default:
                return null;
        }
    }

    private void MarisaMessageExit()
    {
        Continue();
    }
}
