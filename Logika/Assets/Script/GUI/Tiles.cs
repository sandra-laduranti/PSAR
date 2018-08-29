using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    Flower _flower;
    Ray ray;
    RaycastHit hit;

    GameObject objToSpawn;


    // Use this for initialization
    void Start()
    {
        _flower = null;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void attributeFlower()
    {
        if (_flower)
        {
            _flower._x = x;
            _flower._y = y;
        }
    }

    public void setFlower(Flower flower)
    {
        //float x, float y, float z)
        if (_flower)
        {
            if (_flower.isAttribuate())
            {
                return;
            }
        }
        if (flower)
        {
            _flower = flower;
            _flower.spawnFlower(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    public Flower getFlower()
    {
        if (_flower == null)
            return null;
        if (!_flower.isAttribuate())
            return null;
        return _flower;
    }

    void OnMouseDown()
    {
       // Debug.Log("tile: " + x + " : " + y + " CLICK");
        if (_flower)
        {
            TerrainHandler terrain = this.transform.parent.gameObject.GetComponent<TerrainHandler>();
            TerrainHandler.ToggleType mode = terrain.getMode();

            if (mode == TerrainHandler.ToggleType.Create)
            {
                _flower.setXY((float)x, (float)y);
                terrain.getJardin().Add(_flower.copyToElement());
            }
            if (mode == TerrainHandler.ToggleType.Delete)
            {
                if (_flower.isAttribuate())
                {
                    _flower.setXY(-42, -42);
                    _flower.destroyFlower(terrain.getJardin());
                }
            }
        }
    }
}
