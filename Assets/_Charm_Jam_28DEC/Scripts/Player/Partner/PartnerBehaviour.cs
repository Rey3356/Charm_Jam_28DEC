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

    [Header("Player Follow Data")]
    [SerializeField] private float playerFollowAfterRedius = 0;
    [SerializeField] private float playerFollowSpeed = 5f;

    private float randomTimeToStayInIdel = 0;
    private float randomTimeToStayInSearching = 0;
    private float randomTimeToWaitInFound = 0;

    private Vector2 movePos;

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
                CanFollowPlayer();
                break;
            case PartnerState.Searching:
                SearchCharm();
                CanFollowPlayer();
                break;
            case PartnerState.Found:
                FoundState();
                CanFollowPlayer();
                break;
            case PartnerState.FollowPlayer:
                FollowPlayer();
                break;
        }
        Debug.Log(curruntPartnerState);
    }

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

    private void SearchCharm()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, movePos);
        if (distance < .1)
        {
            randomTimeToStayInSearching -= Time.deltaTime;

            Collider2D charmObject = Physics2D.OverlapCircle(transform.position, checkingRedius, charm);

            if (randomTimeToStayInSearching < 0)
            {

                if (charmObject == null)
                {
                    Debug.Log("Search Again!");
                    GetNewPositionToMove();
                }
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
        const int probability = 2;
        return Random.Range(0, 5) > probability;
    }

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

    private void CanFollowPlayer()
    {
        float diff = Vector3.Distance(playerManager.transform.position, transform.position);
        if (diff > playerFollowAfterRedius)
        {
            curruntPartnerState = PartnerState.FollowPlayer;
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerManager.transform.position, playerFollowSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, playerManager.transform.position);
        if (distance < playerFollowAfterRedius)
        {
            curruntPartnerState = PartnerState.Idel;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkingRedius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerFollowAfterRedius);
    }
}
