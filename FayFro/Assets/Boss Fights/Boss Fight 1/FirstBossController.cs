using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FirstBossController : DefaultBoss
{

    [SerializeField] private LinearMover _columns = null;

    [SerializeField] private LinearMover _spires = null;
    [SerializeField] private LinearMover _spires2 = null;
    [SerializeField] private LinearMover _spires3 = null;
    [SerializeField] private LinearMover _spires4 = null;

    [SerializeField] private GameObject _boxes = null;

    [SerializeField] private int _bossSecondStageHPValue = 0;
    [SerializeField] private int _bossThirdStageHPValue = 0;

    [SerializeField] private LayerMask _boxLayer;

    [SerializeField] private float _boxUpForce;

    private const float _circleRadius = 0.10f;

    private GameObject _column = null;
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

    private IEnumerator Attacking()
    {


        while (hp != 0)
        {
            GameObject[] lateBoxes = GameObject.FindGameObjectsWithTag("box_attack_boss");
            foreach (var box in lateBoxes)
            {
                Destroy(box);
            }
            //pause

            //red symbols, pause, destory symbols
            //spires up
            yield return StartCoroutine(_spires.MovingUpCoroutine());
            yield return StartCoroutine(_spires.MovingDownCoroutine());

            if (hp < _bossSecondStageHPValue)
            {
                yield return StartCoroutine(_spires2.MovingDownCoroutine());
                yield return StartCoroutine(_spires2.MovingUpCoroutine());
            }

            if (hp < _bossThirdStageHPValue)
            {
                yield return StartCoroutine(_spires3.MovingUpCoroutine());
                yield return StartCoroutine(_spires3.MovingDownCoroutine());

                yield return new WaitForSeconds(1f);

                yield return StartCoroutine(_spires4.MovingDownCoroutine());
                yield return StartCoroutine(_spires4.MovingUpCoroutine());
            }



            yield return new WaitForSeconds(2f);
            //yellow symbols, pause, destroy
            //box down

            int boxesCount = _boxes.transform.childCount;
            int randomCollidBox = Random.Range(1, boxesCount - 1);
            for (int i = 0; i < boxesCount; i++)
            {
                GameObject box = Resources.Load("BossFight1/box") as GameObject;

                GameObject boxOnScene = Instantiate(box, _boxes.transform.GetChild(i).transform);
                if (i == randomCollidBox)
                {
                    boxOnScene.GetComponent<Collider2D>().isTrigger = false;
                }
            }

            yield return new WaitForSeconds(2f);
            //green symbols, pause, destroy
            //column up
            int randomColumnTransformNum = Random.Range(1, _column.transform.childCount);
            _column.gameObject.transform.position = _columns.transform.GetChild(randomColumnTransformNum).transform.position;
            yield return StartCoroutine(_columns.MovingUpCoroutine());

            Transform checkBoxTransform = _column.transform;
            Collider2D upBox = Physics2D.OverlapCircle(checkBoxTransform.position, _circleRadius, _boxLayer);
            if (upBox != null)
            {
                Debug.Log("upBox");
                upBox.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, _boxUpForce));
            }

            yield return StartCoroutine(_columns.MovingDownCoroutine());



            yield return new WaitForSeconds(2f);
            hp--;
        }
    }




}


