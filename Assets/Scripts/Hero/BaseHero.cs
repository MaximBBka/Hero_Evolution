using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public abstract class BaseHero : DragHandler, IMerge, IMoney
    {
        [field: SerializeField] public ModelHero Model { get; private set; }

        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _render;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerManager layerManager;


        public int MaxIndex;
        public int CurrentIndex;
        public event IMerge.CallBackMerge OnMerge;
        public event IMoney.CallbackMoney OnMoneyChange;
        public event IMoney.CallbackUpMoney OnMoneyUp;

        public void Init(LayerManager manager, ModelHero model)
        {
            layerManager = manager;
            Model = model;
        }
        public override void OnMouseDrag()
        {
            base.OnMouseDrag();
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            vector.z = layerManager.SetLayer(transform.position.y) + Random.Range(0.0001f, 0.1111f);
            transform.position = vector;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _collider2D && colliders[i].TryGetComponent<BaseHero>(out BaseHero baseHero))
                {
                    if (transform.position.y > baseHero.transform.position.y)
                    {
                        baseHero._render.sortingOrder = 2;
                        _render.sortingOrder = 1;
                    }
                    else
                    {
                        baseHero._render.sortingOrder = 1;
                        _render.sortingOrder = 2;
                    }
                }
            }
        }
        public override void OnStartDrag()
        {
            base.OnStartDrag();
            OnMoneyChange.Invoke(Model.GetMoney);
            OnMoneyUp.Invoke();
            _animator.SetTrigger("IsAttack");
        }

        public override void OnEndDrag()
        {
            base.OnEndDrag();
            if (CurrentIndex >= MaxIndex)
            {
                return;
            }
            BaseHero baseHero = SearchUnit();
            if (baseHero != null && baseHero.GetType() == GetType())
            {
                OnMerge.Invoke(baseHero, this);
            }
        }

        private BaseHero SearchUnit()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _collider2D && colliders[i].TryGetComponent<BaseHero>(out BaseHero baseHero))
                {
                    return baseHero;
                }
            }
            return null;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
        }
    }
}
