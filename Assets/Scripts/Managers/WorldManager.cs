using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : Singleton<WorldManager>
{
    public float Width = 100;
    public float Height = 100;

    public Vector3 DeadZoneStart = new Vector3(40, 0, 40);
    public Vector3 DeadZoneEnd = new Vector3(60, 0, 60);

    private void Awake()
    {
        PlaceProps();
        PlaceTimeBubbles();
        PlaceEnemies();
        PlaceShipPieces();
    }

    public void PlaceProps()
    {
        var props = GetProps();

        for (float w = -(Width / 2); w < Width / 2; w += Random.Range(0f, 5f))
        {
            for (float h = -(Height / 2); h < Height / 2; h += Random.Range(0f, 5f))
            {
                if (IsInDeadZone(w, h))
                {
                    continue;
                }

                if (Random.Range(0, 10) > 7)
                {
                    var chosenProp = GetResourceFromWeights();
                    var prop = props.FirstOrDefault(x => x.name == chosenProp);

                    Instantiate(prop, new Vector3(h, 0, w), Quaternion.identity);
                }
            }
        }
    }

    private void PlaceTimeBubbles()
    {
        var resources = GetTimeBubbles();

        for (int i = 0; i < 10; i += 1)
        {
            var chosenResource = GetResourceFromTimeBubbleWeights();
            var resource = resources.FirstOrDefault(x => x.name == chosenResource);
            var spawnPoint = new Vector3(Random.Range(-(Width / 2), Width / 2), 0, Random.Range(-(Height / 2), Height / 2));

            if (IsInDeadZone(spawnPoint.x, spawnPoint.y))
            {
                i--;
                continue;
            }

            Instantiate(resource, spawnPoint, Quaternion.identity);
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

            if (IsInDeadZone(spawnPoint.x, spawnPoint.y))
            {
                i--;
                continue;
            }

            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    private void PlaceShipPieces()
    {
        var enemies = GetShiPieces();

        for (int i = 0; i < GameManager.Instance.ShipPiecesToCollect; i += 1)
        {
            var chosenResource = GetResourceFromShipPieceWeights();
            var resourceToBeSpawned = enemies.FirstOrDefault(x => x.name == chosenResource);
            var spawnPoint = new Vector3(Random.Range(-(Width / 2), Width / 2), 0, Random.Range(-(Height / 2), Height / 2));

            if (IsInDeadZone(spawnPoint.x, spawnPoint.y))
            {
                i--;
                continue;
            }

            Instantiate(resourceToBeSpawned, spawnPoint, Quaternion.identity);
        }
    }

    private bool IsInDeadZone(float x, float z)
    {
        var startX = -(DeadZoneStart.x / 2);
        var startZ = -(DeadZoneStart.z / 2);
        var endX = DeadZoneEnd.x;
        var endZ = DeadZoneEnd.z;

        var deadZone = new Rect(startX, startZ, endX, endZ);

        return deadZone.Contains(new Vector2(x, z));
    }

    private GameObject[] GetProps()
    {
        return Resources.LoadAll<GameObject>("Props");
    }
    
    private GameObject[] GetTimeBubbles()
    {
        return Resources.LoadAll<GameObject>("Time Bubbles");
    }

    private GameObject[] GetEnemies()
    {
        return Resources.LoadAll<GameObject>("Enemies");
    }

    private GameObject[] GetShiPieces()
    {
        return Resources.LoadAll<GameObject>("Ship Pieces");
    }

    private string GetResourceFromWeights()
    {
        var weights = GetWeights();

        return GetGONameFromWeights(weights);
    }
    
    private string GetResourceFromTimeBubbleWeights()
    {
        var weights = GetTimeBubbleWeights();

        return GetGONameFromWeights(weights);
    }

    private string GetResourceFromEnemyWeights()
    {
        var weights = GetEnemyWeights();

        return GetGONameFromWeights(weights);
    }

    private string GetResourceFromShipPieceWeights()
    {
        var weights = GetShipPieceWeights();

        return GetGONameFromWeights(weights);
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

    private Dictionary<string, float> GetTimeBubbleWeights()
    {
        return new Dictionary<string, float>()
        {
            { "Time Bubble Fast", 50f },
            { "Time Bubble Slow", 50f }
        };
    }

    private Dictionary<string, float> GetEnemyWeights()
    {
        return new Dictionary<string, float>()
        {
            { "Caveman", 100f }
        };
    }

    private Dictionary<string, float> GetShipPieceWeights()
    {
        return new Dictionary<string, float>()
        {
            { "Ship Piece", 100f }
        };
    }

    private string GetGONameFromWeights(Dictionary<string, float> weights)
    {
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
}