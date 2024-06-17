using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;

//namespace Assets.HeroEditor.Common.Scripts.EditorScripts

public class PlayerManager : MonoBehaviour
{
    private Character[] Character = new Character[3];

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Sta", 0.01f);
    }
    public void Sta()
    {
        for (int i = 0; i < FightManager.Instance.PlayerPos.Length; i++)
        {
            FightManager.Instance.PlayerPos[i].GetChild(0).TryGetComponent<Character>(out Character[i]);
            Character[i].UpdateAnimation();
        }
    }
    public void PlayerDeath(int pos, int i)
    {
        var state = Character[pos].GetState();
        if (i == 6)
        {
            state = CharacterState.DeathB;
        }
        else if (i == 0)
        {
            state = CharacterState.Idle;
        }
        

        /*if (state < 0)
        {
            state = CharacterState.DeathF;
        }
        else if (state > CharacterState.DeathF)
        {
            state = CharacterState.Idle;
        }*/

        Character[pos].SetState(state);

    }
}
