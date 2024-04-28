using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingdamage : MonoBehaviour
{
    [HideInInspector] public float damage;
    private TextMesh textmesh;

    private void Start()
    {
        textmesh = GetComponent<TextMesh>();
        textmesh.text = "-" + damage;
    }
    public void OnAnimationover()
    {
        Destroy(gameObject);
    }
}
