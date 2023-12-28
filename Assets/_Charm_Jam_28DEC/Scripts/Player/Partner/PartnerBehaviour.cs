using System.Collections.Generic;
using UnityEngine;

public partial class PartnerBehaviour : MonoBehaviour
{
    private PartnerState curruntPartnerState;

    private List<int> idelStateTimeData;//in seconds
    private List<int> searshingStateTimeData;//in seconds
    private List<int> timeOutData;//In seconds

    private void Start()
    {
        InitState_And_Behaviour();
    }

    private void InitState_And_Behaviour()
    {
        curruntPartnerState = PartnerState.Idel;
    }

    private void Update()
    {
        ManagePartnerState();
    }

    private void ManagePartnerState()
    {
        switch (curruntPartnerState)
        {
            case PartnerState.Idel:
                IdelState();
                break;
            case PartnerState.Searching:
                break;
            case PartnerState.Found:
                break;
            case PartnerState.ReSearching:
                break;
        }
    }

    private void IdelState()
    {
        float randomTimeToStayInIdel = (float)idelStateTimeData[Random.Range(0, idelStateTimeData.Count)];
        randomTimeToStayInIdel -= Time.deltaTime;
        if (randomTimeToStayInIdel < 0)
        {
            curruntPartnerState = PartnerState.Searching;
        }
    }

    private void SearchCharm()
    {

    }

    private void SelectCharm()
    {

    }


}
