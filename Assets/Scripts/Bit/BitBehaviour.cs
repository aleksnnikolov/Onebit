using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BitBehaviour : MonoBehaviour
{
    AudioManager audioManager;
    GameManager gameManager;
    BitPhysics bitPhysics;
    ArcRenderer arcRenderer;

    public GameObject powerAura;
    public GameObject proximityAura;

    public GameObject holderUnit;
    public GameObject controlledUnit;
    public GameObject lastControlledUnit;

    public bool canThrow = false;

    public bool allControllable = false;
    public bool isHolded = false;
    public List<GameObject> poweredUnits = new List<GameObject>();
    public List<GameObject> controllableUnits = new List<GameObject>();
    public List<GameObject> closeUnits = new List<GameObject>();
    public Vector3 bitSpeed;

    public GameObject destroyedBitPrefab;

    bool readyToThrow = false;

    Rigidbody2D rb;
    Animator anim;

    public ParticleSystem death_fx; 
    public ParticleSystem death_fx2;
    public ParticleSystem launch_fx;
    public ParticleSystem idle_fx;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bitPhysics = GetComponent<BitPhysics>();
        arcRenderer = GetComponentInChildren<ArcRenderer>();

        if (GameManager.GetCurrentSceneIndex() >= 4 || GameManager.GetCurrentSceneIndex() == 0) {
            canThrow = true;
        }
    }

    private void Start()
    {
        SelectControllableUnit();
        lastControlledUnit = controlledUnit;
        Invoke("UpdateControlledUnit", 0.1f);
    }

    void Update()
    {
        //pickup from ground
        if (Input.GetKeyDown(KeyCode.E) && !isHolded && closeUnits.Contains(controlledUnit) && !GameManager.inputBlocked)
        {
            isHolded = true;
            holderUnit = controlledUnit;
            transform.parent = holderUnit.transform.Find("bitpos");
            transform.localPosition = new Vector2(0f, 0f);
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            launch_fx = holderUnit.GetComponent<CrawlerMovement>().launch_fx;
            audioManager.Play("pickup", 0f);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isHolded && !GameManager.inputBlocked)
        {
            //pickup from another holder
            if (!controlledUnit.Equals(holderUnit) && closeUnits.Contains(controlledUnit))
            {
                arcRenderer.Deactivate();
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                holderUnit = controlledUnit;
                transform.parent = holderUnit.transform.Find("bitpos");
                transform.localPosition = new Vector2(0f, 0f);
                launch_fx = holderUnit.GetComponent<CrawlerMovement>().launch_fx;
                audioManager.Play("pickup", 0f);
            }
            //do nothing so non controlled units don't accidentally pick up bit
            else if (!controlledUnit.Equals(holderUnit))
            {

            }
            //drop bit
            else
            {
                arcRenderer.Deactivate();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isHolded = false;
                transform.parent = null;
                bitPhysics.PassiveThrow(holderUnit.GetComponent<Rigidbody2D>().velocity);
                holderUnit = null;
                transform.localScale = Vector3.one;
                audioManager.Play("pickup", 0f);
            }
        }

        //changes controlled unit
        if(Input.GetKeyDown(KeyCode.F) && !GameManager.inputBlocked) {
            if (allControllable)
                NoControllable();

            if (controlledUnit != null)
                controlledUnit.GetComponent<PlayerPM>().IsNotControlled();

             SelectControllableUnit();
        }

        //toggles control all mode
        if(Input.GetKeyDown(KeyCode.LeftControl) && !GameManager.inputBlocked) {
            if (!allControllable)
            {
                NoControllable();
                AllControllable();
            }
            else
            {
                NoControllable();
            }
        }

        if (isHolded && holderUnit == controlledUnit && Input.GetKeyDown(KeyCode.Mouse0) && canThrow && !GameManager.inputBlocked) {
            arcRenderer.Activate();
            readyToThrow = true;
        }

        if (isHolded && holderUnit == controlledUnit && Input.GetKeyUp(KeyCode.Mouse0) && canThrow && !GameManager.inputBlocked && readyToThrow) {
            arcRenderer.Deactivate();

            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isHolded = false;
            transform.parent = null;
            holderUnit = null;
            transform.localScale = Vector3.one;
            launch_fx.Play();

            Vector3 throwDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 dir2D = new Vector2(throwDir.x, throwDir.y);
            audioManager.Play("bitThrow", 0f);
            bitPhysics.ActiveThrow(dir2D);

            readyToThrow = false;
        }

        //Update bitSpeed for the CameraMovement script
        if (!isHolded)
            bitSpeed = rb.velocity;
        else
            bitSpeed = holderUnit.GetComponent<Rigidbody2D>().velocity;

        if (controllableUnits.Count == 0) {
            audioManager.Stop("crawlerWalk");
        }
    }

    //updates controlledUnit in case of the previous one leaving the aura
    public void UpdateControlledUnit() {
        if (!controllableUnits.Contains(controlledUnit) && controllableUnits.Count > 0)
            SelectControllableUnit();
        else if (!controllableUnits.Contains(controlledUnit) && controllableUnits.Count == 0)
            controlledUnit = null;
    }

    //selects another controlledUnit if any are present in the controllableUnit list
    public void SelectControllableUnit() {

        if (controllableUnits.Count > 0) {
            int targetIndex = 0;

            if (controlledUnit == null)
            {
                targetIndex = 0;
            }
            else
            {
                targetIndex = controllableUnits.IndexOf(controlledUnit) + 1;
                if (targetIndex == controllableUnits.Count)
                    targetIndex = 0;
            }

            controlledUnit = controllableUnits[targetIndex];
            lastControlledUnit = controlledUnit;
            controlledUnit.GetComponent<PlayerPM>().IsControlled();

            for (int i = 0; i < controllableUnits.Count; i++) {
                if (controllableUnits[i] != controlledUnit)
                    controllableUnits[i].GetComponent<PlayerPM>().IsNotControlled();
            }
        } else {
            controlledUnit = null;
        }

    }

    public void AllControllable() {
        lastControlledUnit = controlledUnit;

        for (int i = 0; i < controllableUnits.Count; i++)
        {
            controllableUnits[i].GetComponent<PlayerPM>().IsControlled();
        }
        allControllable = true;
    }

    public void NoControllable() {
        for (int i = 0; i < controllableUnits.Count; i++)
        {
            controllableUnits[i].GetComponent<PlayerPM>().IsNotControlled();
        }
        controlledUnit = lastControlledUnit;
        controlledUnit.GetComponent<PlayerPM>().IsControlled();
        allControllable = false;
    }

    //Used in occasions as loading game
    public void ForceDrop() {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isHolded = false;
        transform.parent = null;
        holderUnit = null;
        transform.localScale = Vector3.one;
    }

    public void CallDestroyBit() {
        StartCoroutine(DestroyBit());
    }

    IEnumerator DestroyBit() {
        gameManager.canReload = false;
        if(isHolded)
            ForceDrop();
        rb.simulated = false;
        anim.SetTrigger("Destroy");

        //Shakey
        float speed = 80f;
        float amount = 0.015f;
        float i = 0;
        while (i < 1.0f) {
            float offset = Mathf.Sin(Time.time * speed) * amount;
            amount -= 0.00002f;
            if (amount <= 0.005f)
                amount = 0.005f;
            transform.position += new Vector3(offset, 0, 0);
            i += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().enabled = false;
        arcRenderer.Deactivate();
        GameObject destroyedBit = Instantiate(destroyedBitPrefab, transform.position, Quaternion.identity) as GameObject;
        death_fx.Play();
        death_fx2.Play();
        audioManager.Play("bitDeath", 0f);
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(gameManager.restartFromCheckPoint());
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Respawn");
        GetComponent<SpriteRenderer>().enabled = true;
        
    }

    //List management
    public void AddPoweredUnit(GameObject unit)
    {
        poweredUnits.Add(unit);
        
    }

    public void RemovePoweredUnit(GameObject unit)
    {
        poweredUnits.Remove(unit);
    }

    public void AddControllableUnit(GameObject unit)
    {
        controllableUnits.Add(unit);
    }

    public void RemoveControllableUnit(GameObject unit)
    {
        controllableUnits.Remove(unit);
    }

    public void AddCloseUnit(GameObject unit)
    {
        closeUnits.Add(unit);
    }

    public void RemoveCloseUnit(GameObject unit)
    {
        closeUnits.Remove(unit);
    }


}
