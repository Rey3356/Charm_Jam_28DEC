using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactLayer;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        InputManager.OnInteract += InputManager_OnInteract;
    }

    private void InputManager_OnInteract(object sender, EventArgs e)
    {
        if (CanInteract(out Collider2D[] objectToInteract))
        {
            // if playerHas Chamr thatn it can interact with partner
            if (playerManager.HasCharmSO())
            {
                Debug.Log("PlayerHas CharmSO and he is trying to get partner");
                foreach (var item in objectToInteract)
                {
                    if (item.TryGetComponent(out PartnerBehaviour partnerBehaviour))
                    {
                        if (partnerBehaviour.IsGood(playerManager.GetCham().GetCharmSO()))
                        {
                            //Get Score
                            Debug.LogWarning("CorrectCharm!");
                            playerManager.GetCham().ThrowMe();
                            playerManager.SetCharm(null);
                        }
                        else
                        {
                            Debug.LogError("IncorrectCharm!");
                        }
                    }
                    else
                    {
                        playerManager.GetCham().ThrowMe();
                        playerManager.SetCharm(null);
                        return;
                    }
                }

                //else it can interact with cham

            }
            else
            {
                foreach (var item in objectToInteract)
                {
                    if (item.TryGetComponent(out Charm charm))
                    {
                        playerManager.SetCharm(charm);
                        charm.SetParent(playerManager.GetCharmHolder());
                        return;
                    }
                }

            }
        }
        else
        {
            if (playerManager.HasCharmSO())
            {
                playerManager.GetCham().ThrowMe();
                playerManager.SetCharm(null);
            }
        }
    }

    private bool CanInteract(out Collider2D[] objectToInteract)
    {
        objectToInteract = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactLayer);
        return objectToInteract.Length != 0;
    }

    private void OnDisable()
    {
        InputManager.OnInteract -= InputManager_OnInteract;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
