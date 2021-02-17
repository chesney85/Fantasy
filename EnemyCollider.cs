using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
   public EnemyHealth health;
   public int damageMultiplier;
   
   public void DamageEnemy(int _bulletDamage){
      health.DamageEnemy(_bulletDamage * damageMultiplier);
   }
}
