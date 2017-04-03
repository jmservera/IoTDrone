using AR.Drone.Data;
using AR.Drone.Infrastructure;
using FFmpeg.AutoGen;

namespace AR.Drone.Video
{
    public class VideoPacketDecoder : DisposableBase
    {
        private readonly PixelFormat _pixelFormat;
        private VideoConverter _videoConverter;
        private VideoDecoder _videoDecoder;
        private AVPacket _avPacket;

        public VideoPacketDecoder(PixelFormat pixelFormat)
        {
            _pixelFormat = pixelFormat;
            _avPacket = new AVPacket();
        }

        public unsafe bool TryDecode(ref VideoPacket packet, out VideoFrame frame)
        {
            if (_videoDecoder == null) _videoDecoder = new VideoDecoder();


            fixed (byte* pData = &packet.Data[0])
            {
                _avPacket.data = pData;
                _avPacket.size = packet.Data.Length;
                AVFrame* pFrame;
                if (_videoDecoder.TryDecode(ref _avPacket, out pFrame))
                {
                    if (_videoConverter == null) _videoConverter = new VideoConverter(_pixelFormat.ToAVPixelFormat());

                    byte[] data = _videoConverter.ConvertFrame(pFrame);

                    frame = new VideoFrame();
                    frame.Timestamp = packet.Timestamp;
                    frame.Number = packet.FrameNumber;
                    frame.Width = packet.Width;
                    frame.Height = packet.Height;
                    frame.Depth = data.Length/(packet.Width*packet.Height);
                    frame.PixelFormat = _pixelFormat;
                    frame.Data = data;
                    return true;
                }
            }
            frame = null;
            return false;
        }

        protected override void DisposeOverride()
        {
            if (_videoDecoder != null) _videoDecoder.Dispose();
            if (_videoConverter != null) _videoConverter.Dispose();
        }
    }
}
