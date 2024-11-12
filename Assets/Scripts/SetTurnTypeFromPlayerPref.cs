using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnTypeFromPlayerPref : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    private void Start()
    {
        StartCoroutine(DelayedApplyPlayerPref());
    }

    private IEnumerator DelayedApplyPlayerPref()
    {
        yield return new WaitForSeconds(0.5f);

        
        DisableAllTurnMethods();

        ApplyPlayerPref();
    }

    public void ApplyPlayerPref()
    {
       
        if (PlayerPrefs.HasKey("turn"))
        {
            int value = PlayerPrefs.GetInt("turn");

            DisableAllTurnMethods();

            if (value == 0)
            {
             
                snapTurn.leftHandSnapTurnAction.action.Enable();
                snapTurn.rightHandSnapTurnAction.action.Enable();
            }
            else if (value == 1)
            {

                continuousTurn.leftHandTurnAction.action.Enable();
                continuousTurn.rightHandTurnAction.action.Enable();
            }
        }
       
    }

    private void DisableAllTurnMethods()
    {

        if (snapTurn != null)
        {
            snapTurn.leftHandSnapTurnAction.action.Disable();
            snapTurn.rightHandSnapTurnAction.action.Disable();
        }

        if (continuousTurn != null)
        {
            continuousTurn.leftHandTurnAction.action.Disable();
            continuousTurn.rightHandTurnAction.action.Disable();
        }
    }

    public void SetTurnType(int turnType)
    {


       
        PlayerPrefs.SetInt("turn", turnType);
        PlayerPrefs.Save();

        
        ApplyPlayerPref();
    }
}
