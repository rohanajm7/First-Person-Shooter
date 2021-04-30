using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField] float health = 100f;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageDealer(float amount)
    {
        health -= amount;
    }

}
