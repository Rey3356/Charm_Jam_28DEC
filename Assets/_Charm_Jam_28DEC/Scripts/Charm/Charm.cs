using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : MonoBehaviour
{
    [Header("LooksData")]
    [SerializeField] private AnimationClip[] myAnimations;
    [SerializeField] private Animator objAnimator;

    [Header("CharmData")]
    public CharmSO charmData;
    private Collider2D charmCollider;

    private void Awake()
    {
        charmCollider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        ChangeLook();
    }

    private void ChangeLook()
    {
        objAnimator.Play(myAnimations[Random.Range(0, myAnimations.Length)].name);
    }

    public CharmSO GetCharmSO()
    {
        return charmData;
    }

    public void SetCharmSO(CharmSO charmSO)
    {
        charmData = charmSO;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        charmCollider.enabled = false;
    }

    public void ThrowMe()
    {
        //play effect or something
        Destroy(gameObject);
        transform.parent = null;
        charmCollider.enabled = true;
    }
}
