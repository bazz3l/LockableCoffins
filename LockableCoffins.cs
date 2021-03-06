using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Lockable Coffins", "Bazz3l", "1.0.0")]
    [Description("Allow placement of codelocks on coffins.")]
    class LockableCoffins : RustPlugin
    {
        #region Oxide
        void OnEntitySpawned(BaseEntity entity)
        {
            if (entity is StorageContainer && entity.PrefabName.Contains("coffin"))
            {
                MakeLockable(entity as StorageContainer);
            }

            if (entity is BaseLock)
            {
                MoveLock(entity as BaseEntity);
            }
        }
        #endregion

        #region Core
        void MakeLockable(StorageContainer container) => container.isLockable = true;

        void MoveLock(BaseEntity entity)
        {
            BaseEntity parentEntity = entity.GetParentEntity();

            if (parentEntity != null && parentEntity.PrefabName.Contains("coffin"))
            {
                entity.transform.localPosition += new Vector3(0.1f, -0.2f, 0f);

                parentEntity.SendNetworkUpdateImmediate();
            }
        }
        #endregion
    }
}