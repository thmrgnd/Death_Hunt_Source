using UnityEngine;

public class Fire : Item
{
    [SerializeField] GameObject fireDeathCollider;
    DeathManager deathManager;
    bool fireLit;
    bool diedOfFire;
    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
    }

    void Update()
    {

    }

    public void SetDiedOfFire()
    {
        diedOfFire = true;
    }

    public override void EatItem()
    {
        if (fireLit)
        {
            if (diedOfFire)
            {
                deathManager.SetDiedOfFireEating();
                return;
            }
        }
    }

    public override void UseItem()
    {
        if (fireLit)
            return;
        fireDeathCollider.SetActive(true);
        fireLit = true;
        Animator[] children = gameObject.GetComponentsInChildren<Animator>();
        foreach (var animator in children)
        {
            animator.SetTrigger("Fire Active");
        }
    }

}
