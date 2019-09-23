using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseState : MonoBehaviour
{
    public List<Crunk> team1players;
    public List<Crunk> team2players;
    public Ship team1ship;
    public Ship team2ship;


    private bool gameOver = false;

    private void Start()
    {
        
    }

    private void CheckForGameOver()
    {
        bool team1lost = true;
        foreach(Crunk p in team1players){
            if(p != null)
            {
                team1lost = false;
            }
        }
        if(team1ship != null)
        {
            team1lost = false;
        }
        bool team2lost = true;
        foreach (Crunk p in team2players)
        {
            if (p != null)
            {
                team2lost = false;
            }
        }
        if (team2ship != null)
        {
            team2lost = false;
        }

        if(team1lost || team2lost)
        {
            gameOver = true;
            Debug.Log("GameOver");
        }
    }

    private IEnumerator PlayGame()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(2f);
            CheckForGameOver();
        }
    }
}
