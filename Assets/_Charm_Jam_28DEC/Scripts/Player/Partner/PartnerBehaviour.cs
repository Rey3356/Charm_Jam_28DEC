using System.Collections.Generic;
using UnityEngine;

public partial class PartnerBehaviour : MonoBehaviour
{
    private PartnerState curruntPartnerState;

    [Header("Player Reference")]
    [SerializeField] private PlayerManager playerManager;

    [Header("Timing Details")]
    [SerializeField] private List<int> idelStateTimeData;//in seconds
    [SerializeField] private List<int> searshingStateTimeData;//in seconds
    [SerializeField] private List<int> timeOutData;//In seconds

    [Header("Movement and Interaction")]
    [SerializeField] private float speed = 0;
    [SerializeField] private float checkingRedius = 0;
    [SerializeField] private LayerMask charm;

    private float randomTimeToStayInIdel = 0;
    private float randomTimeToStayInSearching = 0;
    private float randomTimeToWaitInFound = 0;

    private Vector2 movePos;

    #region Core
    private void Start()
    {
        InitState_And_Behaviour();
    }

    private void InitState_And_Behaviour()
    {
        curruntPartnerState = PartnerState.Idel;
        randomTimeToStayInIdel = (float)idelStateTimeData[Random.Range(0, idelStateTimeData.Count)];
        randomTimeToWaitInFound = (float)timeOutData[Random.Range(0, timeOutData.Count)];
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
                SearchCharm();
                break;
            case PartnerState.Found:
                FoundState();
                break;
        }
    }
    #endregion

    #region IdelState
    private void IdelState()
    {
        randomTimeToStayInIdel -= Time.deltaTime;
        if (randomTimeToStayInIdel < 0)
        {
            movePos = playerManager.GetPartnerBoundsArea();
            curruntPartnerState = PartnerState.Searching;
            randomTimeToStayInSearching = searshingStateTimeData[Random.Range(0, searshingStateTimeData.Count)];
        }
    }
    #endregion

    #region SearchState
    private void SearchCharm()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, movePos);
        //if we near to the location
        if (distance < .1)
        {
            //than we decrease the time
            randomTimeToStayInSearching -= Time.deltaTime;

            //try to get the charm
            Collider2D charmObject = Physics2D.OverlapCircle(transform.position, checkingRedius, charm);

            //if time is over 
            if (randomTimeToStayInSearching < 0)
            {

                //and in that time if we not got charm then we gonna reash for new location
                if (charmObject == null)
                {
                    Debug.Log("Search Again!");
                    GetNewPositionToMove();
                }
                //if we get that we go in this function 
                else
                {
                    //use that charm 
                    IfWeFoundCharm();
                }
            }
        }
    }

    private void IfWeFoundCharm()
    {
        //if prob is high than we can select this charm else we need new position
        if (CanSelectCharm())
        {
            curruntPartnerState = PartnerState.Found;
        }
        else
        {
            curruntPartnerState = PartnerState.Searching;
            GetNewPositionToMove();
        }
    }

    private void GetNewPositionToMove()
    {
        movePos = playerManager.GetPartnerBoundsArea();
        randomTimeToStayInSearching = searshingStateTimeData[Random.Range(0, searshingStateTimeData.Count)];
    }
    private bool CanSelectCharm()
    {
        //i have to go with lower value cause it's taking too much time when we can't able to find charm quiqly if you want than you can increase the value of probability or random.range
        const int probability = 2;
        return Random.Range(0, 5) > probability;
    }
    #endregion

    #region FoundState
    private void FoundState()
    {
        randomTimeToWaitInFound -= Time.deltaTime;
        Debug.Log(randomTimeToWaitInFound);
        if (randomTimeToWaitInFound < 0)
        {
            curruntPartnerState = PartnerState.Idel;
            randomTimeToWaitInFound = (float)timeOutData[Random.Range(0, timeOutData.Count)];
        }
    }
    #endregion
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkingRedius);
    }
}
