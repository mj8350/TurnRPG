using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    [SerializeField] GameObject obj;

    SkeletonAnimation monsterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //ChangeAnimation("Attack");
        }
    }
    public void ChangeAnimation(GameObject obj,string AnimationName)  //Names are: Idle, Walk, Dead and Attack
    {
        monsterAnimator = obj.GetComponent<SkeletonAnimation>();

        if (monsterAnimator == null)
            return;

        bool IsLoop = true;
        if (AnimationName == "Dead" || AnimationName == "Attack")
            IsLoop = false;

        //set the animation state to the selected one
        monsterAnimator.AnimationState.SetAnimation(0, AnimationName, IsLoop);
    }
}
