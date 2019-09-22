using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{

    private Dictionary<Collider, ModuleSlot> m_ColliderToModuleDictionary = new Dictionary<Collider, ModuleSlot>();

    public Dictionary<Collider, Transform> m_ColliderToAnchorDictionary = new Dictionary<Collider, Transform>();

    [SerializeField]
    private List<Collider> m_ModuleColliders = new List<Collider>(3);
    [SerializeField]
    private List<Transform> m_ModuleAnchors = new List<Transform>(3);
    [SerializeField]
    private float m_OxygenDecayRate = 1f;
    private float m_OxygenPercent = 100f;
    protected Rigidbody m_Rigidbody;
    
    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            m_ColliderToAnchorDictionary.Add(m_ModuleColliders[i], m_ModuleAnchors[i]);
        }
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.angularVelocity = new Vector3(0, Random.Range(-1f, 1f), 0);
        m_Rigidbody.velocity = new Vector3(Random.Range(-.5f, .5f), 0, Random.Range(-.5f, .5f));
        m_ColliderToModuleDictionary = new Dictionary<Collider, ModuleSlot>();
        for (int i = 0; i < m_ModuleColliders.Count; i++)
        {
            m_ColliderToModuleDictionary.Add(m_ModuleColliders[i], new ModuleSlot(this));
        }
    }

    public Collider GetColliderFromModuleSlot(ModuleSlot ms)
    {
        return m_ColliderToModuleDictionary.FirstOrDefault(x => x.Value == ms).Key;
    }

    public ModuleSlot GetModuleSlotAtCollider(Collider c)
    {
        if (m_ColliderToModuleDictionary.ContainsKey(c))
        {
            return m_ColliderToModuleDictionary[c];
        }
        return null;
    }

    public bool AddModule(Collider c, Module m)
    {
        if (m_ColliderToModuleDictionary[c] == null)
        {
            m_ColliderToModuleDictionary[c].AddModule(m);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Module RemoveModuleAtCollider(Collider c)
    {
        ModuleSlot moduleToRemove = m_ColliderToModuleDictionary[c];
        m_ColliderToModuleDictionary[c] = null;
        return moduleToRemove.RemoveModule();
    }

    private void Update()
    {
        if (m_OxygenPercent > 0)
        {
            m_OxygenPercent = Mathf.Max(m_OxygenPercent - m_OxygenDecayRate, 0);
        }
    }

    public void AddOxygen(float amt)
    {
        m_OxygenPercent = Mathf.Min(m_OxygenPercent + amt, 100f);
    }

}


public class ModuleSlot
{
    private Module m_Module = null;
    private Ship m_Ship;
    public ModuleSlot(Ship s)
    {
        m_Ship = s;
    }

    //might be null
    public Module Module => m_Module;

    public void AddModule(Module m)
    {
        // TODO make this take time
        m_Module = m;
        Transform anchor = m_Ship.m_ColliderToAnchorDictionary[m_Ship.GetColliderFromModuleSlot(this)];
        m_Module.transform.parent = anchor;
        m_Module.transform.LookAt(m_Module.transform.position + anchor.forward);
        m_Module.transform.position = anchor.position;
        m_Module.GetComponent<Rigidbody>().isKinematic = true;
        m_Module.AttachToShip(m_Ship);
    }

    public Module RemoveModule()
    {
        // TODO make this take time
        Module m = m_Module;
        m_Module.RemoveFromShip();
        m_Module = null;
        return m;
    }

    public bool ModuleOccupied()
    {
        return m_Module != null;
    }
}
