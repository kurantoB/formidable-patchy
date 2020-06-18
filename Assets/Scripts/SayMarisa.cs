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
        float waitTime = storyText.Length > 70 ? 9 : 4;
        timer tmr = GameObject.FindGameObjectWithTag("MessageTimer").GetComponent<timer>();
        tmr.timerReset(waitTime, MarisaMessageExit);
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
            default:
                return null;
        }
    }

    private void MarisaMessageExit()
    {
        GameObject.FindObjectOfType<GameFlow>().HandleContinue(this, BaseContinue);
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
