using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int currScore;
    public Text score;

    private void Start()
    {
        currScore = 0;    
    }

    public void IncreaseScore()
    {
        currScore += 1;

        if (currScore >= 10) {
            currScore = 10;
        }

        score.text = "Score: " + currScore.ToString();
    }
	
}
