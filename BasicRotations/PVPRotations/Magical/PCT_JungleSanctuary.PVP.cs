namespace RebornRotations.PVPRotations.Magical;

[Rotation("Jungle Sanctuary PvP", CombatType.PvP, GameVersion = "7.2")]
[SourceCode(Path = "main/RebornRotations/PVPRotations/Magical/PCT_JungleSanctuary.PVP.cs")]
[Api(4)]
public class PCT_JungleSanctuaryPvP : PictomancerRotation
{
    #region Configurations

    [RotationConfig(CombatType.PvP, Name = "Use Purify")]
    public bool UsePurifyPvP { get; set; } = true;

    [RotationConfig(CombatType.PvP, Name = "Stop attacking while in Guard.")]
    public bool RespectGuard { get; set; } = true;

    [RotationConfig(CombatType.PvP, Name = "Prioritize Burst Damage")]
    public bool PrioritizeBurst { get; set; } = true;
    #endregion

    #region Standard PVP Utilities
    private bool DoPurify(out IAction? action)
    {
        action = null;
        if (!UsePurifyPvP) return false;

        var purifiableStatusesIDs = new List<int>
        {
            // Stun, DeepFreeze, HalfAsleep, Sleep, Bind, Heavy, Silence
            1343, 3219, 3022, 1348, 1345, 1344, 1347
        };

        if (purifiableStatusesIDs.Any(id => Player.HasStatus(false, (StatusID)id)))
        {
            return PurifyPvP.CanUse(out action);
        }

        return false;
    }
    #endregion

    #region oGCDs
    protected override bool EmergencyAbility(IAction nextGCD, out IAction? action)
    {
        action = null;
        if (RespectGuard && Player.HasStatus(true, StatusID.Guard)) return false;
        if (DoPurify(out action)) return true;

        // Use Guard when low health
        if (Player.GetHealthRatio() < 0.25 && GuardPvP.CanUse(out action)) return true;

        return base.EmergencyAbility(nextGCD, out action);
    }

    protected override bool DefenseSingleAbility(IAction nextGCD, out IAction? action)
    {
        action = null;
        if (RespectGuard && Player.HasStatus(true, StatusID.Guard)) return false;

        // Use Tempera Coat defensively
        if (Player.GetHealthRatio() < 0.6 && TemperaCoatPvP.CanUse(out action)) return true;

        return base.DefenseSingleAbility(nextGCD, out action);
    }

    protected override bool AttackAbility(IAction nextGCD, out IAction? action)
    {
        action = null;
        if (RespectGuard && Player.HasStatus(true, StatusID.Guard)) return false;

        // Prioritize Muses for burst damage
        if (PrioritizeBurst)
        {
            // Use all Muses in sequence for maximum burst
            if (PomMusePvP.CanUse(out action, usedUp: true)) return true;
            if (WingedMusePvP.CanUse(out action, usedUp: true)) return true;
            if (ClawedMusePvP.CanUse(out action, usedUp: true)) return true;
            if (FangedMusePvP.CanUse(out action, usedUp: true)) return true;
        }
        else
        {
            // Use Muses more strategically
            if (Target.GetHealthRatio() < 0.5)
            {
                if (PomMusePvP.CanUse(out action, usedUp: true)) return true;
                if (WingedMusePvP.CanUse(out action, usedUp: true)) return true;
                if (ClawedMusePvP.CanUse(out action, usedUp: true)) return true;
                if (FangedMusePvP.CanUse(out action, usedUp: true)) return true;
            }
        }

        // Manage Subtractive Palette based on movement
        switch (IsMoving)
        {
            case true:
                if (ReleaseSubtractivePalettePvP.CanUse(out action)) return true;
                break;
            case false:
                if (SubtractivePalettePvP.CanUse(out action)) return true;
                break;
        }

        return base.AttackAbility(nextGCD, out action);
    }

    #endregion

    #region GCDs
    protected override bool GeneralGCD(out IAction? action)
    {
        action = null;
        if (RespectGuard && Player.HasStatus(true, StatusID.Guard)) return false;

        // Star Prism is a high priority
        if (StarPrismPvP.CanUse(out action)) return true;

        // Use portraits when available
        if (MogOfTheAgesPvP.CanUse(out action)) return true;
        if (RetributionOfTheMadeenPvP.CanUse(out action)) return true;

        // Use Comet in Black for burst damage
        if (CometInBlackPvP.CanUse(out action, usedUp: true)) return true;

        // Use Creature Motif to set up combos
        if (CreatureMotifPvP.CanUse(out action)) return true;

        // Basic attack
        if (FireInRedPvP.CanUse(out action)) return true;

        return base.GeneralGCD(out action);
    }
    #endregion
}
