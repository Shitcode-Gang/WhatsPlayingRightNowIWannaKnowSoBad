using Windows.Media.Control;

var smtc = GlobalSystemMediaTransportControlsSessionManager.RequestAsync().GetResults();

if (smtc == null)
    return;

var oldSession = smtc.GetCurrentSession();

oldSession.PlaybackInfoChanged += MediaHandler.HandlePlaybackUpdate;

smtc.SessionsChanged += (sender, eventArgs) =>
{
    var sessions = smtc.GetSessions();
    if (sessions.Count <= 0)
        return;
    
    if (sessions.All(session => session != oldSession))
    {
        Console.WriteLine("Old session gone, changing...");
        oldSession = smtc.GetCurrentSession();
        MediaHandler.ForceHandlePlaybackUpdate(oldSession);
        oldSession.PlaybackInfoChanged += MediaHandler.HandlePlaybackUpdate;
    }
};

smtc.CurrentSessionChanged += (sender, eventArgs) =>
{
    oldSession.PlaybackInfoChanged -= MediaHandler.HandlePlaybackUpdate;
    oldSession = smtc.GetCurrentSession();
    Console.WriteLine($"Changed current session to {oldSession.SourceAppUserModelId}");
    MediaHandler.ForceHandlePlaybackUpdate(oldSession);
    oldSession.PlaybackInfoChanged += MediaHandler.HandlePlaybackUpdate;
};

MediaHandler.ForceHandlePlaybackUpdate(oldSession);

while (Console.ReadLine() != "stop")
{
    
}
