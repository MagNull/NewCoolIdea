namespace Sources.Runtime.Models.Abilities
{
    public class Ability
    {
        private float _castTime;
        private float _coolDown;
        private string _name;
        private bool _mobility;

        public string Name => _name;

        public bool Mobility => _mobility;

        public Ability(string name, float castTime, float coolDown, bool mobility)
        {
            _name = name;
            _castTime = castTime;
            _coolDown = coolDown;
            _mobility = mobility;
        }
    }
}