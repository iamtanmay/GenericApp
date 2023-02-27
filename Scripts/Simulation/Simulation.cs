using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppStarter
{
    public class Simulation : MonoBehaviour
    {
        public SimulationTime _time;

        public Transform ContainersParent;

        public Transform AnalyzersParent;

        public Transform StimulatorsParent;

        //Formulas by ID
        public Dictionary<uint, Formula> _formulas;

        //Reaction formulas by ID
        public Dictionary<uint, ReactionRule> _rules;

        //Reagent instances by instanceID
        public Dictionary<uint, Reagent> _reagents;

        //Container instances by instanceID
        public Dictionary<uint, Container> _containers;

        //Stimulator instances by instanceID
        public Dictionary<uint, Instrument> _stimulators;

        //Analyzer instances by instanceID
        public Dictionary<uint, Instrument> _analyzers;

        public bool _init = false;

        public void Start()
        {            
        }

        public void Awake()
        {            
        }

        public void Init()
        {
            _formulas = new Dictionary<uint, Formula>();

            _rules = new Dictionary<uint, ReactionRule>();

            _reagents = new Dictionary<uint, Reagent>();

            _containers = new Dictionary<uint, Container>();

            _stimulators = new Dictionary<uint, Instrument>();

            _analyzers = new Dictionary<uint, Instrument>();

            _init = true;
            _time.inSeconds = 0f;
            _time.inTicks = 0;
        }

        public void FixedUpdate()
        {
            //Process all Containers not at Equilibrium
            foreach (KeyValuePair<uint, Container> container in _containers)
            {
                //Container SimulationTick() Processes ReactionRules to Create()/ Destroy() reactants and change reactant properties.Calls FXSystem -> Transition() for effects / audio
            }

            //Process all active Stimulators
            foreach (KeyValuePair<uint, Instrument> stimulator in _stimulators)
            {
                //Stimulator SimulationTick() Adds input to ReactionSystem, for example adding Heat energy, ReactionSystem->Heat()
            }

            //Process all active Analyzers
            foreach (KeyValuePair<uint, Instrument> analyzer in _analyzers)
            {
                //Analyzer SimulationTick() Reports ReactionSystem state properties
            }

            _time.inSeconds += Time.fixedDeltaTime;
            _time.inTicks++;
        }
    }
}