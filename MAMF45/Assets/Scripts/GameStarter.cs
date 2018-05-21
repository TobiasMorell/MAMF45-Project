using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour {
    public GameObject[] ToBeDestroyedOnStart;

	public Clock clock;

    public void StartGame()
    {
        if (!Constants.Instance.HasGameBegun)
        {
            Constants.Instance.HasGameBegun = true;
            StartCoroutine(CountdownToGameover());

            //Start spawner and spawn initial group of bunnies
            //SpawnBunny(new ColdIllness());

            foreach (GameObject o in ToBeDestroyedOnStart)
                Destroy(o);
        }
    }

    private IEnumerator CountdownToGameover()
    {
		var gameTime = Constants.Instance.GameTime;
		clock.Run(gameTime);
		yield return new WaitForSeconds(gameTime - Constants.Instance.GameEndFadeTime);
        Camera.main.GetComponent<SceneFader>().FadeOut(Constants.Instance.GameEndFadeTime);
    }
}
