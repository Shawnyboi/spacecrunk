using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseState : MonoBehaviour
{
    public List<Crunk> players;
    public List<Ship> ships;

    private bool gameOver = false;

    private void Start()
    {
        
    }

    private IEnumerator PlayGame()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(2f);

        }
    }
}
