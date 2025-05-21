using SharedScripts;
using UnityEngine;

public class MissileLauncher : MonoBehaviour,IFireable
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField]private int maxMissileStored = 5;
    [SerializeField]private float reloadTime = 2f;
    private int _currentMissileStored;
    private float _reloadTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentMissileStored= maxMissileStored;
    }

    // Update is called once per frame
    void Update()
    {
        _reloadTimer+= Time.deltaTime;
        if (_reloadTimer >= reloadTime && _currentMissileStored < maxMissileStored)
        {
            _currentMissileStored++;
            _reloadTimer = 0;
        }
    }

    public void FireStart()
    {
        if (_currentMissileStored > 0)
        {
            var missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            _currentMissileStored--;
        }
    }

    public void FireStop()
    {
    }
}
