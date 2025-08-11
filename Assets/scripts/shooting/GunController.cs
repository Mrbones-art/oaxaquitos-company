using UniRx;
using UnityEngine;

namespace TopDown.Shooting
{



    public class GunController : MonoBehaviour
    {
        [Header("Cooldown")]
        [SerializeField] private float cooldown = 0.25f;
        private float cooldownTime;

        [Header("References")]
        [SerializeField] private GameObject Prefabbala;
        [SerializeField] private Transform firepoint;
        [SerializeField] private Animator muzzleFlashAnimator;

        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

        private void Awake()
        {
            TotalAmmo.Value = initialAmmo;

            if (initialAmmo <= clipSize)
                CurrentAmmoInClip.Value = initialAmmo;
            else
                CurrentAmmoInClip.Value = clipSize;
        }

        [Header("Ammo")]
        [SerializeField] private int initialAmmo;
        [SerializeField] private int clipSize;
        private void Update()
        {
            cooldownTime += Time.deltaTime;
        }


        private void Shoot()
        {
            if (cooldownTime < cooldown) return;
            if (CurrentAmmoInClip.Value <= 0) return;

            GameObject bullet = Instantiate(Prefabbala, firepoint.position, firepoint.rotation, null);
            muzzleFlashAnimator.SetTrigger("Dispara");
            cooldownTime = 0;
            bullet.GetComponent<Projectile>().ShootBullet(firepoint);

            CurrentAmmoInClip.Value--;
        }

       private void Reload()
{
    if (TotalAmmo.Value <= 0) return;

    // Bullets needed to fill the clip
    int bulletsNeeded = clipSize - CurrentAmmoInClip.Value;
    if (bulletsNeeded <= 0) return; // Clip already full

    // How many we can actually reload
    int bulletsToReload = Mathf.Min(bulletsNeeded, TotalAmmo.Value);

    // Transfer from reserve to clip
    CurrentAmmoInClip.Value += bulletsToReload;
    TotalAmmo.Value -= bulletsToReload;
}


        #region Input
        private void OnShoot()
        {
            Shoot();
        }
        private void OnReload()
        {
            Reload();
        }
        #endregion
    }
}
