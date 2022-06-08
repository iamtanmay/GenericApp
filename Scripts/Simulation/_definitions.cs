using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All measures in SI units
namespace VRApplication
{
    public enum CompoundType    { Molecular, Ionic, Intermetallic, Complex  }

    public enum Bonding { Metallic, Covalent, Ionic, MolecularSolid }

    public enum Preparation { Powder, Strip, Semisolid, Crystal }

    public static class Constants
    {
        static float Mole = float.Parse("602214150000000000000000");
    }

    public struct SimulationTime
    {
        public float inSeconds;
        public uint inTicks;
    }

    public struct Formula
    {
        public string name;
        public bool isOrganic;
        public int compoundType;
        public float molecularmass;
        //Properties dependent on pressure
        public float[] meltingpoints;
        public float[] boilingpoints;
        //Properties dependent on temperature
        public float[] densities;
        public float[] pressures;
    }

    public abstract class Matter : MonoBehaviour
    {
        public float mass;
        public float moles;
        public float volume;
        public float temperature;
        public float pressure;
        public float density;
    }

    public abstract class Compound : Matter
    {
        public uint formulaID;
    }

    public class Solid : Compound
    {
        public int bonding;
        public bool isCrystalline;
        public int preparation;
    }

    public class Liquid : Compound
    {
    }

    public class Gas : Compound
    {
    }

    public class Plasma : Compound
    {
    }

    /// <summary>
    /// ReactionSystems check for Free Energy conditions in the system and tries to reach minimal Free Energy,
    /// i.e. Equilibrium for all ReactionRules. When all ReactionRates are 0, system is deactivated
    /// </summary>
    public class Reagent
    {
        public uint instanceID;

        public Compound compound;

        public void Create(float iamount)
        {
        }
    }

    public struct ReactionRule
    {
        public uint[] Reactants, Catalysts, Products;
        public uint[] CoefficientReactants, CoefficientCatalysts, CoefficientProducts;
        public int order;
        //Reaction rates dependent on temperature
        public float[] k;
        //Standard Enthalpy of reaction  
        public float dH;
    }

    public class Container : Matter
    {
        //ID 0 is the world container
        public uint instanceID;
        public float capacity;
        public float occupiedVolume;
        public Reagent[] reagents;
        public float[] concentrations;
        public bool[] IsSolvent;
        //Enthalpy in current tick
        public float dHInCurrentTick;
        public float internalEnergy;
        public bool isAtEquilibrium;

        public void Heat(float deltaEnergy)
        {
        }

        public void Stir(float deltaEnergy)
        {
        }

        public void Irradiate(float deltaEnergy)
        {
        }

        public void Pressure(float deltaPressue)
        {
        }

        public void Volume(float deltaVolume)
        {
        }

        public void Add(Reagent iagent, float amount)
        {
        }

        public void SimulationTick()
        {
        }
    }

    public abstract class Instrument : Matter
    {
        public Container sample;

        public void Activate()
        {
        }

        public void Configure()
        {
        }

        public void SimulationTick()
        {
        }
    }
}