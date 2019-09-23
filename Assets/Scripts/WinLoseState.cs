using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseState : MonoBehaviour
{
    public List<Crunk> team1players;
    public List<Crunk> team2players;
    public Ship team1ship;
    public Ship team2ship;

	[SerializeField]
	Animator gameOverScreen = null;

    private bool gameOver = false;

	public bool fake1Lost = false;
	public bool fake2Lost = false;

    private void Start()
    {
		StartCoroutine(PlayGame());
    }

    private void CheckForGameOver()
    {
        bool team1lost = true;
        foreach(Crunk p in team1players){
            if(p != null || !p.isAlive())
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
            if (p != null || !p.isAlive())
            {
                team2lost = false;
            }
        }
        if (team2ship != null)
        {
            team2lost = false;

        }

		team1lost = team1lost || fake1Lost;
		team2lost = team2lost || fake2Lost;

		Debug.Log(team1lost);
		if (team1lost || team2lost)
        {
			if (team1lost)
			{
				gameOverScreen.SetTrigger("CrunkWins");
			}

			if (team2lost)
			{
				gameOverScreen.SetTrigger("HunkWins");

			}

			gameOver = true;
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

	public void GotoMainMenu()
	{
		SceneManager.LoadScene("StartScreen");
	}
}
