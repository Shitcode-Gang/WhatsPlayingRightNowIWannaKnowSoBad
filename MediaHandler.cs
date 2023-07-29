using Windows.Foundation;
using Windows.Media.Control;

namespace Testenk;

public static class MediaHandler
{
    public static void HandlePlaybackUpdate(
        GlobalSystemMediaTransportControlsSession session, PlaybackInfoChangedEventArgs args)
    {
        var props = session.TryGetMediaPropertiesAsync();
        while (props.Status != AsyncStatus.Completed)
        {
        }
        var status = session.GetPlaybackInfo().PlaybackStatus.ToString();
        Console.WriteLine($"Currently {status}");
        if (status == "Playing")
        {
            var media = props.GetResults();
            Console.WriteLine($"{media.Artist} - {media.Title}");
        }
    }

    public static void ForceHandlePlaybackUpdate(GlobalSystemMediaTransportControlsSession session)
    {
        HandlePlaybackUpdate(session, PlaybackInfoChangedEventArgs.FromAbi(IntPtr.Zero));
    }
}