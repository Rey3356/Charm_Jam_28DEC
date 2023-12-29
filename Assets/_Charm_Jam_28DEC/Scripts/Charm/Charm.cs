using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : MonoBehaviour
{
    [Header("LooksData")]
    [SerializeField] private Sprite[] mylooks;
    [SerializeField] private SpriteRenderer obj;
    [Header("CharmData")]
    public CharmSO charmData;
    private Collider2D charmCollider;

    private void Awake()
    {
        charmCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        ChangeLook();

    }

    private void ChangeLook()
    {
        obj.sprite = mylooks[Random.Range(0, mylooks.Length)];
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
