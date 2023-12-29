using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("OnInteraction")]
    [SerializeField] private Charm curruntCharm = null;
    [SerializeField] private Transform charmHolder;
    [Header("PartnerData")]
    [SerializeField] private BoxCollider2D boundsColliderMin;
    [SerializeField] private BoxCollider2D boundsColliderMax;

    [SerializeField] private bool isDebug;

    public bool HasCharmSO()
    {
        return curruntCharm != null;
    }

    public void SetCharm(Charm charm)
    {
        curruntCharm = charm; 
    }

    public Transform GetCharmHolder()
    {
        return charmHolder;
    }

    public Charm GetCham()
    {
        return curruntCharm;
    }

    private void Start()
    {
        if (isDebug)
        {
            GetPartnerBoundsArea();
        }
    }

    public Vector2 GetPartnerBoundsArea()
    {

        float minX = (boundsColliderMin.bounds.size.x / 2);
        float minY = (boundsColliderMin.bounds.size.y / 2);

        float maxX = (boundsColliderMax.bounds.size.x / 2);
        float maxY = (boundsColliderMax.bounds.size.y / 2); 

        //assignXRandomValue
        bool isPosX = Random.Range(0, 2) == 1 ? true : false;
        float randomX = 0;
        if (isPosX)
            randomX = Random.Range(minX, maxX);
        else
            randomX = Random.Range(-minX, -maxX);



        //AssignYRandomValue
        bool isPosY = Random.Range(0, 2) == 1 ? true : false;
        float randomY = 0;
        if (isPosY)
            randomY = Random.Range(minY, maxY);
        else
            randomY = Random.Range(-minY, -maxY);


        Vector2 randomPos = new Vector2(randomX + transform.position.x, randomY + transform.position.y);

        if (isDebug)
        {
            Debug.Log("MinValue : " + minX + ", " + minY);
            Debug.Log("MaxValue : " + maxX + ", " + maxY);
            Debug.Log("MovementValue : " + randomX + ", " + randomY);
        }
        return randomPos;
    }
}
