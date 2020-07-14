using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FirstBossController : DefaultBoss
{

    [SerializeField] private VerticalMover _columns = null;//have one column[0] and four transforms for this column

    [SerializeField] private VerticalMover _spires = null;
    [SerializeField] private VerticalMover _spires2 = null;
    [SerializeField] private VerticalMover _spires3 = null;
    [SerializeField] private VerticalMover _spires4 = null;

    [SerializeField] private GameObject _boxes = null;
    [SerializeField] private GameObject _boxPref = null;

    [SerializeField] private int _bossSecondStageHPValue = 0;
    [SerializeField] private int _bossThirdStageHPValue = 0;
    [SerializeField] private int _bossFourthStageHPValue = 0;

    [SerializeField] private LayerMask _boxLayer = 0;

    [SerializeField] private float _boxUpForce = 0.0f;

    //[SerializeField] private Transform _symbolTransform = null;
    [SerializeField] private GameObject _redSymbolPref = null;
    [SerializeField] private GameObject _yellowSymbolPref = null;
    [SerializeField] private GameObject _greenSymbolPref = null;

    private const float _circleRadius = 0.10f;

    private GameObject _column = null;

    private int currentRepaetValue = 1;
    private void Start()
    {
        _column = _columns.transform.GetChild(0).gameObject;
        StartAttack();
    }
    public override void StartAttack()
    {
        StartCoroutine("Attacking");
    }



    public override void FinishAttack()
    {

    }

    private void DestroyLateBoxes()
    {
        GameObject[] lateBoxes = GameObject.FindGameObjectsWithTag("box_attack_boss");
        foreach (var box in lateBoxes)
        {
            Destroy(box);
        }
    }

    private void SpawnBoxes()
    {
        int boxesCount = _boxes.transform.childCount;
        int randomCollidBox = Random.Range(1, boxesCount - 1);
        for (int i = 0; i < boxesCount; i++)
        {
            GameObject boxOnScene = Instantiate(_boxPref, _boxes.transform.GetChild(i).transform);
            if (i == randomCollidBox)
            {
                boxOnScene.GetComponent<Collider2D>().isTrigger = false;
            }
        }
    }

    private void TryToForceBox()
    {
        Transform checkBoxTransform = _column.transform.GetChild(0).transform;
        Collider2D upBox = Physics2D.OverlapCircle(checkBoxTransform.position, _circleRadius, _boxLayer);
        if (upBox != null)
        {
            Debug.Log("upBox");
            upBox.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, _boxUpForce));
        }
    }
    private IEnumerator Attacking()
    {


        while (hp != 0)
        {
            DestroyLateBoxes();
            //pause

            //red symbols, pause, destory symbols
            //spires up
            if(hp < _bossFourthStageHPValue)
            {
                currentRepaetValue += 1;
            }


            for (int j = 0; j < currentRepaetValue; j++) {

                for (int i = 0; i < _spires.transform.childCount; i++)
                {
                    GameObject tempSymbol = Instantiate(_redSymbolPref);
                    tempSymbol.transform.position = new Vector3(_spires.transform.GetChild(i).transform.position.x, 0.65f, tempSymbol.transform.position.z);
                }


                yield return new WaitForSeconds(1f);

                int spires3Count = _spires3.transform.childCount;
                int randDeactiveSpire3 = Random.Range(0, spires3Count);
                _spires3.transform.GetChild(randDeactiveSpire3).gameObject.SetActive(false);

                int spires4Count = _spires4.transform.childCount;
                int randDeactiveSpire4 = Random.Range(0, spires4Count);
                _spires4.transform.GetChild(randDeactiveSpire4).gameObject.SetActive(false);


                yield return StartCoroutine(_spires.MovingUpCoroutine());

                if (hp < _bossSecondStageHPValue)
                {
                    for (int i = 0; i < _spires2.transform.childCount; i++)
                    {
                        GameObject tempSymbol = Instantiate(_redSymbolPref);
                        tempSymbol.transform.position = new Vector3(_spires2.transform.GetChild(i).transform.position.x, 0.65f, tempSymbol.transform.position.z);
                    }
                    yield return new WaitForSeconds(0.25f);
                }

                yield return StartCoroutine(_spires.MovingDownCoroutine());

                if (hp < _bossSecondStageHPValue)
                {
                    yield return StartCoroutine(_spires2.MovingDownCoroutine());
                    if (hp < _bossThirdStageHPValue)
                    {
                        for (int i = 0; i < _spires3.transform.childCount; i++)
                        {
                            if (_spires3.transform.GetChild(i).gameObject.activeInHierarchy)
                            {
                                GameObject tempSymbol = Instantiate(_redSymbolPref);
                                tempSymbol.transform.position = new Vector3(_spires3.transform.GetChild(i).transform.position.x, 0.65f, tempSymbol.transform.position.z);
                            }
                        }
                    }
                    yield return StartCoroutine(_spires2.MovingUpCoroutine());
                }

                if (hp < _bossThirdStageHPValue)
                {


                    yield return StartCoroutine(_spires3.MovingUpCoroutine());

                    for (int i = 0; i < _spires4.transform.childCount; i++)
                    {
                        if (_spires4.transform.GetChild(i).gameObject.activeInHierarchy)
                        {
                            GameObject tempSymbol = Instantiate(_redSymbolPref);
                            tempSymbol.transform.position = new Vector3(_spires4.transform.GetChild(i).transform.position.x, 0.65f, tempSymbol.transform.position.z);
                        }
                    }

                    yield return StartCoroutine(_spires3.MovingDownCoroutine());


                    yield return new WaitForSeconds(1f);


                    yield return StartCoroutine(_spires4.MovingDownCoroutine());
                    yield return StartCoroutine(_spires4.MovingUpCoroutine());

                }
                _spires3.transform.GetChild(randDeactiveSpire3).gameObject.SetActive(true);
                _spires4.transform.GetChild(randDeactiveSpire4).gameObject.SetActive(true);
            }


            for (int i = 0; i < _boxes.transform.childCount; i++)
            {
                GameObject tempSymbol = Instantiate(_yellowSymbolPref);
                tempSymbol.transform.position = new Vector3(_boxes.transform.GetChild(i).transform.position.x, 0.65f, tempSymbol.transform.position.z);
            }

            yield return new WaitForSeconds(1f);
            //yellow symbols, pause, destroy
            //box down

            SpawnBoxes();

            yield return new WaitForSeconds(1f);


            int randomColumnTransformNum = Random.Range(1, _columns.transform.childCount);
            _column.gameObject.transform.position = _columns.transform.GetChild(randomColumnTransformNum).transform.position;
            {
                GameObject tempSymbol = Instantiate(_greenSymbolPref);
                tempSymbol.transform.position = new Vector3(_column.transform.position.x, 0.65f, tempSymbol.transform.position.z);
            }

            yield return new WaitForSeconds(2f);
            //green symbols, pause, destroy
            //column up
            
            yield return StartCoroutine(_columns.MovingUpCoroutine());

            TryToForceBox();

            yield return StartCoroutine(_columns.MovingDownCoroutine());



            yield return new WaitForSeconds(1f);
            hp--;
        }

        DestroyLateBoxes();



    }




}


