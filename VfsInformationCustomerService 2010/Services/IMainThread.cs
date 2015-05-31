using System;
using System.Collections.Generic;
using System.Text;

namespace VfsInformationFeedService
{
    public interface IMainThread
    {
        void Start();
        void Stop();
        void Pause();
        void Resume();
    }
}
