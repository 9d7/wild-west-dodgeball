using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private HealthUIController HealthBar;
    [SerializeField] private Collider2D damageCollider;
    [SerializeField] private float DamageAnimTime = 1;
    private float m_health = 0;
    public MainMenu menuController;

    public bool invinicible;
    // Start is called before the first frame update
    void Start()
    {
        m_health = MaxHealth;
        UpdateUI();
    }

    public void BeInvincibleForTime(float t)
    {
        StopCoroutine("InvincibleForTime");
        StartCoroutine(InvincibleForTime(t));
    }

    private IEnumerator InvincibleForTime(float _t)
    {
        invinicible = true;
        damageCollider.enabled = false;
        yield return new WaitForSeconds(_t);
        damageCollider.enabled = true;
        invinicible = false;
    }
    
    private IEnumerator HurtRoutine()
    {
        BeInvincibleForTime(DamageAnimTime);
        GetComponent<PlayerMovement>().spriteAnim.SetBool("Hurt", true);
        yield return new WaitForSeconds(DamageAnimTime);
        GetComponent<PlayerMovement>().spriteAnim.SetBool("Hurt", false);
    }
    
    public bool TakeDamage(float amt)
    {
        if (invinicible)
            return false;
        m_health = Mathf.Clamp(m_health - amt, 0, MaxHealth);
        UpdateUI();
        StartCoroutine(HurtRoutine());
        if (m_health == 0)
        {
            //Die
            GameManager.Instance.OnPlayerDied();
            menuController.GameEnd(false);
            return true;
        }

        return false;
    }

    private void UpdateUI()
    {
        HealthBar.RenderHealth(m_health);
    }
}
