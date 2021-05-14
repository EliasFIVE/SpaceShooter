using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private Vector2 rotMinMax = new Vector2(15, 90);
    [SerializeField] private Vector2 driftMinMax = new Vector2(.25f, 2);
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private float fadeTime = 4f;

    [Header("Set Dynamically")]
    [SerializeField] private Item_SO item;
    private GameObject cube;
    private TextMesh letter;
    private Vector3 rotPerSecond;
    private float birthTime;

    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Renderer cubeRend;

    private void Awake()
    {
        cube = transform.Find("Cube").gameObject;
        letter = GetComponent<TextMesh>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeRend = cube.GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        Vector3 vel = Random.onUnitSphere;
        vel.z = 0;
        vel.Normalize();
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;

        transform.rotation = Quaternion.identity;

        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y), Random.Range(rotMinMax.x, rotMinMax.y), Random.Range(rotMinMax.x, rotMinMax.y));

        birthTime = Time.time;

/*        cubeRend.material.color = item.color;
        letter.text = item.letter;*/
    }

    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        //Effect of the cube dissolving over time
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }

        if (u > 0)
        {
            Color c = cubeRend.material.color;
            c.a = 1f - u;
            cubeRend.material.color = c;
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }

        if (!bndCheck.IsOnScreen)
        {
            Destroy(gameObject);
        }
    }

    public void SetType(Item_SO newItem)
    {
        item = newItem;
        cubeRend.material.color = item.color;
        letter.text = item.letter;
    }

/*    private void OnCollisionEnter(Collision collision)
    {
        AbsorbePowerUp(collision.gameObject);
    }*/

   private void OnTriggerEnter(Collider other)
    {
        ShipController shipController = other.GetComponentInParent<ShipController>();

        if (!shipController.stats.shipDefinition.isPlayer)
            return;

        AbsorbePowerUp(shipController);
    }

    public void AbsorbePowerUp(ShipController ship)
    {
        ShipStats shipStats = ship.stats;

        switch (item.type)
        {
            case ItemType.shield:
                ShieldController shield = ship.gameObject.GetComponentInChildren<ShieldController>();
                shield.PowerUpShield();
                break;

            case ItemType.weapon:
                Weapon_SO newWeapon = (Weapon_SO)item;
                if(shipStats.GetActiveWeapon() != newWeapon.weaponType)
                    ship.ClearWeapons();
                ship.ActivateFirstInactiveWeapon(newWeapon.weaponType);
                break;
        }

        Destroy(gameObject);
    }
}
