using UnityEngine;

public class StatsTab : MonoBehaviour
{

    public PlayerUI playerUI;

    private void OnEnable()
    {
        playerUI.SetStatsText();
    }

}
