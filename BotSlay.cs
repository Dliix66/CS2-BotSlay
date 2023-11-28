using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;

namespace BotSlay;

public class BotSlay: BasePlugin
{
	public override string ModuleName => "BotSlay";
	public override string ModuleVersion => "1.0.0";
	public override string ModuleAuthor => "Dliix66";
	public override string ModuleDescription => "Slay bots when the last player dies.";

	public override void Load(bool hotReload)
	{
		base.Load(hotReload);
		Server.PrintToChatAll($"[{ChatColors.Green}BotSlay{ChatColors.Default}] Plugin {(hotReload ? "hot-re" : "")}loaded!");
	}

	[GameEventHandler(HookMode.Post)]
	public HookResult OnPlayerDeath(EventPlayerDeath evnt, GameEventInfo info)
	{
		CCSPlayerController? player = evnt.Userid;
		if (player.IsValid == false || player.IsBot)
			return HookResult.Continue;

		List<CCSPlayerController> remainingPlayer = Utilities.GetPlayers();
		foreach (CCSPlayerController ccsPlayerController in remainingPlayer)
		{
			if (ccsPlayerController.IsValid == false || ccsPlayerController.IsBot ||
				ccsPlayerController.PlayerPawn.IsValid == false || ccsPlayerController == evnt.Userid)
				continue;

			if (ccsPlayerController.PlayerPawn.Value.Health > 0 &&
				ccsPlayerController.TeamNum != (byte)CsTeam.Spectator &&
				ccsPlayerController.TeamNum != (byte)CsTeam.None)
			{
				return HookResult.Continue;
			}
		}

		Server.PrintToChatAll($"[{ChatColors.Green}BotSlay{ChatColors.Default}] Slaying bots to end the round...");
		AddTimer(2f, SlayBots);

		return HookResult.Continue;
	}

	private void SlayBots()
	{
		List<CCSPlayerController> remainingPlayer = Utilities.GetPlayers();
		foreach (CCSPlayerController ccsPlayerController in remainingPlayer)
		{
			if (ccsPlayerController.IsValid == false || ccsPlayerController.PlayerPawn.IsValid == false ||
				ccsPlayerController.IsBot == false ||
				ccsPlayerController.TeamNum == (byte)CsTeam.Spectator || ccsPlayerController.TeamNum == (byte)CsTeam.None)
				continue;

			ccsPlayerController.PlayerPawn.Value.CommitSuicide(false, true);
		}
	}
}
