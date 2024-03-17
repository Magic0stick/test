using Content.Server.Administration;
using Content.Server.Revenant.Components;
using Content.Server.Popups;
using Content.Shared.IdentityManagement;
using Content.Shared.Interaction;
using Content.Shared.Inventory;
using Content.Shared.Popups;
using Content.Shared.Timing;
using Content.Shared.Verbs;
using Content.Server.EUI;
using Content.Shared.Revenant.Components;


using Robust.Shared.Player;
using Robust.Shared.Toolshed;

namespace Content.Client.Revenant;

public sealed partial class TelepathySystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _invSystem = default!;
    [Dependency] private readonly ToolshedManager _toolshed = default!;
    [Dependency] private readonly PopupSystem _popupSystem = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;

    [Dependency] private readonly EuiManager _euiManager = default!;


    public override void Initialize()
    {
        SubscribeLocalEvent<GetVerbsEvent<Verb>>(GetVerbs);
    }

    private void GetVerbs(GetVerbsEvent<Verb> ev)
    {
    AddAdminVerbs(ev);
    }
/// Ещё не урезанная версия
    private void AddAdminVerbs(GetVerbsEvent<Verb> args)
    {
        if (!EntityManager.TryGetComponent(args.User, out ActorComponent? actor))
            return;

        var player = actor.PlayerSession;

        if (!HasComp<EssenceComponent>(args.User))
        {
            Verb mark = new();
            mark.Text = Loc.GetString("toolshed-verb-mark");
            mark.Message = Loc.GetString("toolshed-verb-mark-description");
            mark.Category = VerbCategory.Admin;
            mark.Act = () => _toolshed.InvokeCommand(player, "=> $marked", Enumerable.Repeat(args.Target, 1), out _);
            args.Verbs.Add(mark);

            if (TryComp(args.Target, out ActorComponent? targetActor))
            {
                // Subtle Messages
                Verb prayerVerb = new();
                prayerVerb.Text = Loc.GetString("prayer-verbs-subtle-message");
                prayerVerb.Category = VerbCategory.Admin;
                prayerVerb.Act = () =>
                {
                    _quickDialog.OpenDialog(player, "Subtle Message", "Message", "Popup Message", (string message, string popupMessage) =>
                {
                    _prayerSystem.SendSubtleMessage(targetActor.PlayerSession, player, message, popupMessage == "" ? Loc.GetString("prayer-popup-subtle-default") : popupMessage);
                });
                };
            }
        }
    }
}