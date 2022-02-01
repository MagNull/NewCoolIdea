using System;
using Sources.Runtime.Models.Abilities;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AbilityCastState : State
    {
        public Ability Ability { get; set; }

        private readonly NavMeshAgent _navMeshAgent;

        private float _abilityCastingTime;

        public AbilityCastState(NavMeshAgent navMeshAgent, Func<dynamic> getTarget, Transformable characterTransformable,
            Func<Weapon> getWeapon, StateMachine stateMachine) 
            : base(getTarget, characterTransformable, getWeapon, stateMachine)
        {
            _navMeshAgent = navMeshAgent;
        }

        public override void Enter()
        {
            _navMeshAgent.isStopped = !Ability.Mobility;
            _abilityCastingTime = Ability.CastTime;
            if(_getWeapon.Invoke() is MeleeWeapon meleeWeapon)
                meleeWeapon.Activate();
        }

        public override void Exit()
        {
            Ability.StartCooldown();
            if (Ability.Mobility)
                _navMeshAgent.isStopped = true;
            if(_getWeapon.Invoke() is MeleeWeapon meleeWeapon)
                meleeWeapon.Deactivate();

        }

        public override void LogicUpdate()
        {
            if (_abilityCastingTime <= 0)
            {
                _stateMachine.ChangeState<IdleState>();
            }
        }

        public override void Update(float deltaTime)
        {
            dynamic target = _getTarget.Invoke();
            if (target is Transformable targetTransformable)
            {
                _navMeshAgent.SetDestination(targetTransformable.Position);
            }
            else if(target is Vector3 targetPoint)
            {
                _navMeshAgent.SetDestination(targetPoint); ;
            }
            _characterTransformable.MoveTo(_navMeshAgent.nextPosition);
            _abilityCastingTime -= deltaTime;
        }
    }
}