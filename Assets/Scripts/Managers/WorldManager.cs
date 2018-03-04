using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : Singleton<WorldManager>
{
    public float Width = 100;
    public float Height = 100;

    public Vector3 DeadZoneStart = new Vector3(40, 0, 40);
    public Vector3 DeadZoneEnd = new Vector3(60, 0, 60);

    private void Awake()
    {
        PlaceProps();
        PlaceEnemies();
        PlaceShipPieces();

        FindObjectOfType<NavMeshSurface>().BuildNavMesh();

        //We do this here so they aren't included in the navmesh. #GameJamHacks
        PlaceTimeBubbles();
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

                if (Random.Range(0, 10) > 8)
                {
                    var chosenProp = GetResourceFromWeights();
                    var prop = props.FirstOrDefault(x => x.name == chosenProp.ResourceName);
                    
                    SpawnResourceItem(prop, chosenProp, new Vector3(h, 0, w));
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
            var resource = resources.FirstOrDefault(x => x.name == chosenResource.ResourceName);
            var spawnPoint = new Vector3(Random.Range(-(Width / 2), Width / 2), 0, Random.Range(-(Height / 2), Height / 2));

            if (IsInDeadZone(spawnPoint.x, spawnPoint.y))
            {
                i--;
                continue;
            }

            SpawnResourceItem(resource, chosenResource, spawnPoint);
        }
    }

    private void PlaceEnemies()
    {
        var enemies = GetEnemies();
        
        for (int i = 0; i < 10; i += 1)
        {
            var chosenEnemy = GetResourceFromEnemyWeights();
            var enemy = enemies.FirstOrDefault(x => x.name == chosenEnemy.ResourceName);
            var spawnPoint = new Vector3(Random.Range(-(Width / 2), Width / 2), 0, Random.Range(-(Height / 2), Height / 2));

            if (IsInDeadZone(spawnPoint.x, spawnPoint.y))
            {
                i--;
                continue;
            }

            SpawnResourceItem(enemy, chosenEnemy, spawnPoint);
        }
    }

    private void PlaceShipPieces()
    {
        var enemies = GetShiPieces();

        for (int i = 0; i < GameManager.Instance.ShipPiecesToCollect; i += 1)
        {
            var chosenResource = GetResourceFromShipPieceWeights();
            var resourceToBeSpawned = enemies.FirstOrDefault(x => x.name == chosenResource.ResourceName);
            var spawnPoint = new Vector3(Random.Range(-(Width / 2), Width / 2), 0, Random.Range(-(Height / 2), Height / 2));

            if (IsInDeadZone(spawnPoint.x, spawnPoint.y))
            {
                i--;
                continue;
            }

            SpawnResourceItem(resourceToBeSpawned, chosenResource, spawnPoint);
        }
    }

    private void SpawnResourceItem(GameObject resource, ResourceItem resourceItem, Vector3 location)
    {
        var newResource = Instantiate(resource, location, Quaternion.identity);
        newResource.transform.localScale = Vector3.one * Random.Range(resourceItem.MinScale, resourceItem.MaxScale);
        newResource.transform.rotation = Quaternion.Euler(GetRandomPointBetweenTwoVectors(resourceItem.MinRotation, resourceItem.MaxRotation));
    }

    private Vector3 GetRandomPointBetweenTwoVectors(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
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

    private ResourceItem GetResourceFromWeights()
    {
        var weights = GetWeights();

        return GetResourceItemFromWeights(weights);
    }
    
    private ResourceItem GetResourceFromTimeBubbleWeights()
    {
        var weights = GetTimeBubbleWeights();

        return GetResourceItemFromWeights(weights);
    }

    private ResourceItem GetResourceFromEnemyWeights()
    {
        var weights = GetEnemyWeights();

        return GetResourceItemFromWeights(weights);
    }

    private ResourceItem GetResourceFromShipPieceWeights()
    {
        var weights = GetShipPieceWeights();

        return GetResourceItemFromWeights(weights);
    }

    private ResourceItem[] GetWeights()
    {
        return new ResourceItem[]
        {
            new ResourceItem() { ResourceName = "Bush", Weight = 50f, MinScale = 0.5f, MaxScale = 1.2f, MinRotation = Vector3.zero, MaxRotation = new Vector3(0, 360, 0) },
            new ResourceItem() { ResourceName = "Tree", Weight = 50f, MinScale = 0.6f, MaxScale = 1.2f, MinRotation = Vector3.zero, MaxRotation = new Vector3(0, 360, 0) },
            new ResourceItem() { ResourceName = "Rock", Weight = 10f, MinScale = 0.5f, MaxScale = 1.2f, MinRotation = Vector3.zero, MaxRotation = new Vector3(360, 360, 360) },
            new ResourceItem() { ResourceName = "Fire", Weight = 10f, MinScale = 0.9f, MaxScale = 1.1f, MinRotation = Vector3.zero, MaxRotation = new Vector3(0, 360, 0) }
        };
    }

    private ResourceItem[] GetTimeBubbleWeights()
    {
        return new []
        {
            new ResourceItem() { ResourceName = "Time Bubble Fast", Weight = 50f },
            new ResourceItem() { ResourceName = "Time Bubble Slow", Weight = 50f }
        };
    }

    private ResourceItem[] GetEnemyWeights()
    {
        return new ResourceItem[]
        {
            new ResourceItem() { ResourceName = "Caveman", Weight = 100f, MinScale = 0.9f, MaxScale = 1.1f }
        };
    }

    private ResourceItem[] GetShipPieceWeights()
    {
        return new ResourceItem[]
        {
            new ResourceItem() { ResourceName = "Ship Piece", Weight = 100f }
        };
    }

    private ResourceItem GetResourceItemFromWeights(ResourceItem[] resourceItems)
    {
        var total = resourceItems.Sum(x => x.Weight);
        var target = Random.Range(0f, total);
        var current = 0f;

        foreach (var resource in resourceItems)
        {
            current += resource.Weight;

            if (current >= target)
            {
                return resource;
            }
        }

        return null;
    }
}

public class ResourceItem
{
    public string ResourceName = "";

    public float Weight = 100f;

    public float MinScale = 1f;
    public float MaxScale = 1f;

    public Vector3 MinRotation = Vector3.zero;
    public Vector3 MaxRotation = Vector3.zero;
}