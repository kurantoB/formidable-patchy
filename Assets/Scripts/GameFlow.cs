using UnityEngine;
using Fungus;
using TMPro;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public Flowchart flowChart;
    public delegate void BaseContinue();
    public TextMeshProUGUI thingsLeftText;
    public Effects effects;
    public GameObject endMenu;

    public void ChangeTopic(string topic)
    {
        TopicManager.instance.ClearTopicRoll();

        flowChart.SetStringVariable("NextBlock", topic);
        flowChart.SetBooleanVariable("PatchyInitiated", false);
        flowChart.SetBooleanVariable("ConfessionZone", false);
        flowChart.SetBooleanVariable("TopicWinddown", false);
        if (flowChart.GetStringVariable("CurrentBlock").Equals("Becoming a Youkai (Real Talk)")
            || flowChart.GetStringVariable("CurrentBlock").Equals("Magic (Real Talk)")) {
            effects.MoveProgressHeart(2);
        }
    }

    public void HandleContinue(Say sayCommand, BaseContinue bc)
    {
        string nextBlock = flowChart.GetStringVariable("NextBlock");
        if (!"Empty".Equals(nextBlock))
        {
            sayCommand.StopParentBlock();
            flowChart.ExecuteBlock(nextBlock);
            if (TopicManager.instance.IsAlreadyVisited(nextBlock))
            {
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
                SetThingsLeftText(flowChart.GetIntegerVariable("ChancesLeft"));
                flowChart.SetStringVariable("CurrentBlock", nextBlock);
                flowChart.SetStringVariable("NextBlock", "Empty");
                flowChart.SetStringVariable("BadTransition", "Already Talked About");
            }
            else if (!flowChart.GetBooleanVariable("TopicWinddown"))
            {
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 2));
                SetThingsLeftText(flowChart.GetIntegerVariable("ChancesLeft"));
                flowChart.SetStringVariable("CurrentBlock", nextBlock);
                flowChart.SetStringVariable("BadTransition", "Abrupt Topic Change");
                flowChart.SetStringVariable("NextBlock", "Empty");
            } else if (nextBlock.Equals("Youkai Confession") || nextBlock.Equals("Magic Confession") || nextBlock.Equals("Fail Confession"))
            {
                flowChart.SetStringVariable("NextBlock", "Empty");
            } else {
                // good transition
                flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
                SetThingsLeftText(flowChart.GetIntegerVariable("ChancesLeft"));
                flowChart.SetStringVariable("CurrentBlock", nextBlock);
                flowChart.SetStringVariable("NextBlock", "Empty");

                TopicManager.instance.ActivateTopicRoll();
            }
        } else {
            bc();
        }
    }

    public void PatchyTopicChange()
    {
        TopicManager.instance.ClearTopicRoll();

        if (flowChart.GetIntegerVariable("ChancesLeft") == 0)
        {
            Debug.Log("PatchyTopicChange: End of Convo");
            flowChart.FindBlock(flowChart.GetStringVariable("CurrentBlock")).Stop();
            flowChart.ExecuteBlock("End of Convo");
            flowChart.SetStringVariable("NextBlock", "Empty");
            return;
        }

        string nextBlock = TopicManager.instance.GetNextTopic();

        TopicManager.instance.ActivateTopicRoll();

        Debug.Log("PatchyTopicChange: " + nextBlock);
        flowChart.SetIntegerVariable("ChancesLeft", Mathf.Max(0, flowChart.GetIntegerVariable("ChancesLeft") - 1));
        SetThingsLeftText(flowChart.GetIntegerVariable("ChancesLeft"));
        flowChart.SetBooleanVariable("PatchyInitiated", true);
        flowChart.ExecuteBlock(nextBlock);
        flowChart.SetStringVariable("CurrentBlock", nextBlock);
        flowChart.SetStringVariable("NextBlock", "Empty");
    }

    public void CheckProgress(string newTopic)
    {
        //int progress = TopicManager.instance.GetProgress(newTopic);
        effects.MoveProgressHeart(0);
    }

    public void ReenableTopicRoll()
    {
        if (flowChart.GetIntegerVariable("ChancesLeft") >= 1)
        {
            TopicManager.instance.ActivateTopicRoll();
        }
    }

    private void SetThingsLeftText(int thingsLeft)
    {
        if (thingsLeft == 1)
        {
            thingsLeftText.SetText("1 thing left to talk about");
        } else
        {
            thingsLeftText.SetText(thingsLeft + " things left to talk about");
        }
        effects.BlinkThingsLeft();
    }

    public void DisplayEndMenu()
    {
        endMenu.SetActive(true);
    }

    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
