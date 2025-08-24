using UnityEngine;
[System.Serializable]
public class Mover
{

    public bool initialised = false;




    public virtual void Initialise(CharacterMotor motor)
    {
        cm = motor;
        initialised = true;
    }
    public CharacterMotor cm;
    /// <summary>
    /// Called when the player starts moving using this Mover
    /// </summary>
    internal virtual void StartedMoving()
    {
        Debug.Log($"Started moving on {ToString()}");
    }
    /// <summary>
    /// Called when the player stops moving using this Mover
    /// </summary>
    internal virtual void StoppedMoving()
    {

    }
    /// <summary>
    /// Checks if the player can move this way
    /// </summary>
    /// <returns></returns>
    internal virtual bool CanMove()
    {
        return false;
    }
    /// <summary>
    /// Calculates how the player should move
    /// </summary>
    /// <returns></returns>
    internal virtual void Process()
    {

    }
}
