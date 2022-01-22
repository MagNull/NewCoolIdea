namespace Sources.Runtime.Models.Abilities
{
    public class Ability : IUpdatable
    {
        private float _castTime;
        private float _coolDown;
        private string _name;
        private bool _mobility;
        private float _coolDownTimer;

        public string Name => _name;

        public bool Mobility => _mobility;

        public float CastTime => _castTime;

        public bool CanCast => _coolDownTimer <= 0;

        public Ability(string name, float castTime, float coolDown, bool mobility)
        {
            _name = name;
            _castTime = castTime;
            _coolDown = coolDown;
            _mobility = mobility;
        }

        public void StartCooldown() => _coolDownTimer = _coolDown;

        public void Update(float deltaTime)
        {
            if (_coolDownTimer > 0)
                _coolDownTimer -= deltaTime;
        }
    }
}