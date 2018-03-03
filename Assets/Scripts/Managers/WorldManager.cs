using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : Singleton<WorldManager>
{
    public float Width = 100;
    public float Height = 100;

    private void Awake()
    {
        PlaceProps();
        PlaceEnemies();
    }

    public void PlaceProps()
    {
        var props = GetProps();

        for (float w = -(Width / 2); w < Width / 2; w += Random.Range(0f, 5f))
        {
            for (float h = -(Height / 2); h < Height / 2; h += Random.Range(0f, 5f))
            {
                if (Random.Range(0, 10) > 7)
                {
                    var chosenProp = GetResourceFromWeights();
                    var prop = props.FirstOrDefault(x => x.name == chosenProp);

                    Instantiate(prop, new Vector3(h, 0, w), Quaternion.identity);
                }
            }
        }
    }

    private void PlaceEnemies()
    {
        var enemies = GetEnemies();
        
        for (int i = 0; i < 10; i += 1)
        {
            var chosenEnemy = GetResourceFromEnemyWeights();
            var enemy = enemies.FirstOrDefault(x => x.name == chosenEnemy);
            var spawnPoint = new Vector3(Random.Range(-(Width / 2), Width / 2), 0, Random.Range(-(Height / 2), Height / 2));

            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    private GameObject[] GetProps()
    {
        return Resources.LoadAll<GameObject>("Props");
    }

    private GameObject[] GetEnemies()
    {
        return Resources.LoadAll<GameObject>("Enemies");
    }

    private string GetResourceFromWeights()
    {
        var weights = GetWeights();
        var total = weights.Sum(x => x.Value);
        var target = Random.Range(0f, total);
        var current = 0f;

        foreach (var weight in weights)
        {
            current += weight.Value;

            if (current >= target)
            {
                return weight.Key;
            }
        }

        return null;
    }

    private string GetResourceFromEnemyWeights()
    {
        var weights = GetEnemyWeights();
        var total = weights.Sum(x => x.Value);
        var target = Random.Range(0f, total);
        var current = 0f;

        foreach (var weight in weights)
        {
            current += weight.Value;

            if (current >= target)
            {
                return weight.Key;
            }
        }

        return null;
    }

    private Dictionary<string, float> GetWeights()
    {
        return new Dictionary<string, float>()
        {
            { "Bush", 50f },
            { "Tree", 50f },
            { "Rock", 50f },
            { "Fire", 10f }
        };
    }

    private Dictionary<string, float> GetEnemyWeights()
    {
        return new Dictionary<string, float>()
        {
            { "Caveman", 100f }
        };
    }
}