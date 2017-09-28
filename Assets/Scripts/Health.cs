using UnityEngine;

public class Health : MonoBehaviour {

    public const int maxHealth = 7;

    public int currentHealth = maxHealth;

    public void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("Lose!");
        }
    }
	
}
