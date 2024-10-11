using State_Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace State_Pattern
{
    public interface IVideoPlayState // 각 메서드를 호출하는 다리 역활 
    {
        void Play(VideoPlayer videoPlayer);
        void Pause(VideoPlayer videoPlayer);
        void Stop(VideoPlayer videoPlayer);
    }
    public class VideoPlayer
    {
        private IVideoPlayState curState;
        public VideoPlayer()
        {
            SetState(new StopState()); // 초기 값
        }
        public void SetState(IVideoPlayState newState) // 인터페이스를 통해 새로운 상태를 전환
        {
            curState = newState; // 현재 상태에 새로운 상태로 덮어쓰기
        }
        public void Play()
        {
            curState.Play(this);
        }
        public void Pause()
        {
            curState.Pause(this);
        }
        public void Stop()
        {
            curState.Stop(this);
        }
    }
    public class PlayState : IVideoPlayState
    {
        public void Play(VideoPlayer videoPlayer)
        {
            Console.WriteLine("이미 재생 중입니다...!");
        }
        public void Pause(VideoPlayer videoPlayer)
        {
            Console.WriteLine("비디오 일시 정지합니다...!");
            videoPlayer.SetState(new PauseState());
        }
        public void Stop(VideoPlayer videoPlayer)
        {
            Console.WriteLine("비디오 중지합니다...!");
            videoPlayer.SetState(new StopState());
        }
    }
    public class PauseState : IVideoPlayState
    {
        public void Play(VideoPlayer videoPlayer)
        {
            Console.WriteLine("비디오를 계속 재생합니다...!");
            videoPlayer.SetState(new PlayState());
        }
        public void Pause(VideoPlayer videoPlayer)
        {
            Console.WriteLine("이미 일시 정지 중입니다...!");
        }
        public void Stop(VideoPlayer videoPlayer)
        {
            Console.WriteLine("비디오 중지합니다...!");
            videoPlayer.SetState(new StopState());
        }
    }
    public class StopState : IVideoPlayState
    {
        public void Play(VideoPlayer videoPlayer)
        {
            Console.WriteLine("비디오를 재생 시작합니다...!");
            videoPlayer.SetState(new PlayState());
        }
        public void Pause(VideoPlayer videoPlayer)
        {
            Console.WriteLine("정지 상태에서는 일시 정지가 불가능 합니다...!");
        }
        public void Stop(VideoPlayer videoPlayer)
        {
            Console.WriteLine("이미 정지 중입니다...!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            VideoPlayer videoPlayer = new VideoPlayer(); // VideoPlayer 객체 생성

            videoPlayer.Play();  // 비디오 시작
            videoPlayer.Pause(); // 비디오 일시 정지
            videoPlayer.Play();  // 비디오 재개
            videoPlayer.Stop();  // 비디오 정지
        }
    }
}


    

