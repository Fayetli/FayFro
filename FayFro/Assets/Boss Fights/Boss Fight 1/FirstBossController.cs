using System.Collections;
using UnityEngine;

class FirstBossController : DefaultBoss
{
    [SerializeField] private float _waitTime;

    [SerializeField] private VerticalMover _columns = null;//have one column[0] and four transforms for this column

    [SerializeField] private VerticalMover _spiresDown = null;
    [SerializeField] private HorizontalMover _spiresLeft = null;
    [SerializeField] private HorizontalMover _spiresRight = null;
    [SerializeField] private VerticalMover _spiresUp = null;
    [SerializeField] private VerticalMover _chapteredSpires = null;
    private VerticalMover[] _chapterSpires;

    [SerializeField] private GameObject _startBox = null;
    [SerializeField] private Transform _boxSpawnTransform = null;
    [SerializeField] private GameObject _boxPref = null;

    [SerializeField] private int _bossSecondStageHPValue = 0;
    [SerializeField] private int _bossThirdStageHPValue = 0;
    [SerializeField] private int _bossFourthStageHPValue = 0;

    [SerializeField] private Transform[] _symbolsTransform;//left - up - right

    [SerializeField] private LayerMask _boxLayer = 0;

    [SerializeField] private float _boxUpForce = 0.0f;

    //[SerializeField] private Transform _symbolTransform = null;
    [SerializeField] private GameObject _redSymbolPref = null;
    [SerializeField] private GameObject _yellowSymbolPref = null;
    [SerializeField] private GameObject _greenSymbolPref = null;

    [SerializeField] private GameObject _platform = null;

    private const float _circleRadius = 0.10f;

    private GameObject _column = null;

    private int _currentRepaetValue = 1;
    private float _waitTimeMultiplier = 1;
    private void Start()
    {
        _column = _columns.transform.GetChild(0).gameObject;
        _chapterSpires = new VerticalMover[_chapteredSpires.transform.childCount];
        for (int i = 0; i < _chapterSpires.Length; i++)
        {
            _chapterSpires[i] = _chapteredSpires.transform.GetChild(i).gameObject.GetComponent<VerticalMover>();
        }
    }
    public override void StartAttack()
    {
        StartCoroutine(Attacking());
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



    private void TryToForceBox()
    {
        Transform checkBoxTransform = _column.transform.GetChild(0).transform;
        Collider2D upBox = Physics2D.OverlapCircle(checkBoxTransform.position, _circleRadius, _boxLayer);
        if (upBox != null)
        {
            upBox.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, _boxUpForce));
        }
    }

    private void SpawnSymbol(Transform transform, GameObject symbol)
    {
        GameObject currentSymbol = Instantiate(symbol);
        currentSymbol.transform.position = new Vector3(transform.position.x, transform.position.y, currentSymbol.transform.position.z);
    }
    private void SpawnSymbol(Transform transform, GameObject symbol, float y)
    {
        GameObject currentSymbol = Instantiate(symbol);
        currentSymbol.transform.position = new Vector3(transform.position.x, y, currentSymbol.transform.position.z);
    }

    public override void TakeDamage()
    {
        _hp--;
    }

    public override int GetHP()
    {
        return _hp;
    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(_waitTime);

        //_Spires down
        SpawnSymbol(_spiresDown.transform.GetChild(0).gameObject.transform, _redSymbolPref, _startBox.transform.position.y);
        SpawnSymbol(_spiresDown.transform.GetChild(1).gameObject.transform, _redSymbolPref, _startBox.transform.position.y);

        yield return new WaitForSeconds(3.0f);

        yield return StartCoroutine(_spiresDown.MovingUpCoroutine());

        for (int j = 0; j < _chapterSpires.Length; j++)
        {
            Transform currentTransform = _chapterSpires[j].gameObject.transform;
            SpawnSymbol(currentTransform, _redSymbolPref);
        }

        yield return StartCoroutine(_chapteredSpires.MovingUpCoroutine());

        while (_hp > 0)
        {
            Debug.Log("Boss attacked! Hp: " + _hp);

            if (_hp < _bossFourthStageHPValue)
            {
                _currentRepaetValue += 1;
            }


            for (int j = 0; j < _currentRepaetValue; j++)
            {
                yield return new WaitForSeconds(1f * _waitTimeMultiplier);


                {
                    SpawnSymbol(_symbolsTransform[0], _redSymbolPref);
                    SpawnSymbol(_symbolsTransform[2], _redSymbolPref);

                    StartCoroutine(_spiresRight.MovingLeftCoroutine());
                    yield return StartCoroutine(_spiresLeft.MovingRightCoroutine());



                    SpawnSymbol(_symbolsTransform[1], _redSymbolPref);

                    StartCoroutine(_spiresRight.MovingRightCoroutine());
                    yield return StartCoroutine(_spiresLeft.MovingLeftCoroutine());



                    yield return StartCoroutine(_spiresUp.MovingDownCoroutine());
                    yield return StartCoroutine(_spiresUp.MovingUpCoroutine());
                }

                if (_hp <= _bossSecondStageHPValue)
                {
                    int[] skipSpire = { 3, 1, 2 };
                    for (int i = 0; i < skipSpire.Length; i++)
                    {
                        for (int l = 0; l < _chapterSpires.Length; l++)
                        {
                            if (l == skipSpire[i] - 1)
                                continue;

                            SpawnSymbol(_symbolsTransform[l], _redSymbolPref);
                        }
                        for (int l = 0; l < _chapterSpires.Length; l++)
                        {
                            if (l == skipSpire[i] - 1)
                                continue;

                            StartCoroutine(_chapterSpires[l].MovingUpCoroutine());

                        }

                        yield return new WaitForSeconds(2.5f * _waitTimeMultiplier);

                        for (int l = 0; l < _chapterSpires.Length; l++)
                        {
                            if (l == skipSpire[i] - 1)
                                continue;

                            StartCoroutine(_chapterSpires[l].MovingDownCoroutine());

                        }

                        yield return new WaitForSeconds(2.5f * _waitTimeMultiplier);
                    }


                }

                if (_hp < _bossThirdStageHPValue)
                {

                    StartCoroutine(_chapterSpires[1].gameObject.transform.GetChild(1).gameObject.GetComponent<VerticalMover>().MovingUpCoroutine());


                    SpawnSymbol(_symbolsTransform[0], _redSymbolPref);
                    SpawnSymbol(_symbolsTransform[2], _redSymbolPref);

                    StartCoroutine(_spiresRight.MovingLeftCoroutine());
                    yield return StartCoroutine(_spiresLeft.MovingRightCoroutine());

                    yield return new WaitForSeconds(1.0f);

                    StartCoroutine(_chapterSpires[1].gameObject.transform.GetChild(1).gameObject.GetComponent<VerticalMover>().MovingDownCoroutine());
                    StartCoroutine(_chapterSpires[0].gameObject.transform.GetChild(1).gameObject.GetComponent<VerticalMover>().MovingUpCoroutine());
                    StartCoroutine(_chapterSpires[2].gameObject.transform.GetChild(1).gameObject.GetComponent<VerticalMover>().MovingUpCoroutine());



                    StartCoroutine(_spiresRight.MovingRightCoroutine());
                    StartCoroutine(_spiresLeft.MovingLeftCoroutine());

                    yield return new WaitForSeconds(2.0f);

                    SpawnSymbol(_symbolsTransform[1], _redSymbolPref);

                    yield return new WaitForSeconds(1.0f);

                    yield return StartCoroutine(_spiresUp.MovingDownCoroutine());
                    yield return StartCoroutine(_spiresUp.MovingUpCoroutine());

                    StartCoroutine(_chapterSpires[0].gameObject.transform.GetChild(1).gameObject.GetComponent<VerticalMover>().MovingDownCoroutine());
                    StartCoroutine(_chapterSpires[2].gameObject.transform.GetChild(1).gameObject.GetComponent<VerticalMover>().MovingDownCoroutine());





                }


                _platform.GetComponent<Collider2D>().isTrigger = true;



                yield return new WaitForSeconds(1f * _waitTimeMultiplier);
                //yellow symbols, pause, destroy
                //box down

                GameObject box = Instantiate(_boxPref);
                box.transform.position = new Vector2(_boxSpawnTransform.position.x, _boxSpawnTransform.position.y);

                yield return new WaitForSeconds(1f * _waitTimeMultiplier);

                int randomColumnTransformNum = Random.Range(1, _columns.transform.childCount);
                _column.gameObject.transform.position = _columns.transform.GetChild(randomColumnTransformNum).transform.position;
                {
                    GameObject tempSymbol = Instantiate(_greenSymbolPref);
                    tempSymbol.transform.position = new Vector3(_column.transform.position.x, 0.65f, tempSymbol.transform.position.z);
                }

                yield return new WaitForSeconds(2.5f * _waitTimeMultiplier);
                //green symbols, pause, destroy
                //column up

                yield return StartCoroutine(_columns.MovingUpCoroutine());

                _platform.GetComponent<Collider2D>().isTrigger = false;

                //TryToForceBox();

                yield return StartCoroutine(_columns.MovingDownCoroutine());


                yield return new WaitForSeconds(1f * _waitTimeMultiplier);

                DestroyLateBoxes();

                _waitTimeMultiplier -= 0.01f;
            }




        }




    }
}


