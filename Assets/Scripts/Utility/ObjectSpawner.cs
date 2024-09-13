using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeedMyKids1.Utilities
{

    public class ObjectSpawner : MonoBehaviour
    {
        /// <summary>
        /// A way to give an array of prefabs preferential distribution.
        /// </summary>
        [System.Serializable]
        internal class WeightedPrefab
        {
            [SerializeField] internal GameObject prefab = null;
            [SerializeField, Range(0f, 1f),
                Tooltip("1.0 means all objects will be this, 0.0 that none will be")]
            internal float weight = 1.0f;

            [Space, Header("Randomization")]
            [SerializeField] internal Vector3 randRotation;

            [SerializeField] internal ParticleSystem.MinMaxCurve xScaleRandomization;
            [SerializeField] internal ParticleSystem.MinMaxCurve yScaleRandomization;
            [SerializeField] internal ParticleSystem.MinMaxCurve zScaleRandomization;
        }

        [System.Serializable]
        public struct SpawnConditions
        {
            internal Vector3 spawnPos;
            internal float radius;
            internal Vector3 randRot;
            internal ParticleSystem.MinMaxCurve x_randScale;
            internal ParticleSystem.MinMaxCurve y_randScale;
            internal ParticleSystem.MinMaxCurve z_randScale;

            public SpawnConditions(Vector3 spawnPos, float radius, Vector3 randRot,
                ParticleSystem.MinMaxCurve x_randScale, ParticleSystem.MinMaxCurve y_randScale, ParticleSystem.MinMaxCurve z_randScale)
            {
                this.spawnPos = spawnPos;
                this.radius = radius;
                this.randRot = randRot;
                this.x_randScale = x_randScale;
                this.y_randScale = y_randScale;
                this.z_randScale = z_randScale;
            }
        }

        [SerializeField] WeightedPrefab[] weightedPrefabs = new WeightedPrefab[0];

        [SerializeField] private int numObjectsToSpawn = 5;
        public static void SpawnObjects(int numToSpawn, GameObject[] prefabArray, float[] weights, SpawnConditions spawnConditions, Transform parent = null)
        {
            if (parent == null)
                //Create a parent for organizing the scene (and quick deletion)
                parent = new GameObject($"Spawned objects (Time Stamp: {System.DateTime.Now})").transform;

            for (int i = 0; i < numToSpawn; i++)
            {
                GameObject prefab = prefabArray[0];

                //Make a random percentage
                float rand = Random.Range(0.0f, 1.0f);

                //Pick one thats closest to and higher than the percentage
                for (int j = 0; j < weights.Length; j++)
                {
                    if (weights[j] > rand)
                    {
                        prefab = prefabArray[j];
                    }
                }

                //Randomize Rot
                Quaternion randRot = Vec3_Utils.RandomizedRotation(spawnConditions.randRot);

                //Gen position
                Vector2 horzRand = Random.insideUnitCircle * spawnConditions.radius;

                Vector3 pos = spawnConditions.spawnPos + new Vector3(horzRand.x, 0f, horzRand.y);

                GameObject spawned = Instantiate(prefab, pos, randRot);

                //Randomize Scale            
                Vec3_Utils.RandomizedVector3(spawnConditions.x_randScale.constantMin, spawnConditions.x_randScale.constantMax,
                spawnConditions.y_randScale.constantMin, spawnConditions.y_randScale.constantMax,
                spawnConditions.z_randScale.constantMin, spawnConditions.z_randScale.constantMax);

                spawned.transform.parent = parent;
            }
        }

        /// <summary>
        /// Spawn from this instance's field values.
        /// </summary>
        /// <param name="radius"></param>
        internal void SpawnPrefabs(float radius)
        {
            //Create a parent for organizing the scene (and quick deletion)
            Transform parent = new GameObject($"Spawned objects (Time Stamp: {System.DateTime.Now})").transform;

            //For Each of the objects 
            for (int i = 0; i < weightedPrefabs.Length; i++)
            {
                var toSpawn = weightedPrefabs[i];

                int numToSpawnOfType = (int)((float)numObjectsToSpawn * toSpawn.weight);

                GameObject[] spawned = new GameObject[numObjectsToSpawn];

                //Instantiate them all
                for (int j = 0; j < numToSpawnOfType; j++)
                {
                    spawned[j] = Instantiate(toSpawn.prefab);
                }

                //Randomize Rot for all
                for (int j = 0; j < numToSpawnOfType; j++)
                {
                    spawned[j].transform.rotation = Vec3_Utils.RandomizedRotation(toSpawn.randRotation);
                }

                //Gen position for all
                for (int j = 0; j < numToSpawnOfType; j++)
                {
                    Vector2 horzRand = Random.insideUnitCircle * spawnRadius;

                    Vector3 pos = transform.position + new Vector3(horzRand.x, 0f, horzRand.y);

                    spawned[j].transform.position = pos;
                }


                //Randomize Scale for all
                for (int j = 0; j < numToSpawnOfType; j++)
                {
                    spawned[j].transform.localScale = Vec3_Utils.RandomizedVector3(toSpawn.xScaleRandomization, toSpawn.yScaleRandomization, toSpawn.zScaleRandomization);
                }

                //Give them parents
                for (int j = 0; j < numToSpawnOfType; j++)
                {
                    spawned[j].transform.parent = parent;
                }
            }
        }

        [SerializeField] private float spawnRadius = 100f;
        [SerializeField] bool spawn = false;

        private void OnValidate()
        {
            if (spawn)
            {
                spawn = false;

                SpawnPrefabs(spawnRadius);
            }

            SortPrefabs();
        }

        private void SortPrefabs()
        {
            for (int i = 0; i < weightedPrefabs.Length - 1; i++)
            {
                for (int j = 0; j < weightedPrefabs.Length - i - 1; j++)
                {
                    if (weightedPrefabs[j].weight > weightedPrefabs[j + 1].weight)
                    {
                        var temp = weightedPrefabs[j];
                        weightedPrefabs[j] = weightedPrefabs[j + 1];
                        weightedPrefabs[j + 1] = temp;
                    }
                }
            }
        }
    }
}
