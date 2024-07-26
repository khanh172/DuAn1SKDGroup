using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void Die(); // Tuỳ chọn, nếu bạn muốn các đối tượng chết khi nhận đủ sát thương
}
