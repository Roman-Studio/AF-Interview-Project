namespace AFSInterview.Units
{
    public interface IAFDamageable
    { 
        bool CanBeDamaged();
        void Damage(float receivedDamage);
    }
}