using System;
using Sources.Runtime.Models.CharactersStateMachine;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Models.Abilities
{
    public class RangerMultishotState : Ability
    {
        private ProjectilesFactory _projectilesFactory;
        private Transform _projectileOrigin;
        private Camera _camera;
        
        public RangerMultishotState(Func<dynamic> getTarget, Transformable characterTransformable,
            StateMachine stateMachine, float coolDown, ProjectilesFactory factory, Transform projectileOrigin) 
            : base(getTarget, characterTransformable, stateMachine, coolDown)
        {
            _projectilesFactory = factory;
            _projectileOrigin = projectileOrigin;
            _camera = Camera.main;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            LookAtCursor();
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                CreateMultishot();
                _stateMachine.ChangeState<IdleState>();
            }
        }

        private void LookAtCursor()
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            
            var plane = new Plane(Vector3.up, Vector3.up);
            if (plane.Raycast(ray, out float enter))
            {
                Debug.Log(ray.GetPoint(enter));
                _characterTransformable.LookAt(ray.GetPoint(enter));
            }
        }

        private void CreateMultishot()
        {
            for (var i = -1; i <= 1; i++)
            {
                var projectile = new Projectile(_projectileOrigin.position + _projectileOrigin.right * i,
                    _projectileOrigin.rotation, _projectileOrigin.forward, 20, 1);
                _projectilesFactory.Create(projectile);
            }
        }
    }
}