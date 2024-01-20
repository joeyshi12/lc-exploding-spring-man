using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;

namespace SpringManKamikaze.MonoBehaviours
{
    public class SmkNetworkManager : NetworkBehaviour
    {
        public static SmkNetworkManager instance;

        void Awake()
        {
            instance = this;
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnExplosionServerRpc(SerializableVector3 position)
        {
            SpawnExplosionClientRpc(position);
        }

        [ClientRpc]
        private void SpawnExplosionClientRpc(SerializableVector3 position)
        {
            SpawnExplosion(position.ToVector3() + Vector3.up);
        }

        public void SpawnExplosion(Vector3 explosionPosition, float damageRadius = 6f, float minDamage = 50f, float maxDamage = 100f) {
            Plugin.mls.LogInfo("Spawning explosion at position = " + explosionPosition.ToString());
            Instantiate(StartOfRound.Instance.explosionPrefab, explosionPosition, Quaternion.Euler(-90f, 0f, 0f), RoundManager.Instance.mapPropsContainer.transform).SetActive(value: true);

            float num = Vector3.Distance(GameNetworkManager.Instance.localPlayerController.transform.position, explosionPosition);
            if (num < 14f)
            {
                HUDManager.Instance.ShakeCamera(ScreenShakeType.Big);
            }
            else if (num < 25f)
            {
                HUDManager.Instance.ShakeCamera(ScreenShakeType.Small);
            }

            Collider[] overlapColliders = Physics.OverlapSphere(explosionPosition, damageRadius, 2621448, QueryTriggerInteraction.Collide);
            for (int i = 0; i < overlapColliders.Length; i++)
            {
                Collider collider = overlapColliders[i];
                float distance = Vector3.Distance(explosionPosition, collider.transform.position);
                if (distance > 4f && Physics.Linecast(explosionPosition, collider.transform.position + Vector3.up * 0.3f, 256, QueryTriggerInteraction.Ignore))
                {
                    continue;
                }
                if (collider.gameObject.layer == 3)
                {
                    PlayerControllerB playerControllerB = collider.gameObject.GetComponent<PlayerControllerB>();
                    if (playerControllerB != null && playerControllerB.IsOwner) {
                        float damage = Mathf.Lerp(minDamage, maxDamage, distance / damageRadius);
                        playerControllerB.DamagePlayer(Mathf.RoundToInt(damage));
                    }
                }
                else if (collider.gameObject.layer == 21)
                {
                    Landmine landmine = collider.gameObject.GetComponentInChildren<Landmine>();
                    if (landmine != null && !landmine.hasExploded && distance < damageRadius)
                    {
                        Plugin.mls.LogInfo("Setting off other mine");
                        landmine.ExplodeMineServerRpc();
                    }
                }
                else if (collider.gameObject.layer == 19)
                {
                    EnemyAICollisionDetect enemyAICollisionDetect = collider.gameObject.GetComponentInChildren<EnemyAICollisionDetect>();
                    if (enemyAICollisionDetect != null && enemyAICollisionDetect.mainScript.IsOwner && distance < damageRadius * 0.75f)
                    {
                        enemyAICollisionDetect.mainScript.HitEnemyOnLocalClient(6);
                    }
                }
            }

            int num3 = ~LayerMask.GetMask("Colliders");
            overlapColliders = Physics.OverlapSphere(explosionPosition, 10f, num3);
            for (int i = 0; i < overlapColliders.Length; i++)
            {
                Rigidbody rigidBody = overlapColliders[i].GetComponent<Rigidbody>();
                if (rigidBody != null)
                {
                    rigidBody.AddExplosionForce(70f, explosionPosition, 10f);
                }
            }
        }
    }

    [Serializable]
    public class SerializableVector3
    {
        public float x, y, z;

        public SerializableVector3(Vector3 vec3) { 
            x = vec3.x;
            y = vec3.y;
            z = vec3.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
