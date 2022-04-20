using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaOutlet : MonoBehaviour
{
    private GameObject model;
    private Animator animator;
    public AITrainingBot Producing;
    private AITrainingBot made;


    private bool isEmpty;

    private void Awake()
    {
        model = transform.Find("Model").gameObject;
        animator = model.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        made = Instantiate(Producing, this.transform.position, Quaternion.identity);
        made.SetTargetLoc(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty)
        {
            made = Instantiate(Producing, this.transform.position, Quaternion.identity);
            made.SetTargetLoc(new Vector3(0,0,0));

            animator.Play("OpenDoor");
        }
        else
        {
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var items = other.GetComponentsInParent<AMecha>();
        if (items.Length <= 0)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
    }
}
