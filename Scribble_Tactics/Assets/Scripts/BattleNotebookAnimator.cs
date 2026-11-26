using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BattleNotebookAnimator : MonoBehaviour
{
    [SerializeField] float slideSpeed = 1f;

    private bool isBattleInitiated;
    Vector3 targetBookPosition;

    void Start()
    {
        EventManager.Instance.OnBattleInitiate += EventManager_OnBattleInitiate;
    }

    void Update()
    {
        if (isBattleInitiated)
        {
            MoveBookIntoPosition();
        }
    }

    private void EventManager_OnBattleInitiate(object sender, EventManager.OnBattleInitiateArgs e)
    {
        targetBookPosition = e.player.transform.position;
        targetBookPosition.z = transform.position.z; 
        isBattleInitiated = true;
    }

    private void MoveBookIntoPosition()
    {
        transform.position += (targetBookPosition - transform.position) * slideSpeed * Time.deltaTime;

        float reachedDistance = 1f;
        if (Vector3.Distance(transform.position, targetBookPosition) < reachedDistance)
        {
            transform.position = targetBookPosition;
            isBattleInitiated = false;
        }

    }
  
}
